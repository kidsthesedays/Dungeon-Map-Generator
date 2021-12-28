using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid grid;
    
    private void Start()
    {
        grid = new Grid(4, 2,10f,new Vector3(20,0));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(),56);
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}

