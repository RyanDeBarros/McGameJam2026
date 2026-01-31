using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RadarController : MonoBehaviour
{
    // TODO UI mask to cover map 'RADIUS' amount away from player

    [SerializeField] private List<float> momDistanceThresholds = new() { 5f, 10f, 20f, 30f, 40f, 50f, 75f, 100f };
    private int currentThresholdIndex = -1;

    private Transform player;
    private Transform mom;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        Assert.IsNotNull(player);
        mom = GameObject.FindWithTag("Mom").transform;
        Assert.IsNotNull(mom);
    }

    private void Update()
    {
        float momDistance = Vector3.Distance(player.position, mom.position);
        int newThresholdIndex = momDistanceThresholds.FindIndex(t => momDistance <= t); 

        if (newThresholdIndex != currentThresholdIndex)
        {
            currentThresholdIndex = newThresholdIndex;
            if (currentThresholdIndex >= 0)
            {
                NotificationManager.NotifyMomDistance(momDistance);
                // TODO play SFX
            }
        }
    }
}
