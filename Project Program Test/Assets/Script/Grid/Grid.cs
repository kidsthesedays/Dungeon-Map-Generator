using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.UIElements;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, getWorldPosition(x,y)
                                                                             + new Vector3(cellSize, cellSize)*0.5f,30,Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(getWorldPosition(x,y),getWorldPosition(x,y +1),Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x,y),getWorldPosition(x+1,y),Color.white, 100f);
            }
        }
        Debug.DrawLine(getWorldPosition(0,height),getWorldPosition(width,height),Color.white, 100f);
        Debug.DrawLine(getWorldPosition(width,0),getWorldPosition(width,height),Color.white, 100f);
        
        SetValue(2,1,56);
    }   

    private Vector3 getWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    
    private void getXY(Vector3 worldPosition, out int x , out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int Value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = Value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
        
    }

    public void SetValue(Vector3 worldPosition, int Value)
    {
        int x, y;
        getXY(worldPosition, out x,out y);
        SetValue(x,y,Value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        getXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
    
}
