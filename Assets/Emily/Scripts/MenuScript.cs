using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;







public class MenuScript : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject creditsPanel;

    
    void RunExe()
    {
        string exePath = System.IO.Path.Combine(
        Application.streamingAssetsPath,
        "popup.exe");

    Process.Start(exePath);
    }

    public void PlayGame()
    {
        UnityEngine.Debug.Log("Play");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {

        #if UNITY_STANDALONE_WIN
           RunExe();
        #endif
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


