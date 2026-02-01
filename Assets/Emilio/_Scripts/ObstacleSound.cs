using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    private AudioSource soundSource;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            soundSource.Play();
        }

    }

}
