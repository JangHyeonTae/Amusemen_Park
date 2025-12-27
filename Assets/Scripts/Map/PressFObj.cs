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
                    StartGame();
                    break;
                case PressEnum.End:
                    GoToEnd();
                    break;
                case PressEnum.Shop:
                    ShowShop();
                    break;
                case PressEnum.Item:
                    GetItem();
                    break;
            }
        }
    }
    private void StartGame()
    {
        Vector3 target = GameSystemManager.Instance.mapStartPos;
        target.y = playerTr.position.y;

        TeleportPlayer(playerTr, target);
    }
    private void GoToEnd()
    {
        GameSystemManager.Instance.MapClear();
    }
    private void ShowShop()
    {
        shopManager.ShowShop();
    }
    private void GetItem()
    {
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
