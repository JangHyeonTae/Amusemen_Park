using Invector.vCamera;
using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    private vThirdPersonController vController;
    private vThirdPersonCamera cameraController;

    [SerializeField] private GameObject inventoryView;
    [SerializeField] private GameObject shopView;

    private bool isInventoryOpen;
    public bool IsInventoryOpen { get { return isInventoryOpen; } set { isInventoryOpen = value; OnInventoryOpen?.Invoke(isInventoryOpen); } }
    public Action<bool> OnInventoryOpen;

    private void Awake()
    {
        cameraController = gameObject.GetComponentInChildren<vThirdPersonCamera>();
        inventoryView = FindObjectOfType<InventoryView>().transform.Find("InventoryView").gameObject;
        shopView = FindObjectOfType<ShopView>().transform.Find("ShopView").gameObject;
    }

    private void OnEnable()
    {
        OnInventoryOpen += OpenInventroy;
        ShopManager.Instance.OnOpen += OpenShop;
    }

    private void OnDisable()
    {
        OnInventoryOpen -= OpenInventroy;

        ShopManager.Instance.OnOpen -= OpenShop;
    }

    private void Start()
    {
        vController = GetComponent<vThirdPersonController>();
        IsInventoryOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(inventoryView == null)
                inventoryView = FindObjectOfType<InventoryView>().transform.Find("InventoryView").gameObject;

            IsInventoryOpen = !IsInventoryOpen;
        }
    }

    private void OpenInventroy(bool value)
    {
        if(inventoryView == null)
            inventoryView = FindObjectOfType<InventoryView>().transform.Find("InventoryView").gameObject;

        inventoryView.gameObject.SetActive(value);

        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;

        if (vController != null)
            vController.lockRotation = value;

        if (cameraController != null)
            cameraController.enabled = !value;
    }

    private void OpenShop(bool value)
    {
        if (shopView == null)
        {
            shopView = FindObjectOfType<ShopView>().transform.Find("ShopView").gameObject;
        }

        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;

        if (vController != null)
            vController.lockRotation = value;

        if (cameraController != null)
            cameraController.enabled = !value;
    }

    public void TakeDamage()
    {
        Die();
    }

    public void Die()
    {
        Debug.Log("플레이어 죽음");
        GameSystemManager.Instance.currentStage = 0;
    }
}

