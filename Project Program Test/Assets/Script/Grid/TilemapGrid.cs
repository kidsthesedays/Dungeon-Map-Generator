using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class TilemapGrid
{
    private Grid<TilemapObject> grid;

    public TilemapGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<TilemapObject>(width, height, cellSize, originPosition, (Grid<TilemapObject>g,int x,int y) => new TilemapObject(grid, x, y));
        
    }
    
    public class TilemapObject
    {
        public enum TilemapSprite
        {
            None,
            Tile
        }

        private Grid<TilemapObject> grid;
        private int x;
        private int y;
        private TilemapSprite tilemapSprite;

        public TilemapObject(Grid<TilemapObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            
        }

        public void SetTilemapSprite(TilemapSprite tilemapSprite)
        {
            this.tilemapSprite = tilemapSprite;
            grid.TriggerGridObjectChanged(x,y);
        }

        public override string ToString()
        {
            return tilemapSprite.ToString();
        }
    }
    
}*/
