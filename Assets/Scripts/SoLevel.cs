using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class SoLevel : ScriptableObject
{
    // Ball Count for the level
    [Range(1,10)]
    public int ballCount;
    
    [Range(1,10)]
    public int requiredBall;
    
    // 
    public Transform levelItemsPrefab;
}
