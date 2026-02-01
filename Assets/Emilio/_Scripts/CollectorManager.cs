using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;
using System.IO;

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

    private float fogX;
    private float fogY;
    [SerializeField] private float Xincrement = 0.01f;
    [SerializeField] private float Yincrement = 0.01f;

    void Start()
    {
        BabyCount = 0;
        fogX = fog.localScale.x;
        fogY = fog.localScale.y;
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
                QuitGame();
            }
            else
            {
                Babies = BabyCount;
                NotificationManager.NotifyBabyCollection(BabySpawned - BabyCount);
                fogX = fogX + 0.05f;
                fogY = fogY + 0.05f;
            }

            //Debug.Log("Babies "+ BabyCount);
            Destroy(baby.gameObject);
        }
    }

    private void Update()
    {
        BabySpawned = SpawnerManager.Amount;

        if(fogX >= 0.2 & fogY >= 0.2)
        {
            fogX = fogX - (Xincrement * Time.deltaTime);
            fogY = fogY - (Yincrement * Time.deltaTime);
        }
            
        ChangeScale(fogX, fogY, fog);

    }

    private void ChangeScale (float xval, float yval, RectTransform rectTransform)
    {
        Vector3 scale = rectTransform.localScale;
        scale.x = xval; // Set X scale to 2x
        scale.y = yval; // Set Y scale to 0.5x
        rectTransform.localScale = scale;
    }

    private IEnumerator ClearFogTemporarily(float duration)       
    {
        // Clear fog
        ChangeScale(1, 1, fog);

        yield return new WaitForSeconds(duration);

        // Restore original fog settings
        ChangeScale(fogX, fogY, fog);
    }
    void RunExe()
    {
        string exePath = System.IO.Path.Combine(
            Directory.GetParent(Application.dataPath).FullName,
            "Helper.exe"
        );

        Process.Start(exePath);
    }
    public void QuitGame()
    {

    #if UNITY_STANDALONE_WIN
        RunExe();
    #endif
        Application.Quit();


    }

}
