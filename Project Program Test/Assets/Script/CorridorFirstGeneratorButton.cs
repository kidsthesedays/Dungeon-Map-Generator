using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CorridorFirstGeneratorButton : CorridorFirstDungeonGeneration {
    public Button yourButton2;

    void Start () {
        Button btn = yourButton2.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        tilemapVisualizer.Clear();
        runProceduralGeneration();
        Debug.Log ("You have clicked the button!");
    }

   
}