using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PressEnum
{
    Start,
    End,
    Shop,
    Item
}

public class PressFObj : MonoBehaviour
{
    [SerializeField] private PressEnum pressEnum;

    private Transform playerTr;
    private bool canInteract;
    private SampleItem sampleItem;
    private ShopManager shopManager;

    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        sampleItem = GetComponent<SampleItem>();
        shopManager = GetComponent<ShopManager>();
    }


    private void Update()
    {
        if (!canInteract || playerTr == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {

            switch (pressEnum)
            {
                case PressEnum.Start:
                    StartGame(3);
                    break;
                case PressEnum.End:
                    GoToEnd(3);
                    break;
                case PressEnum.Shop:
                    ShowShop(10);
                    break;
                case PressEnum.Item:
                    GetItem(2);
                    break;
            }
        }
    }

    

    private void StartGame(float range)
    {
        if (Physics.OverlapSphere(transform.position, range, targetLayer).Length <= 0)
            return;
        Vector3 target = GameSystemManager.Instance.mapStartPos;
        target.y = playerTr.position.y;

        TeleportPlayer(playerTr, target);
    }
    private void GoToEnd(float range)
    {
        if (Physics.OverlapSphere(transform.position, range, targetLayer).Length <= 0)
            return;
        GameSystemManager.Instance.ChangeShopScene("ShopScene");
    }
    private void ShowShop(float range)
    {
        if (Physics.OverlapSphere(transform.position, range, targetLayer).Length <= 0)
            return;
        shopManager.ShowShop();
    }
    private void GetItem(float range)
    {
        if (Physics.OverlapSphere(transform.position, range, targetLayer).Length <= 0)
            return;
        if (sampleItem == null)
            return;

        sampleItem.GetItem();
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
            canInteract = false;
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
