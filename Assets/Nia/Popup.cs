using UnityEngine;
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