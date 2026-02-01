using UnityEngine;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{

    void NextScene()
    {
        SceneManager.LoadScene("Mohammed_scene");
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
