using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap/*,wallTilemap*/; //makes tilemaps selectable in unity
    [SerializeField]
    private TileBase floorTile /*,wallTop*/; //makes tiles selectable.

    public void paintFloorTiles(IEnumerable<Vector2Int> floorPos) //IEnumberable is a Generic which makes the tiles loopable.
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) //Take in Tiles and its properties.
    {
        List<Vector2Int> iTile = new List<Vector2Int>();
        foreach (var position in positions)
        {
            paintSingleTile(tilemap,tile,position); //Assign the selected tile texture to the tilemap from given position and map.
            iTile.Add(position);
        }

        for (int i = 0; i < iTile.Count; i++)
        {
            Debug.Log("Position: " + iTile[i] + " Index: " + i);
        }
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) //Assign the Texture the the referenced tile. 
    {
        var tilePos = tilemap.WorldToCell((Vector3Int) position); // Take the position of the tile. World position is the position in the program
        // and make it comparable to the position of the tile.
        tilemap.SetTile(tilePos, tile);
    }
    /*public void PaintSingleBasicWall(Vector2Int position)
    {
        paintSingleTile(wallTilemap,wallTop,position);
    }*/

    public void Clear() // makes sure it generates a new generated map.
    {
        floorTilemap.ClearAllTiles();
        //wallTilemap.ClearAllTiles();
    }

    
}
