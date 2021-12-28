using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleRandomWalkDungeonGeneratorButton : SimpleRandomWalkDungeonGenerator {
    public Button yourButton3;

    void Start () {
        Button btn = yourButton3.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        tilemapVisualizer.Clear();
        runProceduralGeneration();
        Debug.Log ("You have clicked the button!");
    }

   
}