using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    public InputState InputState;
    public Vector3 FirstTouchPosition;
    public Vector3 LastTouchPosition;


    private Camera cam;
    private void Awake()
    {
        if (!Instance)    // Determine if instance is null
            Instance = this;    // Assign instance
        else if(Instance != this)    // Determine if instance already assigned
            Destroy(gameObject);    // If we have that already, we don't need another one.
    }
    
    
    
    private void Start()
    {
        InputState = InputState.None;
        cam = Camera.main;
    }

    
    
    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (InputState == InputState.None || InputState == InputState.Released)
                {
                    InputState = InputState.FirstTouch;
                    SetFirstTouchPosition();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (InputState == InputState.FirstTouch || InputState == InputState.Hold)
                {
                    InputState = InputState.Released;
                    SetLastTouchPosition();
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (InputState == InputState.FirstTouch || InputState == InputState.Hold)
                {
                    InputState = InputState.Hold;
                    SetLastTouchPosition();
                }
            }
            else
            {
                ResetTouchPositions();
            }
        }
    }
    
    
    
    /// <summary>
    /// First touch position setter.
    /// </summary>
    private void SetFirstTouchPosition()
    {
        FirstTouchPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
    
    
    
    /// <summary>
    /// Last touch position setter.
    /// </summary>
    private void SetLastTouchPosition()
    {
        LastTouchPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
        
    
    
    /// <summary>
    /// Touch position reset.
    /// </summary>
    private void ResetTouchPositions()
    {
        FirstTouchPosition = Vector3.zero;
        LastTouchPosition = Vector3.zero;
    }
}
