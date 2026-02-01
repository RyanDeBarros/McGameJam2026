using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PatrolBehavior : StateMachineBehaviour
{
    public float hearingRange = 10.0f;
    public float minVolume = 0.1f;


    public int tresholdIndex = 4;
    public float sightRadius = 5f;
    [Range(0, 360)]
    public float sightAngle = 100f;
    public LayerMask targetMask; //set layers in scene
    public LayerMask obstructionMask; //set layers in scene

    private List<GameObject> navPoints;
    private GameObject player;
    private NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = animator.GetComponent<NavMeshAgent>();
        navPoints = GameObject.FindGameObjectsWithTag("navPoint").ToList();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navPoints.Sort((nav1, nav2) => {
            float dist1 = (player.transform.position - nav1.transform.position).sqrMagnitude;
            float dist2 = (player.transform.position - nav2.transform.position).sqrMagnitude;
            return dist1.CompareTo(dist2);
        });
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(navPoints[UnityEngine.Random.Range(0, math.clamp(tresholdIndex, 0, navPoints.Count()))].transform.position);
        }

        if (isPlayerSeen())
        {
            animator.SetBool("isPlayerSeen", true);
        } else if (HeardSound()) {
            animator.SetBool("isPlayerHeard", true);
        }
        
    }
    //This functions is used in three diff behavior scripts. for now keep it like that.
    private bool isPlayerSeen()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(agent.transform.position, sightRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //only possible to get a player, using first index
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

    public bool HeardSound()
    {
        foreach (ObstacleSound source in FindObjectsByType<ObstacleSound>(FindObjectsSortMode.None))
        {
            if (!source.GetAudioSource().isPlaying) continue;

            float distance = Vector3.Distance(agent.transform.position, source.transform.position);
            float t = Mathf.InverseLerp(source.GetAudioSource().maxDistance, source.GetAudioSource().minDistance, distance);
            float perceivedVolume = source.GetAudioSource().volume * Mathf.Clamp01(t);

            if (distance <= hearingRange && perceivedVolume >= minVolume)
            {
                return true;
            }
        }

        return false;
    }
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

