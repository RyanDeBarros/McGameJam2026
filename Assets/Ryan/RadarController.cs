using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RadarController : MonoBehaviour
{
    [SerializeField] private List<float> momDistanceThresholds = new() { 5f, 10f, 20f, 30f, 40f, 50f, 75f, 100f };
    private int currentThresholdIndex = -1;
    [SerializeField] private float babyDistanceThreshold = 20f;
    private readonly HashSet<Transform> nearbyBabies = new();
    [SerializeField] private RectTransform mapFog;
    [SerializeField] private MinimapManager miniMap;

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

        player = GameObject.FindWithTag("Player").transform;
        mom = GameObject.FindWithTag("Mom").transform;
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
            {
                NotificationManager.NotifyMomDistance(momDistanceThresholds[currentThresholdIndex]);
                // TODO play mom voice line
            }
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
                    nearbyBabies.Add(baby.transform);
                    NotificationManager.NotifyBabyNearby();
                    // TODO start playing baby voice lines continuously
                }
            }
            else
            {
                if (nearbyBabies.Contains(baby.transform))
                {
                    nearbyBabies.Remove(baby.transform);
                    // TODO stop playing baby voice lines
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
