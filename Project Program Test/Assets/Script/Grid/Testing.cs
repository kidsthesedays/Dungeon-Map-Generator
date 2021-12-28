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
        grid = new Grid(4, 2,10f);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(),56);
        }
    }
}

