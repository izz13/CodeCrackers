using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Mono.Cecil.Cil;

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
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = "Mound";
        }
        else
        {
            gameUI.rootVisualElement.Q<Label>("Unit_Title").text = "";
        }
    }
        

}
