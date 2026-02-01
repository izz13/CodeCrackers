using System;
using UnityEngine;
using UnityEngine.AI;

public class AntController : MonoBehaviour
{
    public enum AntStates
    {
        Idle,
        Follow,
        Gather,

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

    GameObject currentResource ;


    int idleHash = Animator.StringToHash("Idle");
    int runHash = Animator.StringToHash("Run");
    int eatHash = Animator.StringToHash("Eat");

    float followStopDistance = 2.5f;

    public string name = "Ant";
    public int health = 100;
    public int level = 1;
    private float gatherTimer = 0f;
    public float gatherCooldown =5f;




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
            case AntStates.Gather:
                gatherUpdate();
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

    private void gatherUpdate()
    {
        // if (gatherTimer < gatherCooldown)
        // {
        //     gatherTimer+=Time.deltaTime;
        // }
        // else
        // {
        //     gatherTimer=0;
        //     setAnimation(AntStates.Idle);
        //     currentState=AntStates.Idle;
    //    }
    }
//Check if the ant is not at the resource
// If it is not move the ant to the resource using the navmesh agent (similar to the follow state update)
// If it is at the resource, change the ant animation to eat and start the gatherTimer
// Once the gatherTimer reaches zero change the animation and state to idle

    public void setAnimation(AntStates newAnimation)
    {
        switch (newAnimation)
        {
            case AntStates.Idle:
                antAnimator.CrossFade(idleHash, 0.1f);
                break;
            case AntStates.Follow:
                antAnimator.CrossFade(runHash, 0.1f);
                break;
            case AntStates.Gather:
                antAnimator.CrossFade(eatHash, 0.1f);
                break;
            default:
                antAnimator.CrossFade(idleHash, 0.1f);
                break;
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
    public void setGatherState(Vector3 pos, GameObject resource)
    {
        if (resource == currentResource)
        {
            return;
        }
        currentState=AntStates.Gather;
        currentResource=resource;

    }


}
