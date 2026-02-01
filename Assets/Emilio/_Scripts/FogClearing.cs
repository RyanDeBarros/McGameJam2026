using UnityEngine;

public class FogClearing : MonoBehaviour
{
    [SerializeField] private float minDensity = 0.01f;
    [SerializeField] private float maxDensity = 0.05f;
    [SerializeField] private float speed = 1f;

    void Update()
    {
        RenderSettings.fogDensity = Mathf.Lerp(minDensity, maxDensity, Mathf.PingPong(Time.time * speed, 1f));
    }
}
