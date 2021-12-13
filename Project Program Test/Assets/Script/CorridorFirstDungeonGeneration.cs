using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class CorridorFirstDungeonGeneration : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLenghth = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;



    protected override void runProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions, potentialRoomPos);
        
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPos);
        floorPositions.UnionWith(roomPositions);
        tilemapVisualizer.paintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);
        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPos in roomToCreate)
        {
            var roomFloor = runRandomWalk(randomWalkParameters, roomPos);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }
    
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPos)
    {
        var currentPosition = startPos;
        potentialRoomPos.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLenghth);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPos.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }

}
