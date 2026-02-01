using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : StateMachineBehaviour
{
    public float jumpScareDistance = 10.0f;
    public float sightRadius = 5f;
    public float chasingSpeed = 2f;
    [Range(0, 360)]
    public float sightAngle = 100f;
    public LayerMask targetMask; //set layers in scene
    public LayerMask obstructionMask; //set layers in scene

    private GameObject player;
    private NavMeshAgent agent;
    private Vector3 lastPositionPlayer;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = chasingSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lastPositionPlayer = player.transform.position;
        if (isPlayerSeen())
        {
            agent.SetDestination(lastPositionPlayer); 
            
            if (Vector3.Distance(agent.transform.position, lastPositionPlayer) <= jumpScareDistance) {
                animator.SetTrigger("JumpScareTrigger");
            }
        }else {
            animator.SetBool("isPlayerSeen", false);
        }
    }
    private bool isPlayerSeen()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(agent.transform.position, sightRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //only possible to get a player
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - agent.transform.position).normalized;

            if (Vector3.Angle(agent.transform.forward, directionToTarget) < sightAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);

                if (!Physics.Raycast(agent.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    return true;
            }
        }

        return false;
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
