using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator //Inherites to use randomwalk on rooms.
{
    [SerializeField]
    private int mineCorridorLenghth = 14, mineCount = 5;
    [SerializeField] //Base Parameters for generation.
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20; //the size of the entire space that is being split.
    [SerializeField]
    [Range(0,10)]
    private int offset = 1; //offset based from the bounds of each room. how far should the rooms be away from each other as a minimum.
    [SerializeField]
    private bool randomWalkRooms = false; // use or not use the algorithm.


    protected override void runProceduralGeneration()
    {
       
        CreateRooms();
        GenerateMine();
        
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int) startPos,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)),minRoomWidth,minRoomHeight); //Create rooms with the parameters given.



        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }



        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center)); //Find the center of rooms.
        }

        HashSet<Vector2Int> corridors = connectRooms(roomCenters);
        floor.UnionWith(corridors);//to put tiles

        tilemapVisualizer.paintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++) //iterate throught all rooms create.
        {
            var roomBounds = roomsList[i]; // make sure it holds the bounds.
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y)); //calculate the center point.
            var roomFloor= runRandomWalk(randomWalkParameters,roomCenter); //use randomWalk from the center.
            foreach (var position in roomFloor) //from the position from the room floor.
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) //Walk randomly based on the offset.
                                                             && position.y >= (roomBounds.yMin + offset)
                                                             && position.y <= (roomBounds.yMax - offset));
                {
                    floor.Add(position); //add the randomWalked positions to the floor for the tiles to be placed with it.
                }
            }
        }

        return floor;
    }

    private HashSet<Vector2Int> connectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];//pick a random center.
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)//it is above 0 if a center is picked.
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters); //find the closest next room.
            roomCenters.Remove(closest); //take it out
            HashSet<Vector2Int> newCorridor = createCorridor(currentRoomCenter, closest); //make a new corridor.
            currentRoomCenter = closest; //then work from that center to find the next closest.
            corridors.UnionWith(newCorridor); // add the corridor that was created.
        }

        return corridors;
    }

    private HashSet<Vector2Int> createCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter; //position of the center.
        corridor.Add(position);
        while (position.y != destination.y) //run through the possible positions in the corridor. stop when they have the same position
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);

        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters) //findt the closest point to the next center that has been found.
        {
            float currentDist = Vector2.Distance(position, currentRoomCenter);
            if (currentDist < distance)
            {
                distance = currentDist;
                closest = position;

            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList) //iterate through each point in the rooms and add a tile.
        {
            for (int col = offset; col < room.size.x - offset; col++) //based on the offset have the columns and rows be the room sizes.
            {
                for (int row = offset; row < room.size.y-offset; row++)
                {
                    Vector2Int position = (Vector2Int) room.min + new Vector2Int(col, row); //make the position equal to the columns and rows.
                    floor.Add(position); //create floor.
                }
            }
        }
        return floor;
    }
      public void GenerateMine()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();
        CreateMine(floorPositions, potentialRoomPos);

        tilemapVisualizer.paintFloorTiles(floorPositions);
    }

    public void CreateMine(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPos)

    {
        
        Vector2Int currentPosition = FindRandomEdge();
       

        potentialRoomPos.Add(currentPosition);
        for (int i = 0; i < mineCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, mineCount);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPos.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }


    }
    public Vector2Int FindRandomEdge ()
    {
        var collision = true;
        var whichSide = Random.Range(0, 3);
        var x=0;
        var y=0;

        if (whichSide == 0)
        {
           x=1;
           y= Random.Range(1,dungeonHeight);
            //Vector2Int leftEdge = new Vector2Int (1, Random.Range(1,dungeonHeight));  
            while(collision)
            {

                x += 1;
                
                if(x>40)
                {
                    collision=false;
                }
            }   
        } 
        else if (whichSide == 1)
        {
            x=dungeonWidth;
            y=Random.Range(1,dungeonHeight);

            //Vector2Int rightEdge = new Vector2Int (dungeonWidth, Random.Range(1,dungeonHeight));    
        } 
        else if (whichSide == 2)
        {
            x=Random.Range(1,dungeonWidth);
            y=dungeonHeight;
            //Vector2Int topEdge = new Vector2Int (Random.Range(1,dungeonWidth), dungeonHeight);
        } 
        else if (whichSide == 3)
        {
            x=Random.Range(1,dungeonWidth);
            y=1;
            //Vector2Int buttomEdge = new Vector2Int (Random.Range(1,dungeonWidth), 1);    
        }
       
           // HashSet<Vector2Int> edges = new HashSet<Vector2Int>();
            Vector2Int currentPosition = new Vector2Int(x,y);

            return currentPosition;
    
    }
    public void FindTiles()
    {
        
    }    






}
