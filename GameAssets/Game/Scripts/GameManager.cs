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
            gameUI.rootVisualElement.Q<Label>("Health").text = "Health: " + moundManager.queenAnt.health.ToString();
            gameUI.rootVisualElement.Q<Label>("Hunger").text = "Hunger: " + moundManager.queenAnt.hunger.ToString();
            gameUI.rootVisualElement.Q<Label>("Level").text = "Level: " + moundManager.queenAnt.level.ToString();

        }
        else if (antManager.ants.Count > 0)
        {
            AntController firstAnt = antManager.ants[0];
            //1.Do the same thing you did for queen
            //2.But for firstAnt instead
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = firstAnt.name;
        }
        else
        {
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = "";
            gameUI.rootVisualElement.Q<Label>("Health").text = "";
            gameUI.rootVisualElement.Q<Label>("Hunger").text = "";
            gameUI.rootVisualElement.Q<Label>("Level").text = "";
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
                antManager.ants.Remove(ant);
            }
        }
        foreach (AntController ant in selectedAnts)
        {
            antManager.addAnt(ant);
        }

    }
        

}
