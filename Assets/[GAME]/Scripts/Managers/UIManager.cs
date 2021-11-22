﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;     
    }
    #endregion

    [Header("Panels")]
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gameInPanel;
    [SerializeField] GameObject retryPanel;
    [SerializeField] GameObject nextLevelPanel;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI diamondNumText;

    [Header("Diamond Variables")]
    [SerializeField] Image diamondImage;
    [SerializeField] GameObject diamond;
    [HideInInspector] public int diamondNum;
    Vector2 anchoredDiamondPos;

    [Header("Scipt References")]
    PlayerController playerController;

    [Header("References")]
    Camera cam;


    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        playerController = FindObjectOfType<PlayerController>();

        DiamondFirstSet();
    }

    public void UpdateLevelText()
    {
        levelText.text = "LEVEL " + PlayerPrefs.GetInt("FakeLevel", 1).ToString();
    }

    public void OpenClosePanels(int panelType) //  0 start // 1 fail // 2 nexxtLevel
    {
        switch (panelType)
        {
            case 0:
                startPanel.SetActive(!startPanel.activeInHierarchy);
                gameInPanel.SetActive(true);
                playerController.userActive = true;

                //Debug.LogError("SET USER ACTIVE");
                GameManager.instance.SendLevelStartedEvent();
                GameManager.instance.ManageGameStatus(GameManager.GameSituation.isStarted);

                break;

            case 1:
                retryPanel.SetActive(!retryPanel.activeInHierarchy);
                gameInPanel.SetActive(true);
                playerController.userActive = false;

                //Debug.LogError("SET USER PASSIVE");

                break;

            case 2:
                nextLevelPanel.SetActive(!nextLevelPanel.activeInHierarchy);
                gameInPanel.SetActive(true);
                playerController.userActive = false;

                //Debug.LogError("SET USER PASSIVE");

                break;
        }
    }

    private void DiamondFirstSet() // initial uı adjust
    {
        diamondNum = PlayerPrefs.GetInt("Diamond", 0);
        diamondNumText.text = diamondNum.ToString();
        anchoredDiamondPos = diamondImage.GetComponent<RectTransform>().anchoredPosition;
    }

    public void DiamondCollectAnim(Vector3 diamondPos) // just call this method to increase diamond count "1" 
    {
        Vector2 screenPos = cam.WorldToScreenPoint(diamondPos);
        GameObject clone = Instantiate(diamond, screenPos, Quaternion.identity, gameInPanel.transform);
        clone.GetComponent<RectTransform>().DOAnchorPos(anchoredDiamondPos, .5f)
            .OnComplete(() => 
            { 
                clone.SetActive(false);
                UpdateDiamondText();
            });
    }
    void UpdateDiamondText()
    {
        diamondNum++;
        PlayerPrefs.SetInt("Diamond", diamondNum);
        diamondNumText.text = PlayerPrefs.GetInt("Diamond").ToString();
    }
}
