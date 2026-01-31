using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject creditsPanel;

    public void PlayGame()
    {
        UnityEngine.Debug.Log("Play");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("quit");
        Application.Quit();
    }


    public void ShowCredits()
    {
         UnityEngine.Debug.Log("Credits");
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        UnityEngine.Debug.Log("Credits Closed");
        creditsPanel.SetActive(false);
    }
}
