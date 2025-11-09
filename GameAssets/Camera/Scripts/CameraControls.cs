using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _ground;

    private CameraControlsInput cameraControlsInput;

    private float cameraHeight = 25f;

    private float cameraSpeed = 15f;

    private void Awake()
    {
        cameraControlsInput = GetComponent<CameraControlsInput>();
    }

    private void Start()
    {
        resetCameraPos();
    }

    private void Update()
    {
        Vector3 movementDirection = new Vector3(cameraControlsInput.CameraMovementInput.x, 0f, cameraControlsInput.CameraMovementInput.y);
        movementDirection.Normalize();
        Vector3 cameraPosition = _playerCamera.transform.position;
        Vector3 cameraDisplacement = movementDirection * cameraSpeed * Time.deltaTime;
        //This is where you should check if the camera's x & y position are going to leave the ground
        //If it is the make cameraPosition's x & y values bound to the ground

        //Get the scale of the ground and save it in a Vector3
        Vector3 groundScale = _ground.transform.localScale * 10f;
        float rightEdge = groundScale.x / 2f;
        float leftEdge = -groundScale.x / 2f;
        float topEdge = groundScale.z / 2f;
        float bottomEdge = -groundScale.z / 2f;

        if (cameraPosition.x + cameraDisplacement.x > rightEdge)
        {
            cameraPosition.x = rightEdge;
            cameraDisplacement.x = 0f;
        }
        else if(cameraPosition.x + cameraDisplacement.x < leftEdge)
        {
            cameraPosition.x = leftEdge;
            cameraDisplacement.x = 0f;
        }
        else
        {
            cameraPosition.x += cameraDisplacement.x;
        }
        if (cameraPosition.z + cameraDisplacement.x > topEdge)
        {
            cameraPosition.z = topEdge;
            cameraDisplacement.z = 0f;
        }
        else if(cameraPosition.z + cameraDisplacement.z < bottomEdge)
        {
            cameraPosition.z = bottomEdge;
            cameraDisplacement.z = 0f;
        }
        else
        {
            cameraPosition.z += cameraDisplacement.z;
        }


        _playerCamera.transform.position = cameraPosition;
    }

    private void resetCameraPos()
    {
        //1. Move the camera's position so that it is equal to the ground's center position
        //2. Move the camera up by some height
        //3. Rotate the camera around the x-axis by 90 degrees
        _playerCamera.transform.position = _ground.transform.position;
        _playerCamera.transform.position += Vector3.up * cameraHeight;
        _playerCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    //HW: 
    //Create an Ant Prefab inside of Unity
    //Make sure that it is a capsule object
    //When you press play there should be an Ant Object in the middle of the ground
    //Have the ant always face toward where the mouse is on the screen(optional)
}
