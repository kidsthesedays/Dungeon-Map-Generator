using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms 
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var prevPos = startPos;

        for (int i = 0; i < walkLength; i++)
        {
            var newPos = prevPos + Dir2D.getRanCardinalDir();
            path.Add(newPos);
            prevPos = newPos;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLenghth)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Dir2D.getRanCardinalDir();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLenghth; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) //BSP Video
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit); // first in first out.
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        splitHorizontally(minWidth, minHeight, roomsQueue, room);
                    }else if (room.size.x >= minWidth * 2)
                    {
                        splitVertically(minWidth, minHeight, roomsQueue, room);
                    }else 
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        splitVertically(minWidth, minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        splitHorizontally(minWidth, minHeight, roomsQueue, room);
                    }else
                    {
                        roomsList.Add(room);
                    }
                    
                }
            }
                
        }

        return roomsList;
    }

    private static void splitVertically(int minWidth, int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }

    private static void splitHorizontally(int minWidth, int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }
}


public static class Dir2D
{
    public static List<Vector2Int> cardinalDirList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //up
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static Vector2Int getRanCardinalDir()
    {
        return cardinalDirList[Random.Range(0, cardinalDirList.Count)];
    }
}
