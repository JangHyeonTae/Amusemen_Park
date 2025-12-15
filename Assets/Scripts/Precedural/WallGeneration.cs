using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGeneration
{
    public static void CreateWall(HashSet<Vector3Int> floorsPos, MapVisualizer visualizer)
    {
        var basicWallPos = FindWallsDirections(floorsPos, Direction3D.cardinalDirectionsList);
        var cornerWallPos = FindWallsDirections(floorsPos, Direction3D.diagonalDirectionsList);

        CreateBasicWalls(visualizer, basicWallPos, floorsPos);
        CreateCornerWalls(visualizer, cornerWallPos, floorsPos);
    }

    public static void CreateBasicWalls(MapVisualizer visualizer, HashSet<Vector3Int> basicWallPos, HashSet<Vector3Int> floorsPos)
    {
        foreach (var pos in basicWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var dir in Direction3D.cardinalDirectionsList)
            {
                var neighbourPos = pos + dir;
                if(floorsPos.Contains(neighbourPos))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }

            visualizer.InstSingleBasicWall(pos, neighboursBinaryType);
        }
    }

    public static void CreateCornerWalls(MapVisualizer mapVisualizer, HashSet<Vector3Int> cornerWallPos, HashSet<Vector3Int> floorsPos)
    {
        foreach (var pos in cornerWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var dir in Direction3D.eightDirectionsList)
            {
                var neightbourPos = pos + dir;
                if (floorsPos.Contains(neightbourPos))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            mapVisualizer.InstSingleCornerWall(pos, neighboursBinaryType);
        }
    }

    public static HashSet<Vector3Int> FindWallsDirections(HashSet<Vector3Int> floorsPos, List<Vector3Int> directionList)
    {
        HashSet<Vector3Int> wallPos = new HashSet<Vector3Int>();

        foreach (var pos in floorsPos)
        {
            foreach (var dir in directionList)
            {
                var neighborPos = pos + dir;

                if (!floorsPos.Contains(neighborPos))
                {
                    wallPos.Add(neighborPos);
                }
            }
        }

        return wallPos;
    }
}
