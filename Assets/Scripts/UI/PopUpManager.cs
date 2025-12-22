using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] private PopupPanel popupPanel;


    public void ShowPopup(string message)
    {
        if (popupPanel == null)
        {
            return;
        }

        popupPanel.ShowPopUp(message);
    }

    public void HidePopUP()
    {
        popupPanel.HidePopUp();
    }

}
