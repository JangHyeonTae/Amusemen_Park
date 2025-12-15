using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddCorridor : SampleMap
{
    [SerializeField] private int corridorLength = 14;
    [SerializeField] private int corridorCount = 5;

    [SerializeField][Range(0.1f, 1f)] private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorGeneration();
    }

    private void CorridorGeneration()
    {
        HashSet<Vector3Int> floorsPos = new HashSet<Vector3Int>();
        HashSet<Vector3Int> potentialRoomsPos = new HashSet<Vector3Int>();

        CreateCorridors(floorsPos, potentialRoomsPos);

        HashSet<Vector3Int> roomsPos = CreateRooms(potentialRoomsPos);

        floorsPos.UnionWith(roomsPos);

        mapVisualizer.OrderFloor(floorsPos);
        WallGeneration.CreateWall(floorsPos, mapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector3Int> floorsPos, HashSet<Vector3Int> potentialRoomsPos)
    {
        var currentPos = startPos;
        potentialRoomsPos.Add(currentPos);
        List<List<Vector3Int>> corridors = new();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = Algorithm.RandomWalkCorridor(currentPos,corridorLength);
            corridors.Add(corridor);
            currentPos = corridor[corridor.Count - 1];
            potentialRoomsPos.Add(currentPos);
            floorsPos.UnionWith(corridor);
        }
    }

    private HashSet<Vector3Int> CreateRooms(HashSet<Vector3Int> potentialRoomsPos)
    {
        HashSet<Vector3Int> roomsPos = new HashSet<Vector3Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomsPos.Count * roomPercent);

        List<Vector3Int> roomsToCreate = potentialRoomsPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach(var roomPos in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(so, roomPos);
            roomsPos.UnionWith(roomFloor);
        }

        return roomsPos;
    }
}
