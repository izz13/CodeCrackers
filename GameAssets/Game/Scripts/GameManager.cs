using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Mono.Cecil.Cil;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameState { Start, Game, End }

    [SerializeField]
    AntManager antManager;

    [SerializeField]
    MoundManager moundManager;

    [SerializeField]
    SelectionBoxController selectionBoxController;

    [SerializeField]
    CameraControlsInput input;

    [SerializeField]
    UIDocument gameUI;


    public GameState currentState;

    public List<GameObject> selectedGameObjects;

    bool rightClick;

    void Start()
    {
        currentState = GameState.Start;
        //selectedGameObjects = new List<GameObject>();
    }



    void Update()
    {
        if (currentState == GameState.Start)
        {
            selectUpdate();
            gameUI_Update();
        }
    }

    void selectUpdate()
    {
        if (input.RightMouseClicked)
        {
            rightClick = true;
        }
        else if (input.RightMouseReleased)
        {
            rightClick = false;
        }
        selectionBoxController.SelectionBoxUpdate(rightClick);
        selectedGameObjects = selectionBoxController.selectedGameObjects;
    }

    void gameUI_Update()
    {
        if (selectedGameObjects.Contains(moundManager.gameObject))
        {
            //Debug.Log("Mound in list");
            //1. Make another label in the ui document for the Unit Stats
            //2. Set the the text of the label you created with the stats of the queen ant
            //3. to get the stats use moundMananger, for example if I wanted the queen health
            //example : moundManager.queenAnt.health;
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = moundManager.queenAnt.name;
        }
        else
        {
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = "";
        }
    }
        

}
