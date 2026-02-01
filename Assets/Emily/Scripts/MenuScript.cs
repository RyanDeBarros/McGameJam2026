using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.IO;







public class MenuScript : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject creditsPanel;

    
    void RunExe()
    {
        string exePath = System.IO.Path.Combine(
            Directory.GetParent(Application.dataPath).FullName,
            "popup","popup.exe"
        );

        var startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            UseShellExecute = false,
            WorkingDirectory = Path.GetDirectoryName(exePath)
        };

        Process.Start(startInfo);
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


