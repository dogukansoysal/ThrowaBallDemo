using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class BallController : MonoBehaviour
{
    public static BallController Instance;    // Singleton definition

    private GameManager _gm;    // Game Manager Instance reference 
    private InputManager _im;    // Input Manager Instance reference 
    public BallState BallState;    // Current Ball State.
    public Transform Ball;        // Current ball in the scene.
    
    public Transform BallPrefab;    // Prefab of ball

    // Awake function from MonoBehaviour.
    private void Awake()
    {
        if (!Instance)    // Determine if instance is null
            Instance = this;    // Assign instance
        else if(Instance != this)    // Determine if instance already assigned
            Destroy(gameObject);    // If we have that already, we don't need another one.
    }
    
    
    // Start function from MonoBehaviour.
    private void Start()
    {
        _gm = GameManager.Instance;
        _im = InputManager.Instance;
    }

    // Update function from MonoBehaviour.
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

    
    /// <summary>
    /// Initialize a ball and set reference to class variable.
    /// </summary>
    public void InitBall()
    {
        // If there is a ball on variable, kill it.
        if(Ball)
            KillBall();
        Ball = Instantiate(BallPrefab, transform);
        BallState = BallState.Ready;
    }
    
    
    /// <summary>
    /// Dragging the ball Mechanic.
    /// </summary>
    /// <param name="direction"> Direction of the drag</param>
    /// <param name="distance"> Distance of the drag</param>
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
    
    
    /// <summary>
    /// Throwing ball with the drag direction, distance and throw force (from GameConstants).
    /// </summary>
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
    
    
    /// <summary>
    /// Kill ball with a time delay (from GameConstants) and make reference as null.
    /// </summary>
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
