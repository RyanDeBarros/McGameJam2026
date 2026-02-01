using UnityEngine;
using UnityEngine.AI;

public class StunnedBehavior : StateMachineBehaviour
{
    public float stunTime = 5f;

    private float time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0f;
        animator.SetBool("isPlayerSeen", false);
        animator.SetBool("isPlayerHeard", false);
        animator.SetBool("isPlayerVanished", false);
        animator.SetBool("JumpScareTrigger", false);
        animator.SetBool("isInvestigating", false);
        NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
        agent.ResetPath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
