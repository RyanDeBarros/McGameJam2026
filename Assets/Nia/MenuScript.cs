using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class RunExeOnExit : MonoBehaviour
{
void OnApplicationQuit()
{
#if UNITY_EDITOR
    UnityEditor.EditorApplication.quitting += RunExe;
#else
    RunExe();
#endif
}

void RunExe()
{

    string exePath = System.IO.Path.Combine(
    Application.dataPath,
    "../Assets/Nia/Popup 4th Wall/popup.exe");

Process.Start(exePath);

}
}

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject creditsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        
        
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
