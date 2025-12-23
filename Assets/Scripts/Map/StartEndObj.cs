using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndObj : MonoBehaviour
{
    public bool isStart;

    private Transform playerTr;
    private bool canInteract;

    private void Update()
    {
        if (!canInteract || playerTr == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isStart)
            {
                Vector3 target = GameSystemManager.Instance.mapStartPos;
                target.y = playerTr.position.y;

                TeleportPlayer(playerTr, target);
            }
            else
            {
                GameSystemManager.Instance.MapClear();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerTr = other.transform;
        canInteract = true;

        PopUpManager.Instance.ShowPopup("F", transform.position + new Vector3(0, 2, 0));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PopUpManager.Instance.HidePopUP();
        }
    }

    private void TeleportPlayer(Transform tr, Vector3 target)
    {
        tr.position = target;

        var rb = tr.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = target;
            return;
        }
    }
}
