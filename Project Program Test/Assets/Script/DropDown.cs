using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{

    // the SO Data 
    public SimpleRandomWalkData bigDungeon;
    public SimpleRandomWalkData island;


    public SimpleRandomWalkDungeonGenerator srw;
    //room first
    //corridor


    public void Start()
    {
        srw = GameObject.Find("SimpleRandomWalkDungeonGenerator").GetComponent<SimpleRandomWalkDungeonGenerator>();
    }


    public void HandleInput(int optionVal) {

        if(optionVal == 0)
        {
            srw.randomWalkParameters = bigDungeon;
        }
        if(optionVal == 1)
        {
            srw.randomWalkParameters = island;
        }

    }

}
