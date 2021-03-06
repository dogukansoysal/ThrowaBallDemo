﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class SoLevel : ScriptableObject
{
    // Ball Count for the level
    [Range(1,10)]
    public int ballCount;
    
    // Required ball count to pass the level.
    [Range(1,10)]
    public int requiredBall;
    
    // Level scene items for playable game.
    public Transform levelItemsPrefab;
}
