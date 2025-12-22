using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndObj : MonoBehaviour
{
    public bool isStart;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PopUpManager.Instance.ShowPopup("Press F");

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isStart)
                    other.gameObject.transform.position = GameSystemManager.Instance.mapStartPos;
                else
                    GameSystemManager.Instance.MapClear();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PopUpManager.Instance.HidePopUP();
        }
    }
}
