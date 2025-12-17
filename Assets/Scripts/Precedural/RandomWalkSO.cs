using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkSO")]
public class RandomWalkSO : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public int maxItemCount = 3;
    public bool startRandomlyEachIteration = true;
}
