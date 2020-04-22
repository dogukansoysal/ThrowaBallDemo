using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;
    
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI StreakText;
    public TextMeshProUGUI RemainingBallText;
    public Transform BallScoreHolder;
    public Transform BallImagePrefab;

    
    public TextMeshProUGUI RestartMenuScoreText;
    
    public GameObject MainMenuPanel;
    public GameObject RestartMenuPanel;
    public GameObject GamePanel;

    public TextMeshProUGUI RestartMenuWinText;
    public TextMeshProUGUI RestartMenuLoseText;

    
    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }


    #region Game UI
    
    public void ShowGamePanel()
    {
        InitBallScoreImages();
        UpdateGamePanel();
        
        GamePanel.SetActive(true);
        HideMainMenuPanel();
        HideRestartMenuPanel();
    }
    
    
    
    public void HideGamePanel()
    {
        GamePanel.SetActive(false);
    }

    
    
    public void UpdateGamePanel()
    {
        UpdateScoreText();
        UpdateStreakText();
        UpdateRemainingBallText();
        UpdateBallScoreImages();

    }
    
    
    
    private void UpdateScoreText()
    {
        ScoreText.text = GameManager.Instance.score.ToString();
    }

    
    
    private void UpdateStreakText()
    {
        StreakText.enabled = GameManager.Instance.streak > 0;

        StreakText.text = "X" + (GameManager.Instance.streak + 1).ToString();
    }
    
    
    
    private void UpdateRemainingBallText()
    {
        RemainingBallText.text = "X" + GameManager.Instance.Level.ballCount.ToString();
    }
    
    
    
    private void UpdateBallScoreImages()
    {
        
        var ballScore = GameManager.Instance.ballScore;
        for (int i = 0; i < ballScore; i++)
        {
            ColorUtility.TryParseHtmlString(GameConstants.GreenHexValue, out var tempColor);
            BallScoreHolder.GetChild(i).GetComponent<Image>().color = tempColor;
        }

        for (int i = ballScore; i < BallScoreHolder.childCount; i++)
        {
            ColorUtility.TryParseHtmlString(GameConstants.GrayHexValue, out var tempColor);
            BallScoreHolder.GetChild(i).GetComponent<Image>().color = tempColor;
        }
    }



    private void InitBallScoreImages()
    {
        while (BallScoreHolder.childCount > 0)
        {
            DestroyImmediate(BallScoreHolder.GetChild(0).gameObject);
        }

        for (int i = 0; i < GameManager.Instance.Level.requiredBall; i++)
        {
            Instantiate(BallImagePrefab, BallScoreHolder);
        }
    }
    
    #endregion


    #region Main Menu UI
    
    public void ShowMainMenuPanel()
    {
        MainMenuPanel.SetActive(true);
        HideRestartMenuPanel();
        HideGamePanel();
    }
    
    
    
    public void HideMainMenuPanel()
    {
        MainMenuPanel.SetActive(false);
    }
    #endregion
    
    
    
    #region Restart Menu UI

    
    public void ShowRestartMenuPanel(bool isWin)
    {
        UpdateRestartMenuPanel(isWin);
        
        RestartMenuPanel.SetActive(true);
        HideMainMenuPanel();
        HideGamePanel();
    }

    
    
    public void HideRestartMenuPanel()
    {
        RestartMenuPanel.SetActive(false);
    }



    private void UpdateRestartMenuPanel(bool isWin)
    {
        UpdateRestartMenuScoreText();
        if (isWin)
        {
            ShowRestartMenuWinText();
        }
        else
        {
            ShowRestartMenuLostText();
        }
    }
    
    
    
    
    public void UpdateRestartMenuScoreText()
    {
        RestartMenuScoreText.text = "Score: " + GameManager.Instance.score.ToString();
    }
    
    
    
    public void ShowRestartMenuWinText()
    {
        RestartMenuWinText.enabled = true;
        RestartMenuLoseText.enabled = false;
    }

    
    
    public void ShowRestartMenuLostText()
    {
        RestartMenuLoseText.enabled = true;
        RestartMenuWinText.enabled = false;
    }
    

    #endregion

}
