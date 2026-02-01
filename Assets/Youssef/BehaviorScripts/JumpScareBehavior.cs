using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class JumpScareBehavior : StateMachineBehaviour
{
    public float jumpScareDuration = 2f;

    private GameObject player;
    private NavMeshAgent agent;
    private float time;
    private GameObject camera;
    private GameObject arms;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("JumpScareTrigger", false);
        time = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        Transform camPoint = animator.transform.Find("JumpscareCameraPoint");
        player.transform.position = camPoint.position;
        player.transform.rotation = camPoint.rotation;
        player.GetComponent<Rigidbody>().useGravity = false;
        animator.GetComponent<JumpScareAudio>().PlayAudio();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        arms = GameObject.FindGameObjectWithTag("Arms");
        camera.transform.localPosition = new Vector3(0, 0, 1.31f);
        arms.transform.localPosition = new Vector3(
            arms.transform.localPosition.x,
            arms.transform.localPosition.y,
            -1.5f
        );
        time += Time.deltaTime;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("Player");
        Transform camPoint = animator.transform.Find("JumpscareCameraPoint");
        player.transform.position = camPoint.position;
        player.transform.rotation = camPoint.rotation;
        FirstPersonController.disable = true;
        if (time > jumpScareDuration)
        {
            camera.transform.localPosition = new Vector3(0, 0, 0);
            arms.transform.localPosition = new Vector3(
                arms.transform.localPosition.x,
                arms.transform.localPosition.y,
                0
            );
            player.GetComponent<Rigidbody>().useGravity = true;
            FirstPersonController.disable = false;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        arms = GameObject.FindGameObjectWithTag("Arms");
        camera.transform.localPosition = new Vector3(0, 0, 0);
        arms.transform.localPosition = new Vector3(
            arms.transform.localPosition.x,
            arms.transform.localPosition.y,
            0
        );
        player.GetComponent<Rigidbody>().useGravity = true;
        FirstPersonController.disable = false;
    }

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
