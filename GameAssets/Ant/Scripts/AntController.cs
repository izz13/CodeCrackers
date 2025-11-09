using System;
using UnityEngine;
using UnityEngine.AI;

public class AntController : MonoBehaviour
{
    public enum AntStates
    {
        Idle,
        Follow,

    };


    public AntStates currentState;

    public bool selected = false;


    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Animator antAnimator;

    [SerializeField, Range(1f, 20f)]
    float speed = 10f;

    [SerializeField]
    GameObject selectionIndicator;

    public Vector3 followPoint;
    public LayerMask antLayerMask;

    private CameraControlsInput cameraControlsInput;

    Vector3 velocity;
    Vector3 direction;
    Vector3 displacement;

    NavMeshAgent agent;

    int idleHash = Animator.StringToHash("Idle");
    int runHash = Animator.StringToHash("Run");

    float followStopDistance = 2.5f;



    private void Awake()
    {
        currentState = AntStates.Idle;
        followPoint = Vector3.zero;
        cameraControlsInput = GetComponent<CameraControlsInput>();
        agent = GetComponent<NavMeshAgent>();

    }


    private void Update()
    {
        switch (currentState)
        {
            case AntStates.Idle:
                idleUpdate();
                break;
            case AntStates.Follow:
                followUpdate();
                break;
            default:
                idleUpdate();
                break;
        }
        if (selected)
        {
            selectionIndicator.SetActive(true);
        }
        else
        {
            selectionIndicator.SetActive(false);
        }


    }

    private void followUpdate()
    {

        if (Vector3.Distance(transform.position,agent.destination) < followStopDistance)
        {
            agent.isStopped = true;
            antAnimator.CrossFade(idleHash, 0.1f);
            currentState = AntStates.Idle;
        }
        else
        {
            currentState = AntStates.Follow;
        }


    }

    private void idleUpdate()
    {

        velocity = Vector3.zero;
        currentState = AntStates.Idle;
    }

    public void setFollowPoint(Vector3 point)
    {

        agent.isStopped = false;
        agent.destination = point;
        antAnimator.CrossFade(runHash, 0.1f);
        currentState = AntStates.Follow;

    }


}
