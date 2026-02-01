using UnityEngine;
using UnityEngine.Assertions;

public class JumpScareAudio : MonoBehaviour
{
    [SerializeField] private AudioSource kissingAudio;
    [SerializeField] private AudioSource eatingAudio;

    private void Awake()
    {
        Assert.IsNotNull(kissingAudio);
        Assert.IsNotNull(eatingAudio);
    }

    public void PlayAudio()
    {
        kissingAudio.Play();
        eatingAudio.Play();
    }
}
