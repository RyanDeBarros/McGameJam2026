using UnityEngine;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{

    void NextScene()
    {
        //  SceneManager.LoadScene(__REPLACE__);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("NextScene", 15.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
