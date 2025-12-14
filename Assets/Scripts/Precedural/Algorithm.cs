using System.Collections.Generic;
using UnityEngine;

public static class Algorithm
{
    public static HashSet<Vector3Int> SimpleRandomWalk(Vector3Int startPos, int walkLength)
    {
        HashSet<Vector3Int> paths = new();

        paths.Add(startPos);
        var prevPos = startPos;

        for (int i = 0; i < walkLength; i++)
        {
            var newPos = prevPos + Direction3D.GetRandomDir();
            paths.Add(newPos);
            prevPos = newPos;
        }
        return paths;
    }
}



public static class Direction3D
{
    //¹æÇâ
    public static List<Vector3Int> cardinalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0,1), // Up
        new Vector3Int(1,0), // Right
        new Vector3Int(0,-1), // Down
        new Vector3Int(-1,0) // Left
    };

    public static Vector3Int GetRandomDir()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
