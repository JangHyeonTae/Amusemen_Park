using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    public void ShowPopUp(string message)
    {
        gameObject.SetActive(true);
        messageText.text = message;
    }

    public void HidePopUp()
    {
        gameObject.SetActive(false);
        messageText.text = "";
    }
}
