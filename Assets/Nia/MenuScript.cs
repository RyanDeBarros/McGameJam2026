using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;







public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject creditsPanel;

    
    void RunExe()
    {
        string exePath = System.IO.Path.Combine(
        Application.dataPath,
        "../Assets/Nia/Popup 4th Wall/popup.exe");

    Process.Start(exePath);
    }

    public void PlayGame()
    {
        UnityEngine.Debug.Log("Play");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        RunExe();
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


