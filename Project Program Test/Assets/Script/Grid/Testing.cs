using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Testing : MonoBehaviour
{
    [SerializeField]
    //private BoolVisualizer boolVisual;
    private Grid<MapGridObject> grid;
    [SerializeField]
    private int width,height;

    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition;

    private void Start()
    {
        //TilemapGrid tilemapGrid = new TilemapGrid(20, 10, 10f, Vector3.zero);
        grid = new Grid<MapGridObject>(width, height,cellSize, originPosition/*,(Grid<MapGridObject>g,int x,int y) => new MapGridObject(g,x,y)*/);
        
        //boolVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            /*MapGridObject mapGridObject = grid.GetGridObject(position);
            if (mapGridObject!=null)
            {
                mapGridObject.AddValue(5);
            }*/
        }

        
    }
}

public class MapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private Grid<MapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public MapGridObject(Grid<MapGridObject> grid, int x,int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x,y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

