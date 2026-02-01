using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class RadarController : MonoBehaviour
{
    [SerializeField] private RectTransform mapFog;
    [SerializeField] private MinimapManager miniMap;
    [SerializeField] private GameObject babyAudioSourceRoot;
    [SerializeField] private AudioClip babyCryingSFX;
    [SerializeField] private List<float> momDistanceThresholds = new() { 5f, 10f, 20f, 30f, 40f, 50f, 75f, 100f };
    private int currentThresholdIndex = -1;
    [SerializeField] private float babyDistanceThreshold = 20f;
    private readonly HashSet<Transform> nearbyBabies = new();

    private Transform player;
    private Transform mom;

    private float _radiusScale;
    public float RadiusScale
    {
        get => _radiusScale;
        set => SetRadiusScale(value);
    }

    private void Awake()
    {
        Assert.IsNotNull(mapFog);
        Assert.IsNotNull(miniMap);
        Assert.IsNotNull(babyAudioSourceRoot);
        Assert.IsNotNull(babyCryingSFX);

        player = GameObject.FindWithTag("Player").transform;
        mom = GameObject.FindWithTag("Mom").transform;
    }

    private void Start()
    {
        RadiusScale = 0.5f;
    }

    private void Update()
    {
        UpdateMomRadar();
        UpdateBabyRadar();
    }

    private void UpdateMomRadar()
    {
        float momDistance = Vector3.Distance(player.position, mom.position);
        int newThresholdIndex = momDistanceThresholds.FindIndex(t => momDistance <= t);

        if (newThresholdIndex != currentThresholdIndex)
        {
            currentThresholdIndex = newThresholdIndex;
            if (currentThresholdIndex >= 0)
                NotificationManager.NotifyMomDistance(momDistanceThresholds[currentThresholdIndex]);
        }

        mapFog.anchoredPosition = miniMap.GetPlayerPosition();
    }

    private void UpdateBabyRadar()
    {
        nearbyBabies.RemoveWhere(baby => baby == null);

        foreach (var baby in GameObject.FindGameObjectsWithTag("Baby"))
        {
            if (Vector3.Distance(baby.transform.position, player.position) <= babyDistanceThreshold)
            {
                if (!nearbyBabies.Contains(baby.transform))
                {
                    AudioSource source = baby.AddComponent<AudioSource>();
                    nearbyBabies.Add(baby.transform);
                    NotificationManager.NotifyBabyNearby();
                    source.clip = babyCryingSFX;
                    source.loop = true;
                    source.spatialBlend = 0.5f;
                    source.Play();
                }
            }
            else
            {
                if (nearbyBabies.Contains(baby.transform))
                {
                    AudioSource source = baby.GetComponent<AudioSource>();
                    source.Stop();
                    Destroy(source);
                    nearbyBabies.Remove(baby.transform);
                }
            }
        }
    }

    private void SetRadiusScale(float radiusScale)
    {
        _radiusScale = radiusScale;
        mapFog.localScale = new(radiusScale, radiusScale, 1f);
    }
}
