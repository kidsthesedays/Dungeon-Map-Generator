using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPos = FindWallsInDir(floorPos, Dir2D.cardinalDirList);
        foreach (var position in basicWallPos)
        {
            //tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDir(HashSet<Vector2Int> floorPos, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var position  in floorPos)
        {
            foreach (var dir in dirList)
            {
                var neighbourPos = position + dir;
                if (floorPos.Contains(neighbourPos) == false)
                    wallPos.Add(neighbourPos);
            }
            
        }

        return wallPos;
    }
}
