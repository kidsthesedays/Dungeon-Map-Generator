using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    //private BoolVisualizer boolVisual;
    private Grid<bool> grid;

    private void Start()
    {
        grid = new Grid<bool>(16, 9,10f, Vector3.zero);
        
        //boolVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            grid.SetValue(position,true);
        }

        
    }
}

