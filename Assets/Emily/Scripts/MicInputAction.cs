using UnityEngine;
using UnityEngine.Rendering.Universal;
/**
attach this to player. adjust loudness threshold to desired minimum amount
**/
public class MicInputAction : MonoBehaviour
{
    public float loudness = 0f;
    private const int sampleWindow = 128;
    [SerializeField] private float loudnessThreshold = 0.05f;

    [SerializeField] private GameObject momAi;
    [SerializeField] private float momCooldown = 0.5f;
    private float lastSpawn;

    void Update()
    {
        loudness = GetMicLoudness();
        if (loudness > loudnessThreshold && Time.time > lastSpawn)
        {
            BringMomToPosition();
            lastSpawn = Time.time + momCooldown;
        }
    }

    float GetMicLoudness()
    {
        if (AmbienceManager.micClip == null) return 0;
        
        int micPosition = Microphone.GetPosition(AmbienceManager.micDevice) - sampleWindow;
        if (micPosition < 0) return 0;

        float[] samples = new float[sampleWindow];
        AmbienceManager.micClip.GetData(samples,micPosition);

        float sum = 0;
        for(int i=0;i<samples.Length;i++)
        {
            sum += samples[i] * samples[i];
        }

        return Mathf.Sqrt(sum / samples.Length);
    }

    void BringMomToPosition()
    {
        momAi.GetComponent<Animator>().SetTrigger("HeardTrigger");
    }

}
