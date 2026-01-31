using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
/**
ATTACH THIS TO DOOR TRIGGER
**/
public class DoorTrigger : MonoBehaviour
{
    [Header("Scene change stuff")]
    [SerializeField] private string sceneName;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private GameObject blackScreen;

    [Header("Door trigger stuff")]
    [SerializeField] private GameObject text;
    private bool isInTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && isInTrigger)
        {
            StartCoroutine(FadeAndLeave());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
            isInTrigger = false;
        }
    }

    private IEnumerator FadeAndLeave()
    {
        text.SetActive(false);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(duration);

       // ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
