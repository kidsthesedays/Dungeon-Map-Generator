using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms 
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength) // Creates a path the first position, in a direction
    //and stops when the length has been reached.
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); //Collection that stores unique values.

        path.Add(startPos);
        var prevPos = startPos;

        for (int i = 0; i < walkLength; i++) //iterate through the walk length index. Then Add a direction. The end point is the new position.
        {
            var newPos = prevPos + Dir2D.getRanCardinalDir();
            path.Add(newPos);
            prevPos = newPos; //the new position becomes the previouis position to keep the loop going from there. 
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

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) //Bounds is bounding to axis of specific values
                                                                                                               // also called AABB for axis-aligned bounding box.
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>(); //Queue rooms that is available for split
        List<BoundsInt> roomsList = new List<BoundsInt>();// take room to split.
        roomsQueue.Enqueue(spaceToSplit); // first in first out. A data structure that automatically add and removes in the queue
                                          // if the conditions for split is correct
        while (roomsQueue.Count > 0)//This allows the algorithm to split till there is no more space.
        {
            var room = roomsQueue.Dequeue(); //Take a room out to split it.
            if (room.size.y >= minHeight && room.size.x >= minWidth) //Check if the room meets the minimum size condition.
                                                                     //This Space can contain a room or be split into multiple rooms
            {
                if (Random.value < 0.5f) //Choose a random way to split the value. it is based on 0 or 1. so if it is less than 0.5.
                                         //it goes and tries to split horizontally first.
                                         
                {
                    if (room.size.y >= minHeight * 2) //checks if the room can be split horizontally.
                    {
                        splitHorizontally(minWidth, roomsQueue, room);
                    }else if (room.size.x >= minWidth * 2) // checks if the room can be split vertically.
                    {
                        splitVertically(minHeight, roomsQueue, room);
                    }else //if it cant be split it is added to the rooms list as a final room
                    {
                        roomsList.Add(room);//also it doesn't get considered to split in the queue.
                    }
                }
                else //split vertically first.
                {
                    if (room.size.x >= minWidth * 2) 
                    {
                        splitVertically(minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        splitHorizontally(minWidth, roomsQueue, room);
                    }else
                    {
                        roomsList.Add(room);
                    }
                    
                }
            }
                
        }

        return roomsList; 
    }

    private static void splitVertically(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room) 
    {
        var xSplit = Random.Range(1, room.size.x); //Chooses a new starting position randomly.
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z)); //Defines the first room and uses xSplit as the max Width.
                                                                                                     
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));//the starting position is the width of the room subtracted
                                                                            //with the width of the first room. Creating two rooms for the queue. Split Vertically.
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);

    }

    private static void splitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)//the same as the vertical split just turned.
    {
        var ySplit = Random.Range(1, room.size.y); //(minHeight, room.size.y - minHeight) can also be used but its not random. will always split in the middle.
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.y));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);

    }
}


public static class Dir2D 
{
    public static List<Vector2Int> cardinalDirList = new List<Vector2Int>
    {
        //Directions
        new Vector2Int(0, 1), //up
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN  
        new Vector2Int(-1, 0) //LEFT
    };

    public static Vector2Int getRanCardinalDir() //get a random direction.
    {
        
        return cardinalDirList[Random.Range(0, cardinalDirList.Count)];
    }
}
