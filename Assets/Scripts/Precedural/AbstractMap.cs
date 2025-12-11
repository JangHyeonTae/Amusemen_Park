using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMap : MonoBehaviour
{
    [SerializeField] protected MapVisualizer mapVisualizer = null;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateMap()
    {
        mapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
