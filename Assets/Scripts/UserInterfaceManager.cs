using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;    // Singleton definition
    
    public TextMeshProUGUI ScoreText;                 // Score text reference on in-Game screen
    public TextMeshProUGUI StreakText;                // Streak text reference on in-Game screen
    public TextMeshProUGUI RemainingBallText;        // Remaining Ball text reference on in-Game screen
    public Transform BallScoreHolder;                // Ball score indicator image holder reference on in-Game screen
    public Transform BallImagePrefab;                // Ball image prefab which will be used as indicator.

    
    public TextMeshProUGUI RestartMenuScoreText;    // Score text reference on Restart Menu screen
    
    public GameObject MainMenuPanel;                // Main Menu panel and it's UIElements parent.
    public GameObject RestartMenuPanel;             // Restart Menu panel and it's UIElements parent.
    public GameObject GamePanel;                    // in-Game UIElements parent.

    public TextMeshProUGUI RestartMenuWinText;      // Win Text reference on Restart Menu screen
    public TextMeshProUGUI RestartMenuLoseText;     // Lose Text reference on Restart Menu screen

    // Awake function from MonoBehaviour.
    private void Awake()
    {
        if (!Instance)    // Determine if instance is null
            Instance = this;    // Assign instance
        else if(Instance != this)    // Determine if instance already assigned
            Destroy(gameObject);    // If we have that already, we don't need another one.
    }


    // Active Panel and UI Elements on in-Game screen.
    #region Game UI
    
    /// <summary>
    /// Show in-Game Panel.
    /// </summary>
    public void ShowGamePanel()
    {
        InitBallScoreImages();
        UpdateGamePanel();
        
        GamePanel.SetActive(true);
        HideMainMenuPanel();
        HideRestartMenuPanel();
    }
    
    
    /// <summary>
    /// Hides in Game screen panel and it's elements.
    /// </summary>
    public void HideGamePanel()
    {
        GamePanel.SetActive(false);
    }

    
    /// <summary>
    /// Updates all game panel in Game screen.
    /// </summary>
    public void UpdateGamePanel()
    {
        UpdateScoreText();
        UpdateStreakText();
        UpdateRemainingBallText();
        UpdateBallScoreImages();

    }
    
    
    /// <summary>
    /// Updates score text in Game screen.
    /// </summary>
    private void UpdateScoreText()
    {
        ScoreText.text = GameManager.Instance.score.ToString();
    }

    
    /// <summary>
    /// Updates streak text in Game screen.
    /// </summary>
    private void UpdateStreakText()
    {
        StreakText.enabled = GameManager.Instance.streak > 0;

        StreakText.text = "X" + (GameManager.Instance.streak + 1).ToString();
    }
    
    
    /// <summary>
    /// Updates remaining Ball text in Game screen.
    /// </summary>
    private void UpdateRemainingBallText()
    {
        RemainingBallText.text = "X" + GameManager.Instance.Level.ballCount.ToString();
    }
    
    
    /// <summary>
    /// Update required ball indicator images.
    /// </summary>
    private void UpdateBallScoreImages()
    {
        // Color as "green" for each ball score.
        for (int i = 0; i < GameManager.Instance.ballScore; i++)
        {
            ColorUtility.TryParseHtmlString(GameConstants.GreenHexValue, out var tempColor);
            BallScoreHolder.GetChild(i).GetComponent<Image>().color = tempColor;
        }
    }


    /// <summary>
    /// Initializing required ball indicator images with the count of level's settings.
    /// </summary>
    private void InitBallScoreImages()
    {
        // If has child already, destroy all.
        while (BallScoreHolder.childCount > 0)
        {
            DestroyImmediate(BallScoreHolder.GetChild(0).gameObject);
        }
        // Spawn new Indicator images.
        for (int i = 0; i < GameManager.Instance.Level.requiredBall; i++)
        {
            Instantiate(BallImagePrefab, BallScoreHolder);
        }
    }
    
    #endregion


    // Active Panel and UI Elements on Main Menu screen.
    #region Main Menu UI
    
    /// <summary>
    /// Show main menu panel and it's elements.
    /// </summary>
    public void ShowMainMenuPanel()
    {
        MainMenuPanel.SetActive(true);
        HideRestartMenuPanel();
        HideGamePanel();
    }
    
    
    /// <summary>
    /// Hide main menu panel and it's elements.
    /// </summary>
    public void HideMainMenuPanel()
    {
        MainMenuPanel.SetActive(false);
    }
    #endregion
    
    
    // Active Panel and UI Elements on Restart Menu screen.
    #region Restart Menu UI

    /// <summary>
    /// Show restart menu panel and it's elements.
    /// </summary>
    public void ShowRestartMenuPanel(bool isWin)
    {
        UpdateRestartMenuPanel(isWin);
        
        RestartMenuPanel.SetActive(true);
        HideMainMenuPanel();
        HideGamePanel();
    }

    
    /// <summary>
    /// Hide restart menu panel and it's elements.
    /// </summary>
    public void HideRestartMenuPanel()
    {
        RestartMenuPanel.SetActive(false);
    }


    /// <summary>
    /// Update restart menu panel and it's elements.
    /// </summary>
    private void UpdateRestartMenuPanel(bool isWin)
    {
        UpdateRestartMenuScoreText();
        if (isWin)
        {
            ShowRestartMenuWinText();
        }
        else
        {
            ShowRestartMenuLoseText();
        }
    }
    
    
    
    /// <summary>
    /// Show restart menu score text
    /// </summary>
    public void UpdateRestartMenuScoreText()
    {
        RestartMenuScoreText.text = "Score: " + GameManager.Instance.score.ToString();
    }
    
    
    /// <summary>
    /// Show restart menu win text.
    /// </summary>
    public void ShowRestartMenuWinText()
    {
        RestartMenuWinText.enabled = true;
        RestartMenuLoseText.enabled = false;
    }

    
    /// <summary>
    /// Show restart menu lose text.
    /// </summary>
    public void ShowRestartMenuLoseText()
    {
        RestartMenuLoseText.enabled = true;
        RestartMenuWinText.enabled = false;
    }
    

    #endregion

}
