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

        List<Vector2Int> deadEnds = findDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions);
        tilemapVisualizer.paintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position)==false) //if no room is at the position it is a dead end.
            {
                var room = runRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> findDeadEnds(HashSet<Vector2Int> floorPositions)  
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions) //if neighbour is equal to 0 from the tile position it means it stops there. 
        {
            int neighboursCount = 0;
            foreach (var direction in Dir2D.cardinalDirList) //loop through all directions from position
            {
                if (floorPositions.Contains(position + direction))//if a direction from the position is found. 
                {
                    neighboursCount++; //add it the list of neighbours
                    
                }
            }
            if (neighboursCount==1) // if there are only one neighbour it is a dead end. if there are more it is not.  
                deadEnds.Add(position);
        }

        return deadEnds;
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
