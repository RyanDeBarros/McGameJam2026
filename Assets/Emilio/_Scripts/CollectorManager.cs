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
            baby.enabled = false;
            if (BabyCount == BabySpawned)
            {
                NotificationManager.NotifyBabyCompletion();
                SceneManager.LoadScene("EndScene");
            }
            else
            {
                NotificationManager.NotifyBabyCollection(BabySpawned - BabyCount);
            }

            //Debug.Log("Babies "+ BabyCount);
            Destroy(baby.gameObject);
        }
    }

    private void Update()
    {
        BabySpawned = SpawnerManager.Amount;
    }

}
