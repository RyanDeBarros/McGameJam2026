using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject horseScreen;

    public void Play()
    {
        horseScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Credits()
    {
        //TODO
    }

    public void Exit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }


}
