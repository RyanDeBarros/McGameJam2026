using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmbienceIntensity
{
    Low = 1,
    Medium = 2,
    High = 3
}

public class AmbienceManager : MonoBehaviour
{
    private static AmbienceManager instance;

    private AudioSource s1;
    private AudioSource s2;
    private bool useS1 = true;

    [SerializeField] private List<AudioClip> intense1AudioClips;
    [SerializeField] private List<AudioClip> intense2AudioClips;
    [SerializeField] private List<AudioClip> intense3AudioClips;
    private AmbienceIntensity intensity = AmbienceIntensity.Low;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float switchTrackDurationMin = 20f;
    [SerializeField] private float switchTrackDurationMax = 60f;
    private float switchTrackDuration = 0f;

    private void Awake()
    {
        instance = this;
        s1 = gameObject.AddComponent<AudioSource>();
        s2 = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        Play(intensity);
        switchTrackDuration = Random.Range(switchTrackDurationMin, switchTrackDurationMax);
    }

    private void Update()
    {
        switchTrackDuration -= Time.deltaTime;
        if (switchTrackDuration <= 0f)
        {
            Play(intensity);

            switchTrackDuration = Random.Range(switchTrackDurationMin, switchTrackDurationMax);
        }
    }

    public void Play(AmbienceIntensity intensity)
    {
        if (this.intensity == intensity)
        {
            return;
        }
        PlayRandomClip(intensity switch
        {
            AmbienceIntensity.Low => intense1AudioClips,
            AmbienceIntensity.Medium => intense2AudioClips,
            AmbienceIntensity.High => intense3AudioClips,
            _ => throw new System.NotImplementedException()
        });

        this.intensity = intensity;
        switchTrackDuration = Random.Range(switchTrackDurationMin, switchTrackDurationMax);
    }

    private void PlayRandomClip(List<AudioClip> clips)
    {
        PlayClip(clips.GetRandomElement());
    }

    private void PlayClip(AudioClip clip)
    {
        IEnumerator FadeOut(AudioSource source)
        {
            source.volume = 1f;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Clamp01((1f - t) / fadeDuration);
                yield return null;
            }
            source.volume = 0f;
            source.Stop();
            source.clip = null;
        }

        IEnumerator FadeIn(AudioSource source)
        {
            source.Play();
            source.volume = 0f;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Clamp01(t / fadeDuration);
                yield return null;
            }
            source.volume = 1f;
        }

        if (Source2().clip)
            StartCoroutine(FadeOut(Source2()));

        Source1().clip = clip;
        if (Source1().clip)
            StartCoroutine(FadeIn(Source1()));

        ToggleSources();
    }

    private void ToggleSources()
    {
        useS1 = !useS1;
    }

    private AudioSource Source1()
    {
        return useS1 ? s1 : s2;
    }

    private AudioSource Source2()
    {
        return useS1 ? s2 : s1;
    }

    public static AmbienceManager GetInstance()
    {
        return instance;
    }
}
