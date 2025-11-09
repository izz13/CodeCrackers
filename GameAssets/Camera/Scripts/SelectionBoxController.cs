using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class SelectionBoxController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    LayerMask selectableLayer;

    [SerializeField]
    CameraControlsInput cameraControlsInput;

    [SerializeField]
    Transform selectionBoxPrefab;

    Vector3 startPosition;
    public Rect selectionRect { get; private set; }
    public bool isSelecting { get; private set; }

    Transform selectionBox;

    public BoxCollider selectionCollider;


    public List<GameObject> selectedGameObjects;



    void UpdateSelectionBox(Vector3 currentMousePosition)
    {

        float width = currentMousePosition.x - startPosition.x;
        float height = currentMousePosition.z - startPosition.z;

        selectionRect = new Rect(
            Mathf.Min(startPosition.x, currentMousePosition.x),
            Mathf.Min(startPosition.z, currentMousePosition.z),
            Mathf.Abs(width),
            Mathf.Abs(height)
        );
        selectionBox.position = new Vector3(startPosition.x + width / 2, 0f, startPosition.z + height / 2);
        selectionBox.localScale = new Vector3(width, 1f, height);
        selectionCollider.size = new Vector3(Mathf.Abs(width), 1f, Mathf.Abs(height));
        selectionCollider.center = selectionBox.position;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isSelecting)
        {
            float width = getMousePosOnGround().x - startPosition.x;
            float height = getMousePosOnGround().z - startPosition.z;

            //Gizmos.DrawCube(new Vector3(startPosition.x + width/2,startPosition.y + 2f,startPosition.z + height/2),new Vector3(width,1f,height));

        }
    }


    public void SelectionBoxUpdate(bool isActive)
    {
        if (isActive && !isSelecting)
        {
            selectedGameObjects.Clear();
            startPosition = getMousePosOnGround();
            selectionBox.gameObject.SetActive(true);
            isSelecting = true;
        }
        if (isSelecting)
        {
            UpdateSelectionBox(getMousePosOnGround());
        }
        if (!isActive && isSelecting)
        {
            selectionBox.gameObject.SetActive(false);
            isSelecting = false;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (!selectedGameObjects.Contains(other.gameObject) && isSelecting)
        {
            selectedGameObjects.Add(other.gameObject);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (selectedGameObjects.Contains(other.gameObject) && isSelecting)
        {
            selectedGameObjects.Remove(other.gameObject);
        }

    }

    Vector3 getMousePosOnGround()
    {
        RaycastHit hit;
        Vector2 mousePos = cameraControlsInput.MousePos;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Vector3 mousePosOnGround = Vector3.zero;
        if (Physics.Raycast(ray, out hit, 10000, selectableLayer))
        {

            Vector3 followPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 displacement = followPoint - transform.position;
            mousePosOnGround = followPoint;

        }
        return mousePosOnGround;
    }

    void Start()
    {
        selectionBox = Instantiate(selectionBoxPrefab, this.transform);
        selectionBox.gameObject.SetActive(false);
        selectedGameObjects = new List<GameObject>();
        
    }


}
