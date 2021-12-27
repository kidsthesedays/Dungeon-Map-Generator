using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFunctions : RoomFirstDungeonGenerator {
    public Button yourButton;

    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        tilemapVisualizer.Clear();
        runProceduralGeneration();
        Debug.Log ("You have clicked the button!");
    }

   
}