using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    
    [SerializeField]
    protected SimpleRandomWalkData randomWalkParameters;
    

    


    protected override void runProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = runRandomWalk(randomWalkParameters, startPos);
        tilemapVisualizer.Clear();
        tilemapVisualizer.paintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos,tilemapVisualizer); //wdawdawdawd 
    }

    protected HashSet<Vector2Int> runRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
    {
        var currentPos = position;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, parameters.walkLength);
            floorPos.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }

        return floorPos;
    }
}
