using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public CameraDetect cameraDetect;

    public void OnDrinkFinished()
    {
        cameraDetect.FinishDrinking();
    }
    public void OnSprayFinished()
    {
        cameraDetect.SprayingDrinking();
    }

}
