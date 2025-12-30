using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private GameObject CreditText;
    private void OnEnable()
    {
        if (startButton == null)
            startButton = GetComponentInChildren<Button>();

        if(creditButton == null)
            creditButton = GetComponentInChildren<Button>();

        startButton.onClick.AddListener(GameStart);
        creditButton.onClick.AddListener(OpenCredit);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(GameStart);
        creditButton.onClick.RemoveListener(OpenCredit);
    }

    private void GameStart()
    {
        GameSystemManager.Instance.currentStage = 0;
        GameSystemManager.Instance.ChangeScene("Map");
    }

    private void OpenCredit()
    {
        if (CreditText == null)
            return;

        CreditText.SetActive(true);
    }

    public void Cancel()
    {
        if (CreditText == null)
            return;

        CreditText.SetActive(false);
    }
}
