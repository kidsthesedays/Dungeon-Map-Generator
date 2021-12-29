using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridRanWalk : MonoBehaviour
{
    [SerializeField] 
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;


    public void runProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = runRandomWalk();
        foreach (var position in floorPos)
        {
          Debug.Log(position);  
        }
    }
    
    protected HashSet<Vector2Int> runRandomWalk() //Takes in parameters that are universal to run the Algorithm.
    {
        var currentPos = startPosition;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>(); 
        for (int i = 0; i < iterations; i++) //Takes number of iterations from the parameters to.
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, walkLength);
            floorPos.UnionWith(path); //add the floorpositions to the path. Adds the randomwalk to the existsing tiles from simpleRandomWalk.
            if (startRandomlyEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count)); // Uses Linq to query the HashSet Collection since there is no index.
                //Then Takes a random floor to iterate from.
            }
        }

        return floorPos;
    }
}
