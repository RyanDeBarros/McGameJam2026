using UnityEngine;
using UnityEngine.SceneManagement;

/*Attach this to the player
  Rigidbody and collider are needed (message just in case)
*/
public class CollectorManager : MonoBehaviour
{

    public int BabyCount;
    private int BabySpawned;
  
    void Start()
    {
        BabyCount = 0;
    }

    private void OnTriggerEnter(Collider baby)
    {
       if (baby.CompareTag("Baby"))
        {
            //Debug.Log("Trigger entered by: " + baby.gameObject.name);
            BabyCount++;
            NotificationManager.NotifyBabyCollection(BabySpawned-BabyCount);
            //Debug.Log("Babies "+ BabyCount);
            baby.enabled = false; 
            Destroy(baby.gameObject);
        }
    }

    private void Update()
    {
        BabySpawned = SpawnerManager.Amount;
        if (BabyCount == BabySpawned)
        {
            NotificationManager.NotifyBabyCompletion();
            SceneManager.LoadScene("EndScene");
        }
        

    }

}
