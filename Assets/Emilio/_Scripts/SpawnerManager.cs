using UnityEngine;
using System.Collections.Generic;

//Pls attach this script to any manager, and drag the baby prefab to the SerializeField

public class SpawnerManager : MonoBehaviour
{
    //private GameObject[] SpawnersDetected;
    private List<GameObject> SpawnersDetected;
    //private GameObject[] Spawners;
    [SerializeField] GameObject Baby;
    
    private bool RandomValue; 
    private string Tag = "Spawner";

    //public int Amount = 1;
    public static int Amount = 1;

    private void Awake()
    {
        //SpawnersDetected = GameObject.FindGameObjectsWithTag(Tag);
        SpawnersDetected  = new List<GameObject>( GameObject.FindGameObjectsWithTag(Tag));
        //Debug.Log("Found " + SpawnersDetected.Length);

        //Random value between 3 and the number of Spawners found 
        Amount = Random.Range(3, 6);

        Amount = Mathf.Min(Amount, SpawnersDetected.Count);

        for (int i = 0; i < Amount; i++)
        {
            int randomIndex = Random.Range(0, SpawnersDetected.Count);
            Transform spawnPoint = SpawnersDetected[randomIndex].transform;

            Instantiate(Baby, spawnPoint.position, spawnPoint.rotation);
            Destroy(SpawnersDetected[randomIndex].gameObject);
            SpawnersDetected.RemoveAt(randomIndex);

            Debug.Log("Spawned Baby at spawner " + randomIndex);
        }

    }
}
