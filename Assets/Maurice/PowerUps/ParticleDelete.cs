using UnityEngine;

public class ParticleDelete : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    }

}
