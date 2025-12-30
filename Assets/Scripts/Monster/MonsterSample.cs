using Cysharp.Threading.Tasks;
using Invector;
using Invector.vCharacterController.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum MState
{
    Idle,
    Move,
    Attack,
    Dead
}

public class MonsterSample : PooledObject
{
    public LayerMask targetLayer;
    public MState state;
    [SerializeField] private float searchRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    public PlayerController target;
    private Animator animator;
    private vSimpleMeleeAI_Animator vAnimator;

    private Vector3 startPos;
    private bool canAttack;

    CancellationTokenSource token;

    public void Init()
    {
        target = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        vAnimator = GetComponent<vSimpleMeleeAI_Animator>();
        startPos = transform.position;
        ResetToken();
    }

    private void OnDisable()
    {
        if (token != null)
        {
            token.Cancel();
            token.Dispose();
            token = null;
        }
        Dead();
    }

    private void ResetToken()
    {
        if (token != null)
        {
            token.Cancel();
            token.Dispose();
            token = null;
        }

        token = new CancellationTokenSource();
    }

    private void Update()
    {
        ChangeAnim(state);
    }

    private void Idle()
    {
        animator.Play("Idle");
        SearchTarget();
    }

    private void SearchTarget()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= searchRange)
        {
            state = MState.Move;
            ChangeAnim(state);
        }
    }

    private void Move()
    {
        animator.Play("Walk2");
        LookTarget();

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);

        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            state = MState.Attack;
            ChangeAnim(state);
        }

        if (Vector3.Distance(transform.position, target.transform.position) > searchRange + 0.5f)
        {
            state = MState.Idle;
            ChangeAnim(state);
        }
    }


    private void Attack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) >= attackRange + 0.5f)
        {
            state = MState.Move;
            ChangeAnim(state);
        }
        LookTarget();
        AttackCool().Forget();
    }

    private async UniTaskVoid AttackCool()
    {
        try
        {
            while (true)
            {
                if (token == null || token.Token.IsCancellationRequested)
                    return;

                if (!animator)
                    return;

                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: token.Token);

                if (animator != null)
                {
                    vAnimator.MeleeAttack();
                }

                if (Vector3.Distance(transform.position, target.transform.position) >= attackRange + 0.5f)
                {
                    break;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken: token.Token);

            }
        }
        catch (OperationCanceledException) { }
    }

    public void AttackEvent()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, 3, targetLayer);

        foreach (Collider target in _colliders)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y = 0;
            Vector3 attackPos = transform.position;
            attackPos.y = 0;

            Vector3 targetDir = (targetPos - attackPos).normalized;

            if (Vector3.Angle(transform.forward, targetDir) > 180 * 0.5f)
                continue;


            IDamagable damageable = target.GetComponent<IDamagable>();
            if (damageable != null)
            {
                Debug.Log($"{target.name}");
                damageable.TakeDamage();
            }
            else
            {
                Debug.Log("damageable null");
            }
        }
    }
    private void LookTarget()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        dir.y = 0;
        transform.forward = dir;
    }

    private void Dead()
    {
        vAnimator.isDead = true;
        vAnimator.DeadAnimation();
    }


    private void ChangeAnim(MState state)
    {

        switch (state)
        {
            case MState.Idle:
                Idle();
                break;
            case MState.Move:
                Move();
                break;
            case MState.Attack:
                Attack();
                break;
        }
    }

    
}
