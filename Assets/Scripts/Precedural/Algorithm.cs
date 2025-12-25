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

    public static List<Vector3Int> RandomWalkCorridor(Vector3Int startPos, int corridorLength)
    {
        List<Vector3Int> corridors = new();
        var dir = Direction3D.GetRandomDir();
        var currentPos = startPos;
        corridors.Add(currentPos);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPos += dir;
            corridors.Add(currentPos);
        }

        return corridors;
    }

    public static List<BoundsInt> BSP(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new();

        roomsQueue.Enqueue(spaceToSplit);

        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y > minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontal(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVerical(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVerical(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontal(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList;
    }


    private static void SplitVerical(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontal(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

}



public static class Direction3D
{
    // 위,아래,좌,우 (현재 방향에서 검사할 대상 및 벽 검사)
    public static List<Vector3Int> cardinalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0,1), // Up
        new Vector3Int(1,0), // Right
        new Vector3Int(0,-1), // Down
        new Vector3Int(-1,0) // Left
    };

    // 벽 검사시 위 + 우, 위 + 좌 ~~~ 아래 + 좌, 아래 + 우 검사하고 코너 벽 생성 위함
    public static List<Vector3Int> diagonalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(1,1),
        new Vector3Int(1,-1),
        new Vector3Int(-1,1),
        new Vector3Int(-1,-1)
    };

    // 여러방면 확인위한 메서드
    public static List<Vector3Int> eightDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0,1),
        new Vector3Int(1,1),
        new Vector3Int(1,0),
        new Vector3Int(1,-1),
        new Vector3Int(0,-1),
        new Vector3Int(-1,-1),
        new Vector3Int(-1,0),
        new Vector3Int(-1,1)
    };

    public static List<Vector3Int> itemDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0,0,2), 
        new Vector3Int(2,0,0), 
        new Vector3Int(0,0,-2),
        new Vector3Int(-2,0,0),
        new Vector3Int(2,0,2),
        new Vector3Int(2,0,-2),
        new Vector3Int(-2,0,-2),
        new Vector3Int(-2,0,2)
    };

    public static Vector3Int GetRandomDir()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
