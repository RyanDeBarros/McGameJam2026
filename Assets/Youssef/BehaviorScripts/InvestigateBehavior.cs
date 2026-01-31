using UnityEngine;
using UnityEngine.AI;

public class InvestigateBehavior : StateMachineBehaviour
{
    public float investigationThreshold = 4.5f;
    public float investigationTime = 4.0f;
    public float rotationRate = 10f;
    public float sightRadius = 5f;
    [Range(0, 360)]
    public float sightAngle = 100f;
    public LayerMask targetMask; //set layers in scene
    public LayerMask obstructionMask; //set layers in scene

    private GameObject player;
    private NavMeshAgent agent;
    private Vector3 lastPositionPlayer;
    private float time;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = GameObject.FindGameObjectWithTag("MC");
        agent = animator.GetComponent<NavMeshAgent>();
        lastPositionPlayer = player.transform.position;
        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            time += Time.deltaTime;
            //animator.transform.Rotate(0.0f, Time.deltaTime * rotationRate, 0.0f);
            if (time > investigationTime) {
            animator.SetBool("isInvestigating", false); //reseting the investigation
            animator.SetBool("isPlayerVanished", true);
            return;
            }
        if (isPlayerSeen())
        {
            animator.SetBool("isInvestigating", false); //reseting the investigation
            animator.SetBool("isPlayerSeen", true);
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
                {
                    return true;
                }
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
