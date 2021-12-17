using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap/*,wallTilemap*/;
    [SerializeField]
    private TileBase floorTile /*,wallTop*/;

    public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            paintSingleTile(tilemap,tile,position);
        }
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int) position);
        tilemap.SetTile(tilePos, tile);
    }
    /*public void PaintSingleBasicWall(Vector2Int position)
    {
        paintSingleTile(wallTilemap,wallTop,position);
    }*/

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        //wallTilemap.ClearAllTiles();
    }

    
}
