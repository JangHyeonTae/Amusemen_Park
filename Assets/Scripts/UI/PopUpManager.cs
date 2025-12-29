using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] private string popUpCanvasKey = "PopUpCanvas";

    private GameObject popUpCanvas;
    private PopupPanel popupPanel;

    private bool ensuring;

    public void ShowPopup(string message, Vector3 pos)
    {
        ShowPopupAsync(message, pos).Forget();
    }

    public void HidePopUP()
    {
        HidePopUpAsync().Forget();
    }

    private async UniTaskVoid ShowPopupAsync(string message, Vector3 pos)
    {
        await EnsurePopupPanelAsync();

        if (popupPanel == null) return;

        popupPanel.transform.position = pos;
        popupPanel.ShowPopUp(message);
    }

    private async UniTaskVoid HidePopUpAsync()
    {
        await EnsurePopupPanelAsync();
        if (popupPanel == null) return;

        popupPanel.HidePopUp();
    }

    private async UniTask EnsurePopupPanelAsync()
    {
        if (popupPanel != null) return;

        if (ensuring)
        {
            while (ensuring && popupPanel == null)
                await UniTask.Yield();
            return;
        }

        ensuring = true;

        try
        {
            popupPanel = FindObjectOfType<PopupPanel>(true);

            if (popupPanel != null)
            {
                popUpCanvas = popupPanel.transform.root.gameObject;
                return;
            }

            popUpCanvas = await Addressables.InstantiateAsync(popUpCanvasKey);

            if (popUpCanvas == null)
            {
                return;
            }

            var tr = popUpCanvas.transform.Find("PopupPanel");
            if (tr == null)
            {
                return;
            }

            popupPanel = tr.GetComponent<PopupPanel>();
            if (popupPanel == null)
            {
                return;
            }
        }
        finally
        {
            ensuring = false;
        }
    }
}
