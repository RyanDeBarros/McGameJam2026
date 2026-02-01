using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    private AudioSource soundSource;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player enter Collision with Branck");
            soundSource.Play();
        }
    }

    public AudioSource GetAudioSource()
    {
        return soundSource;
    }
}
