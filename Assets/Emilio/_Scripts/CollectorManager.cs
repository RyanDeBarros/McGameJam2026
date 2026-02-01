using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*Attach this to the player
  Rigidbody and collider are needed (message just in case)
*/

public class CollectorManager : MonoBehaviour
{
    public int BabyCount;
    private int BabySpawned;
    public static int Babies;

    [SerializeField] private float clearFogDuration = 1.5f;
    [SerializeField] private RectTransform fog;

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
                Babies = BabyCount;
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

    private void ChangeScale (int xval, int yval, RectTransform rectTransform)
    {
        Vector3 scale = rectTransform.localScale;
        scale.x = xval; // Set X scale to 2x
        scale.y = yval; // Set Y scale to 0.5x
        rectTransform.localScale = scale;
    }

}
