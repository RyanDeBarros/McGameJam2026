using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class MinimapManager : MonoBehaviour
{
    [Header("World bounds (XZ) mapped to the minimap")]
    [SerializeField] float minX = 0f;
    [SerializeField] float maxX = 150f;
    [SerializeField] float minZ = 0f;
    [SerializeField] float maxZ = 150f;

    [Header("UI References")]
    [SerializeField] RectTransform minimapRect;      // The RectTransform of Minimap panel (pivot 0.5,0.5)
    [SerializeField] RectTransform iconParent;       // Usually the same as minimapRect
    [SerializeField] RectTransform enemyDotPrefab;   // Small UI Image prefab (e.g., red 6x6)
    [SerializeField] RectTransform playerDotPrefab;  // Small UI Image prefab (e.g., blue 8x8)

    [Header("Tracking")]
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] string playerTag = "Player";
    [SerializeField] float refreshEvery = 2f;        // seconds to rescan enemies/player

    [Header("Options")]
    [SerializeField] bool clampToEdge = true;        // show off-map at border vs hide
    [SerializeField] bool scaleDotWithPanel = false; // auto-scale dots with minimap size
    [SerializeField] Vector2 enemyDotSize = new Vector2(6, 6);
    [SerializeField] Vector2 playerDotSize = new Vector2(8, 8);

    readonly Dictionary<Transform, RectTransform> enemyDots = new();
    Transform playerTf;
    RectTransform playerDot;

    float RangeX => Mathf.Max(0.0001f, maxX - minX);
    float RangeZ => Mathf.Max(0.0001f, maxZ - minZ);

    private void Awake()
    {
        Assert.IsNotNull(minimapRect);
        if (iconParent == null)
            iconParent = minimapRect;
        Assert.IsNotNull(enemyDotPrefab);
        Assert.IsNotNull(playerDotPrefab);
    }

    void Start()
    {
        RefreshAll();
        InvokeRepeating(nameof(RefreshAll), refreshEvery, refreshEvery);
    }

    void RefreshAll()
    {
        RefreshEnemies();
        RefreshPlayer();
    }

    void RefreshEnemies()
    {
        // Find only the top-most tagged "Enemy" in any hierarchy (skip children whose ancestor is also tagged)
        var found = GameObject.FindGameObjectsWithTag(enemyTag)
                              .Select(go => go.transform)
                              .Where(t => !HasTaggedAncestor(t, enemyTag))
                              .ToList();

        // Add new
        foreach (var t in found)
        {
            if (!enemyDots.ContainsKey(t))
            {
                var dot = Instantiate(enemyDotPrefab, iconParent);
                dot.gameObject.SetActive(true);
                if (!scaleDotWithPanel) dot.sizeDelta = enemyDotSize;
                enemyDots.Add(t, dot);
                dot.transform.localPosition = new(0f, 0f, -0.5f);
            }
        }

        // Remove missing/destroyed
        var toRemove = enemyDots.Keys.Where(t => t == null || !found.Contains(t)).ToList();
        foreach (var dead in toRemove)
        {
            if (enemyDots[dead]) Destroy(enemyDots[dead].gameObject);
            enemyDots.Remove(dead);
        }
    }

    void RefreshPlayer()
    {
        if (playerTf == null)
        {
            var pgo = GameObject.FindGameObjectsWithTag(playerTag).FirstOrDefault();
            playerTf = pgo ? pgo.transform : null;
        }

        if (playerTf != null && playerDot == null)
        {
            playerDot = Instantiate(playerDotPrefab, iconParent);
            playerDot.gameObject.SetActive(true);
            if (!scaleDotWithPanel) playerDot.sizeDelta = playerDotSize;
            playerDot.transform.localPosition = new(0f, 0f, -0.5f);
        }
    }

    void LateUpdate()
    {
        // Enemies
        foreach (var kv in enemyDots)
        {
            var t = kv.Key;
            var dot = kv.Value;
            if (t == null) { if (dot) dot.gameObject.SetActive(false); continue; }
            PositionDot(dot, t.position);
        }

        // Player
        if (playerTf && playerDot)
        {
            PositionDot(playerDot, playerTf.position);
            RotateDot(playerDot, playerTf.forward);
        }
    }

    // ----- Helpers -----

    // Skip any object whose ancestor is also tagged with the same tag
    bool HasTaggedAncestor(Transform t, string tag)
    {
        var p = t.parent;
        while (p != null)
        {
            if (p.CompareTag(tag)) return true;
            p = p.parent;
        }
        return false;
    }

    // Map world XZ -> minimap anchoredPosition; handle clamping & scaling
    void PositionDot(RectTransform dot, Vector3 worldPos)
    {
        float u = (worldPos.x - minX) / RangeX; // 0..1
        float v = (worldPos.z - minZ) / RangeZ; // 0..1

        bool inside = (u >= 0f && u <= 1f && v >= 0f && v <= 1f);
        if (!inside && !clampToEdge)
        {
            dot.gameObject.SetActive(false);
            return;
        }

        dot.gameObject.SetActive(true);

        if (clampToEdge)
        {
            u = Mathf.Clamp01(u);
            v = Mathf.Clamp01(v);
        }

        float x = (u - 0.5f) * minimapRect.rect.width;
        float y = (v - 0.5f) * minimapRect.rect.height;
        dot.anchoredPosition = new Vector2(x, y);

        if (scaleDotWithPanel)
        {
            // Tweak divisor for your taste; keeps relative visual size as panel changes
            float s = Mathf.Clamp(minimapRect.rect.width, 64f, 1024f) / 32f;
            dot.sizeDelta = new Vector2(s, s);
        }
    }

    void RotateDot(RectTransform dot, Vector3 forward)
    {
        Vector3 flatForward = new Vector3(forward.x, 0f, forward.z).normalized;
        float angle = Mathf.Atan2(flatForward.x, flatForward.z) * Mathf.Rad2Deg;
        dot.rotation = Quaternion.Euler(0f, 0f, -angle);
    }

    // Public hook if your spawners/despawners want to force an immediate refresh
    public void NotifyEnemiesChanged() => RefreshAll();

    public Vector2 GetPlayerPosition()
    {
        return playerDot.anchoredPosition;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 a = new Vector3(minX, 0f, minZ);
        Vector3 b = new Vector3(maxX, 0f, minZ);
        Vector3 c = new Vector3(maxX, 0f, maxZ);
        Vector3 d = new Vector3(minX, 0f, maxZ);
        Gizmos.DrawLine(a, b); Gizmos.DrawLine(b, c); Gizmos.DrawLine(c, d); Gizmos.DrawLine(d, a);
    }
}
