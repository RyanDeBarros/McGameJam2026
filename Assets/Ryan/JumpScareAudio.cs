using UnityEngine;
using UnityEngine.Assertions;

public class JumpScareAudio : MonoBehaviour
{
    [SerializeField] private new AudioSource audio;

    private void Awake()
    {
        Assert.IsNotNull(audio);
    }

    public void PlayAudio()
    {
        audio.Play();
    }
}
