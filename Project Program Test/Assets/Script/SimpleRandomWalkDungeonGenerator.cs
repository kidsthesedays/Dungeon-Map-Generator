using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator //inherites from the abstract class.
{
    
    [SerializeField]
    public SimpleRandomWalkData randomWalkParameters; //Uses The parameters from the saved parameters files.
    

    


    protected override void runProceduralGeneration() //Method to run functions. Overides to use all algorithms.
    {
        HashSet<Vector2Int> floorPos = runRandomWalk(randomWalkParameters, startPos);
        tilemapVisualizer.Clear();
        tilemapVisualizer.paintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos,tilemapVisualizer);
        //attempting to index the hashset.
        //List<Vector2Int> iFloor = new List<Vector2Int>();
        /*foreach (var position in floorPos)
        { //shows position of all tiles as they are placed. good to follow the behavior of the code
            iFloor.Add(position);
        }

        for (int i = 0; i < iFloor.Count; i++)
        {
            Debug.Log("Position: "+iFloor[i]+" Index: "+i);
            
        }*/
        

    }

    protected HashSet<Vector2Int> runRandomWalk(SimpleRandomWalkData parameters, Vector2Int position) //Takes in parameters that are universal to run the Algorithm.
    {
        var currentPos = position;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>(); 
        for (int i = 0; i < parameters.iterations; i++) //Takes number of iterations from the parameters to.
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, parameters.walkLength);
            floorPos.UnionWith(path); //add the floorpositions to the path. Adds the randomwalk to the existsing tiles from simpleRandomWalk.
            if (parameters.startRandomlyEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count)); // Uses Linq to query the HashSet Collection since there is no index.
                //Then Takes a random floor to iterate from.
            }
        }

        return floorPos;
    }
}
