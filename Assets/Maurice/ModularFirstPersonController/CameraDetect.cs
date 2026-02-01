using UnityEngine;
using UnityEngine.Audio;

public class CameraDetect : MonoBehaviour
{
    public Animator animator;
    public string Holding;
    public GameObject heldItem;
    ILookable currentLooked;
    GameObject currentLookedObject;
    private float speedTimer=0;
    public GameObject spray;
    public GameObject LSD;
    public GameObject Spray;
    public GameObject sprayPoint;
    public float grabDistance = 3f;
    public GameObject LSDprefab;
    public GameObject SprayPrefab;
    public AudioClip[] AudioClips;
    public AudioSource AudioSource;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("Finger");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("John");
        }
        if ( speedTimer > 0 ) 
            speedTimer -= Time.deltaTime;
        else
        {
            CameraEffectsController.distort = false;
            FirstPersonController.walkSpeed = 10;
        }    

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        ILookable newLooked = null;
        GameObject newLookedObject = null;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.gameObject.CompareTag("LSD") || hit.collider.gameObject.CompareTag("Spray"))
            {
                newLooked = hit.collider.GetComponent<ILookable>();
                newLookedObject = hit.collider.gameObject;
            }
        }

        if (newLooked != currentLooked)
        {
            if (currentLooked != null)
                currentLooked.OnLookExit();

            if (newLooked != null)
                newLooked.OnLookEnter();

            currentLooked = newLooked;
            currentLookedObject = newLookedObject;
        }

        // GRAB LOGIC
        if (Input.GetButtonDown("Fire2") && currentLookedObject != null && Holding!="LSD" && Holding != "Spray")
        {

            animator.SetTrigger("grab");
            Holding = currentLookedObject.tag;  // store tag
            Destroy(currentLookedObject.transform.root.gameObject);      // delete object
            animator.SetBool("Holding",true);
            heldItem.SetActive(true);
            if (Holding == "LSD") { LSD.SetActive(true); Spray.SetActive(false); }
            else { LSD.SetActive(false); Spray.SetActive(true); }
            currentLooked = null;
            currentLookedObject = null;
        }
        if (Input.GetButtonDown("Fire1")){
            if (Holding == null) return;
            if(Holding == "LSD")
            {
                AudioSource.resource = AudioClips[0];
                AudioSource.Play();
                animator.SetTrigger("Drinking");
                FirstPersonController.walkSpeed = 2;
            }
            if (Holding == "Spray")
            {
                AudioSource.clip = AudioClips[1];
                AudioSource.Play();
                Instantiate(spray, sprayPoint.transform.position, sprayPoint.transform.rotation, sprayPoint.transform);
                animator.SetTrigger("Spraying");
                FirstPersonController.walkSpeed = 2;
                animator.SetBool("Holding", false);
            }
        }
    }
    public void FinishDrinking()
    {
        speedTimer = 7;
        CameraEffectsController.distort = true;
        FirstPersonController.walkSpeed = 20;
        animator.SetBool("Holding", false);
        heldItem.SetActive(false);
        Holding = null;
        
    }
    public void SprayingDrinking()
    {
        animator.SetBool("Holding", false);
        heldItem.SetActive(false);
        Holding = null;

    }
    void DropItem()
    {
        if (Holding == null) return;

        GameObject prefabToSpawn = null;

        if (Holding == "LSD")
            prefabToSpawn = LSDprefab;
        else if (Holding == "Spray")
            prefabToSpawn = SprayPrefab;

        if (prefabToSpawn != null)
        {
            Vector3 spawnPosition = transform.position + transform.forward * 1.5f;
            spawnPosition.y = 21f; // slight lift

            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }

        // Reset holding state
        animator.SetBool("Holding", false);
        heldItem.SetActive(false);
        LSD.SetActive(false);
        Spray.SetActive(false);

        Holding = null;
    }


}
