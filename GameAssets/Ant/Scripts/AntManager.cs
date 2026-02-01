using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class AntManager : MonoBehaviour
{
    private CameraControlsInput cameraControlsInput;

    public List<AntController> ants;

    AntController[] allAnts;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    LayerMask antLayerMask, uiLayerMask;

    [SerializeField]
    UIDocument uIDocument;

    VisualElement root;

    SelectionBoxController selectionBoxController;

    Button selectButton;

    Button moveButton;

    Button attackButton;

    Vector3 targetPoint;


    public enum AntManagerStates
    {
        Select,
        Move,
        Attack,
    }

    public AntManagerStates currentState;

    private void Awake()
    {
        cameraControlsInput = GetComponent<CameraControlsInput>();
        ants = new List<AntController>();
        currentState = AntManagerStates.Select;
        ants.Clear();

    }

    void Start()
    {
        allAnts = FindObjectsByType<AntController>(FindObjectsSortMode.None);
        // Debug.Log(allAnts.Length);

        // foreach (AntController ant in allAnts)
        // {
        //     addAnt(ants, ant);
        // }

        // selectButton = root.Q<Button>("select");
        // selectButton.clicked += OnSelectButtonClicked;
        // moveButton = root.Q<Button>("move");
        // moveButton.clicked += OnMoveButtonClicked;
        // attackButton = root.Q<Button>("attack");
        // attackButton.clicked += OnAttackButtonClicked;
    }

    void OnSelectButtonClicked()
    {
        currentState = AntManagerStates.Select;
    }

    void OnAttackButtonClicked()
    {
        
    }

    void OnMoveButtonClicked()
    {
        currentState = AntManagerStates.Move;
    }


    // void addAnt(List<AntController> ants, AntController ant)
    // {
    //     ants.Add(ant);
    // }

    public void addAnt(List<AntController> ants, AntController ant)
    {
        if (!ants.Contains(ant))
        {
            ants.Add(ant);
        }
        
    }

    public void addAnt(AntController ant)
    {
        if (!ants.Contains(ant))
        {
            ants.Add(ant);
        }
        
    }

    void removeAnt(List<AntController> ants, AntController ant)
    {
        ants.Remove(ant);
    }

    void Update()
    {
        // MouseOverUI();
        // switch (currentState)
        // {
        //     case AntManagerStates.Select:
        //         selectUpdate();
        //         break;
        //     case AntManagerStates.Move:
        //         moveUpdate();
        //         break;
        //     case AntManagerStates.Attack:
        //         // TODO: Add logic for Attack state
        //         break;
        //     default:
        //         break;
        // }
    }

    private void selectUpdate()
    {
        //selectionBoxController.SelectionBoxUpdate();
        if (selectionBoxController.isSelecting && !MouseOverUI())
        {
            foreach (AntController ant in allAnts)
            {
                Vector2 pos = new Vector2(ant.transform.position.x, ant.transform.position.z);
                if (selectionBoxController.selectionRect.Contains(pos) && !ants.Contains(ant))
                {
                    ant.selected = true;
                    addAnt(ants, ant);
                }
                if (!selectionBoxController.selectionRect.Contains(pos) && ants.Contains(ant))
                {
                    ant.selected = false;
                    removeAnt(ants, ant);
                }
            }
        }
       
    }

    public void moveUpdate(Vector3 pos)
    {
        foreach (AntController ant in ants)
        {
            ant.setFollowPoint(pos);
        }
    }
    public void gatherUpdate(Vector3 pos, GameObject resource)
    {
        foreach (AntController ant in ants)
        {
            ant.setGatherState(pos, resource);
        }
    }

    Vector3 getFollowPoint(Vector2 movePos)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(movePos);
        if (Physics.Raycast(ray, out hit, 10000, antLayerMask))
        {

            Vector3 followPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            return followPoint;


        }
        return Vector3.zero;
    }

    bool MouseOverUI()
    {
        Vector2 mousePostion = cameraControlsInput.MousePos;
        mousePostion.y = Screen.height - mousePostion.y;
        IPanel panel = root.panel;
        Vector2 panelMousePos = RuntimePanelUtils.ScreenToPanel(panel, mousePostion);
        //Debug.Log(panel.Pick(panelMousePos));
        VisualElement mouseOverElement = panel.Pick(panelMousePos);
        if (mouseOverElement != null)
        {
            if (mouseOverElement.name != "BaseElement")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }




}
