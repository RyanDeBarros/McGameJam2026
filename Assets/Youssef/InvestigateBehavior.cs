using UnityEngine;
using UnityEngine.AI;

public class InvestigateBehavior : StateMachineBehaviour
{
    public float investigationThreshold = 4.5f;
    public float investigationTime = 4.0f;
    public float rotationRate = 1f;
    private GameObject player;
    private NavMeshAgent agent;
    private Vector3 lastPositionPlayer;
    private float time;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("MC");
        lastPositionPlayer = player.transform.position;
        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(agent.transform.position, lastPositionPlayer) <= investigationThreshold)
        {
            time += Time.deltaTime;
            animator.transform.Rotate(0.0f, Time.deltaTime * rotationRate, 0.0f);
            if (time >= investigationTime) {
                animator.SetBool("isPlayerVanished", true);
            }
        }
        else {
            agent.SetDestination(lastPositionPlayer);
        }
        //if player seen
        // animator.SetBool("isPlayerSeen", true);
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
