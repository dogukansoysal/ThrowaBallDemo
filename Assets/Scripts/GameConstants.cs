using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public static class GameConstants
{
    #region State Machines
    
    /// <summary>
    /// Game state keeps the current game state if it's playing, paused or in menu.
    /// </summary>
    public enum GameState
    {
        Playing,
        Paused,
        Menu
    }

    /// <summary>
    /// Input state keeps the current input type.
    /// </summary>
    public enum InputState
    {
        FirstTouch,
        Hold,
        Released,
        None
    }
    
    /// <summary>
    /// Ball State keeps the current Ball Controller's ball's state
    /// </summary>
    public enum BallState
    {
        Ready,
        Thrown
    }
        
    #endregion
    
    
    
    #region Variables

    public static float DragDistanceMin = 0.4f;      // Minimum required distance of drag
    public static float DragDistanceMax = 2f;        // Maximum required distance of drag
    
    public static float ThrowForce = 20f;            // Throw force of ball

    public static float BallDeathDuration = 3f;      // The duration after the ball will be destroyed when.

    public static int NumberOfTrajectoryPoints = 20;
    public static float TrajectoryTimeStep = 0.05f;

    
    
    /* Color Hex Codes Collected from Color Palette. */
    public static string GreenHexValue = "#45FF5F";
    public static string RedHexValue = "#FF4545";
    public static string YellowHexValue = "#F5FF3B";
    public static string GrayHexValue = "#B4B4B4";

    #endregion

    // Common static functions.
    #region Functions
    /// <summary>
    /// Exception handling for NaN (Not a Number)
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static bool Vector3IsNan(Vector3 vector)
    {
        // Not a Number check
        if(float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z))
            return true;
        else
        {
            return false;
        }
    }
    
    
    /// <summary>
    /// Returns the direction from first point to last point.
    /// </summary>
    /// <param name="firstPos"></param>
    /// <param name="lastPos"></param>
    /// <returns></returns>
    public static Vector3 CalculateDirection(Vector3 firstPos, Vector3 lastPos)
    {
        var heading = lastPos - firstPos;
        return heading / heading.magnitude; // Normalized direction
    }
    #endregion
    
    
}
