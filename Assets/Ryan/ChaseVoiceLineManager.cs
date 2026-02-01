using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ChaseVoiceLineManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> chaseVoiceLines;
    [SerializeField] private float voiceLineRefresh = 10f;

    private AudioSource audioSource;
    private Coroutine chaseVoiceLineRefresh;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource);
    }

    public void StartChasing()
    {
        IEnumerator ChaseVoiceLineRefresh()
        {
            audioSource.clip = chaseVoiceLines.GetRandomElement();
            audioSource.Play();
            yield return new WaitForSeconds(voiceLineRefresh);
            chaseVoiceLineRefresh = StartCoroutine(ChaseVoiceLineRefresh());
        }

        chaseVoiceLineRefresh = StartCoroutine(ChaseVoiceLineRefresh());

        // TODO repeatedly play mom voice lines when within certain distance of player.
    }

    public void StopChasing()
    {
        StopCoroutine(chaseVoiceLineRefresh);
    }
}
