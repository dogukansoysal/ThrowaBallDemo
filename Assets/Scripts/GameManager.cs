using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using static GameConstants;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;    // Singleton definition

    /* Prefabs */
    public Transform currentLevelItems;
    public SoLevel LevelSoPrefab;
    
    /* Common Variables */
    public GameState GameState;
    public SoLevel Level;

    /* Game Logic Variables */
    public int streak;
    public int score;
    public int ballScore;

    
    public Vector3 DragDirection;
    public float DragDistance;
    
    private void Awake()
    {
        if (!Instance)    // Determine if instance is null
            Instance = this;    // Assign instance
        else if(Instance != this)    // Determine if instance already assigned
            Destroy(gameObject);    // If we have that already, we don't need another one.
    }


    private void Start()
    {
        LoadLevel();
        ShowMainMenu();
    }
    
    // Update is called once per frame.
    private void Update()
    {
        if (BallController.Instance.BallState != BallState.Ready) return;
        
        switch (InputManager.Instance.InputState)
        {
            // Holding input
            case InputState.Hold:
                CalculateDrag();
                BallController.Instance.DragBall(DragDirection, DragDistance);
                //DrawTrajectory();
                break;
            
            // Released input
            case InputState.Released:
                CalculateDrag();
                if (DragDistance < DragDistanceMin)
                    BallController.Instance.ResetBallPosition();
                else
                    BallController.Instance.ThrowBall();
                break;
        }
    }

    
    
    #region Game Logic Functions
    /*
    private void DrawTrajectory()
    {
        transform.GetComponent<TrajectorySimulation>().setTrajectoryPoints(BallController.Instance.Ball.position, -DragDirection * DragDistance * ThrowForce);
    }
    */
    
    
    
    private void CalculateDrag()
    {
        var firstPos = InputManager.Instance.FirstTouchPosition;
        var lastPos = InputManager.Instance.LastTouchPosition;

        // Correct if pulling upwards.
        lastPos.y = lastPos.y > firstPos.y ? firstPos.y : lastPos.y;
        lastPos.z = lastPos.z > firstPos.z ? firstPos.z : lastPos.z;

        var heading = lastPos - firstPos;
        DragDistance = heading.magnitude > DragDistanceMax ? DragDistanceMax : heading.magnitude;
        DragDirection = heading / heading.magnitude; // Normalized direction
        DragDirection = new Vector3(DragDirection.x, -Mathf.Clamp(DragDistance/DragDistanceMax * 0.5f, 0f, 0.5f), DragDirection.y);
    }
    
    
    
    public void BallInHole()
    {
        IncreaseStreak();
        IncreaseScore();
        IncreaseBallScore();
        BallController.Instance.KillBall();
        if (ballScore >= Level.requiredBall)
        {
            FinishGame();
        }
        else
        {
            SpawnNewBall();
            UserInterfaceManager.Instance.UpdateGamePanel();
        }
    }

    
    
    public void BallOutHole()
    {
        ResetStreak();
        BallController.Instance.KillBall();
        SpawnNewBall();
        UserInterfaceManager.Instance.UpdateGamePanel();
    }

    
    
    public void SpawnNewBall()
    {
        if (Level.ballCount > 0)
        {
            DecreaseBallCount();
            BallController.Instance.InitBall();
        }
        else
        {
            FinishGame();
        }
    }

    
    
    private void DecreaseBallCount()
    {
        Level.ballCount--;
    }

    
    
    /* BallScore Functions*/
    private void IncreaseBallScore()
    {
        ballScore++;
    }

    
    
    /* Score Functions*/
    private void IncreaseScore()
    {
        score += streak * 1;
    }

    
    
    /* Streak Functions*/
    private void IncreaseStreak()
    {
        streak++;
    }
    
    
    
    private void ResetStreak()
    {
        streak = 0;
    }

    #endregion
    
    
    
    #region Common Game Manager Functions
        public void ShowMainMenu()
        {
            GameState = GameState.Menu;
            UserInterfaceManager.Instance.ShowMainMenuPanel();
        }
        
        
        
        public void StartGame()
        {
            GameState = GameState.Playing;
            UserInterfaceManager.Instance.ShowGamePanel();
        }

        
        
        public void FinishGame()
        {
            GameState = GameState.Menu;
            UserInterfaceManager.Instance.ShowRestartMenuPanel(ballScore >= Level.requiredBall);
        }
        
        
        
        public void RestartGame()
        {
            ResetLevel();
            StartGame();
        }


        
        // Function to pause the game
        public void PauseGame()
        {
            GameState = GameState.Paused;
            
        }
        
        
        
        private void LoadLevel()
        {
            if (currentLevelItems)
            {
                Destroy(currentLevelItems.gameObject);
            }
            Level = Instantiate(LevelSoPrefab);
            currentLevelItems = Instantiate(Level.levelItemsPrefab);
            SpawnNewBall();
        }



        private void ResetLevel()
        {
            streak = 0;
            score = 0;
            ballScore = 0;
            LoadLevel();
        }
    #endregion
    
}
