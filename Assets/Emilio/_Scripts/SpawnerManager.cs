using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private GameObject[] SpawnersDetected;
    //private GameObject[] Spawners;
    [SerializeField] GameObject Baby;
    
    private bool RandomValue; 
    private string Tag = "Spawner";
    private Transform SpawnTransform;

    public int Amount = 1;


    private void Awake()
    {
        //
        SpawnersDetected = GameObject.FindGameObjectsWithTag(Tag);
        Debug.Log("Found " + SpawnersDetected.Length);

        //Random value between 0 and the number of Spawners found 
        //RandomValue = Random.Range(0, SpawnersDetected.Length);

        for (int i = 0; i < Amount; i++)
        {
            int randomIndex = Random.Range(0, SpawnersDetected.Length);
            Transform spawnPoint = SpawnersDetected[randomIndex].transform;

            Instantiate(Baby, spawnPoint.position, spawnPoint.rotation);

            Debug.Log("Spawned Baby at spawner " + randomIndex);
        }

    }

}
