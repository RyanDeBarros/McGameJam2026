using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private GameObject[] SpawnersDetected;
    private GameObject[] Spawners;
    [SerializeField] GameObject Baby;
    
    private bool RandomValue; 
    private string Tag = "Spawner";
    private Transform SpawnTransform;


    private void Awake()
    {
        //
        SpawnersDetected = GameObject.FindGameObjectsWithTag(Tag);
        Debug.Log("Found " + SpawnersDetected.Length);

        //Random value between 0 and the number of Spawners found 
        //RandomValue = Random.Range(0, SpawnersDetected.Length);

        for (int i = 0; i < SpawnersDetected.Length; i++)
        {
            RandomValue = Random.value < .5f;
            if (RandomValue)
            {
                Debug.Log("Spawner " + i + " active");
                Instantiate(Baby, SpawnersDetected[i].transform.position, SpawnersDetected[i].transform.rotation);

            }
            else
            {
                Debug.Log("Spawner " + i + " inactive");
            }
        }


    }

}
