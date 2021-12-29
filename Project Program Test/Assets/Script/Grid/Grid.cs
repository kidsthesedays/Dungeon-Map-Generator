using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.UIElements;

public class Grid<TGridObject> {
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition/*, Func<Grid<TGridObject>,int,int,TGridObject> createGridObject*/)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        //debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //gridArray[x, y] = createGridObject(this,x,y);
            }
            
        }

        bool showDebug = true;

        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null,
                        getWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white,
                        TextAnchor.MiddleCenter);
                    Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x + 1, y), Color.white, 100f);
                }
            }

            Debug.DrawLine(getWorldPosition(0, height), getWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(getWorldPosition(width, 0), getWorldPosition(width, height), Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    
    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 getWorldPosition(int x, int y) {
            return new Vector3(x, y) * cellSize + originPosition;
    }

    public void getXY(Vector3 worldPosition, out int x, out int y) {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject Value) {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = Value;
                if (OnGridObjectChanged !=null) { OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});}
            }

    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});
    }

    public void TriggerGridObjectChanged()
    {
        
    }

    /*public void SetGridObject(Vector3 worldPosition, TGridObject Value) {
            int x, y;
            getXY(worldPosition, out x, out y);
            SetGridObject(x, y, Value);
    }

    public TGridObject GetGridObject(int x, int y) {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default(TGridObject);
            }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
            int x, y;
            getXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }*/

}
