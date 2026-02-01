using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject horseScreen;
    [SerializeField] private GameObject credits;

    public void Play()
    {
        horseScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }


}
