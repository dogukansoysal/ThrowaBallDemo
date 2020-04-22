using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class TrajectorySimulation : MonoBehaviour
{
    public static TrajectorySimulation Instance;       // Singleton definition

    public Transform TrajectoryHolder;                // Parent of trajectory points
    public GameObject TrajectoryPointPrefeb;          // Prefab of trajectory points
    
    private List<GameObject> trajectoryPoints;        // List of all trajectory points
    
    // Initialization of singleton.
    private void Awake()
    
    {
        if (!Instance)    // Determine if instance is null
            Instance = this;    // Assign instance
        else if(Instance != this)    // Determine if instance already assigned
            Destroy(gameObject);    // If we have that already, we don't need another one.
    }
    
    // Start function from MonoBehaviour.
    void Start ()
    {
        trajectoryPoints = new List<GameObject>();
        // Initialize trajectories.
        for(int i=0;i<NumberOfTrajectoryPoints;i++)
        {
            var point = Instantiate(TrajectoryPointPrefeb, TrajectoryHolder);
            trajectoryPoints.Insert(i, point);
        }
    }

    
    /// <summary>
    /// displays projectile trajectory path. It takes two arguments, start position of object(ball) and initial velocity of object(ball).
    /// </summary>
    /// <param name="pStartPosition"></param>
    /// <param name="pVelocity"></param>
    private void SetTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity)
    {
        // Elapsed time counter
        var fTime = 0f;
        // Looping all points and move them to calculated position.
        for (var i = 0 ; i < NumberOfTrajectoryPoints ; i++)
        {
            fTime += TrajectoryTimeStep;
            trajectoryPoints[i].transform.position = CalculatePointPosition(pStartPosition, pVelocity, fTime);
        }
    }
    
    /// <summary>
    /// Calculate all trajectory points positions.
    /// </summary>
    /// <param name="pStartPosition">Initial position of the ball</param>
    /// <param name="pVelocity">Initial velocity of the ball</param>
    /// <param name="elapsedTime">elapsedTime since throw the ball.</param>
    /// <returns></returns>
    private Vector3 CalculatePointPosition(Vector3 pStartPosition , Vector3 pVelocity ,float elapsedTime){
        return Physics.gravity * (elapsedTime * elapsedTime * 0.5f) + pVelocity * elapsedTime + pStartPosition;
    }

    /// <summary>
    /// Show trajectory
    /// </summary>
    /// <param name="pStartPosition"></param>
    /// <param name="pVelocity"></param>
    public void ShowTrajectory(Vector3 pStartPosition , Vector3 pVelocity)
    {
        // NaN check for both of the vectors
        if(Vector3IsNan(pStartPosition) || Vector3IsNan(pVelocity))
            return;
        
        SetTrajectoryPoints(pStartPosition, pVelocity);
        TrajectoryHolder.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide trajectory
    /// </summary>
    public void HideTrajectory()
    {
        TrajectoryHolder.gameObject.SetActive(false);
    }
}
