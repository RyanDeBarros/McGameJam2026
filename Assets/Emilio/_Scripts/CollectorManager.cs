using UnityEngine;

/*Attach this to the player
  Rigidbody and collider are needed (message just in case)
*/
public class CollectorManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int BabyCount;
    //private bool IsCounted;

    void Start()
    {
        BabyCount = 0;
    }

    private void OnTriggerEnter(Collider baby)
    {
       if (baby.CompareTag("Baby"))
        {
            Debug.Log("Trigger entered by: " + baby.gameObject.name);
            BabyCount++;
            NotificationManager.NotifyBabyCollection(BabyCount);
            Debug.Log("Babies "+ BabyCount);
            baby.enabled = false; 
            Destroy(baby.gameObject);
        }
    }
}
