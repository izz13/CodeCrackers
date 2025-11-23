using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Mono.Cecil.Cil;
using System;
using Unity.VisualScripting;

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
    bool leftClick;

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
            if (antManager.ants.Count > 0)
            {
                if (input.LeftMouseClicked)
                {
                    leftClick = true;
                }
                if (input.RightMouseReleased)
                {
                    leftClick = false;
                }
                //Check if the mouse has been clicked
                //Call the move update function from ant manager 
                //ex. antManager.moveUpdate(mousePos)
            }
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
        findAnts();
    }

    void gameUI_Update()
    {
        

        if (selectedGameObjects.Contains(moundManager.gameObject) && antManager.ants.Count == 0)
        {
            //Debug.Log("Mound in list");
            //1. Make another label in the ui document for the Unit Stats
            //2. Set the the text of the label you created with the stats of the queen ant
            //3. to get the stats use moundMananger, for example if I wanted the queen health
            //example : moundManager.queenAnt.health;
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = moundManager.queenAnt.name;
            gameUI.rootVisualElement.Q<Label>("Label0").text = "Health: " + moundManager.queenAnt.health.ToString();
            gameUI.rootVisualElement.Q<Label>("Label1").text = "Hunger: " + moundManager.queenAnt.hunger.ToString();
            gameUI.rootVisualElement.Q<Label>("Label2").text = "Level: " + moundManager.queenAnt.level.ToString();
            gameUI.rootVisualElement.Q<Label>("Label3").text = "";

        }
        else if (antManager.ants.Count > 0)
        {
            AntController firstAnt = antManager.ants[0];
            //1.Do the same thing you did for queen
            //2.But for firstAnt instead
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = firstAnt.name;
            gameUI.rootVisualElement.Q<Label>("Label0").text = antManager.ants.Count.ToString();
            gameUI.rootVisualElement.Q<Label>("Label1").text = "Health: "+firstAnt.health.ToString();
            gameUI.rootVisualElement.Q<Label>("Label2").text = "Level: " + firstAnt.level.ToString();
            gameUI.rootVisualElement.Q<Label>("Label3").text = "";
        }
        else
        {
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = "";
            gameUI.rootVisualElement.Q<Label>("Label0").text = "";
            gameUI.rootVisualElement.Q<Label>("Label1").text = "";
            gameUI.rootVisualElement.Q<Label>("Label2").text = "";
            gameUI.rootVisualElement.Q<Label>("Label3").text = "";
        }
        // if (!selectedGameObjects.Contains(moundManager.gameObject) && selectedGameObjects.Contains(antManager.gameObject))
        // {
        //     gameUI.rootVisualElement.Q<Label>("Unit_Title").text = moundManager.queenAnt.name;
        //     gameUI.rootVisualElement.Q<Label>("Health").text = "Health: " + moundManager.queenAnt.health.ToString();
        //     gameUI.rootVisualElement.Q<Label>("Hunger").text = "Hunger: " + moundManager.queenAnt.hunger.ToString();
        //     gameUI.rootVisualElement.Q<Label>("Level").text = "Level: " + moundManager.queenAnt.level.ToString();
        // }
    }
    
    void findAnts()
    {
        List<AntController> selectedAnts = new List<AntController>();
        List<AntController> removedAnts = new List<AntController>();
        foreach (GameObject gameObject in selectedGameObjects)
        {
            AntController selectedAnt = gameObject.GetComponent<AntController>();
            if (selectedAnt != null)
            {
                selectedAnts.Add(selectedAnt);
            }
        }

        foreach (AntController ant in antManager.ants)
        {
            if (!selectedAnts.Contains(ant))
            {
                removedAnts.Add(ant);
            }
        }
        foreach (AntController ant in selectedAnts)
        {
            antManager.addAnt(ant);
        }
        foreach (AntController ant in removedAnts)
        {   
            antManager.ants.Remove(ant);
        }

    }
        

}
