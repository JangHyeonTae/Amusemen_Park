using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] private PopupPanel popupPanel;


    public void ShowPopup(string message, Vector3 pos)
    {
        if (popupPanel == null)
        {
            return;
        }

        popupPanel.ShowPopUp(message);
        popupPanel.transform.position = pos;
    }

    public void HidePopUP()
    {
        popupPanel.HidePopUp();
    }

}
