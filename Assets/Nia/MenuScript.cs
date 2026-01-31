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
    // Process.Start(@"C:\Path\To\Program.exe");
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
