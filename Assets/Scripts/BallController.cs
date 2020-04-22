using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class BallController : MonoBehaviour
{
    public static BallController Instance;

    private GameManager _gm;
    private InputManager _im;
    public BallState BallState;
    public Transform Ball;
    
    public Transform BallPrefab;    // Prefab of ball

    private void Awake()
    {
        if (!Instance)    // Determine if instance is null
            Instance = this;    // Assign instance
        else if(Instance != this)    // Determine if instance already assigned
            Destroy(gameObject);    // If we have that already, we don't need another one.
    }
    
    
    
    private void Start()
    {
        _gm = GameManager.Instance;
        _im = InputManager.Instance;
    }

    // Update is called once per frame.
    private void Update()
    {
        if (BallState == BallState.Thrown)
        {
            if(!Ball) return;
            
            if (Ball.localPosition.y < -10)
            {
                _gm.BallOutHole();
            }
        }
        
    }

    
    
    public void InitBall()
    {
        if(Ball)
            KillBall();
        Ball = Instantiate(BallPrefab, transform);
        BallState = BallState.Ready;
    }
    
    
    
    public void DragBall(Vector3 direction, float distance)
    {
        // Not a Number check
        if(Vector3IsNan(direction))
            return;

        // Correct if distance is larger than max distance.
        if (distance > DragDistanceMax)
        {
            distance = DragDistanceMax;
        }
        
        Ball.localPosition = direction * distance;
    }
    
    
    
    public void ThrowBall()
    {
        BallState = BallState.Thrown;
        var rb = Ball.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity =  -_gm.DragDirection * (_gm.DragDistance * ThrowForce);
    }


    
    /// <summary>
    /// Reset ball position.
    /// </summary>
    public void ResetBallPosition()
    {
        Ball.localPosition = Vector3.zero;
    }
    
    
    
    public void KillBall()
    {
        Destroy(Ball.gameObject, BallDeathDuration);
        Ball = null;
    }
    
    /*
    public void ReleaseBall()
    {
        Destroy(Ball);
    }
    */


    #region Coroutines
    
    

    #endregion
}
