using UnityEngine;

public class LookableObject : MonoBehaviour, ILookable
{
    Renderer rend;

    Vector3 originalScale;
    Vector3 targetScale;

    bool isLooking = false;
    public GameObject objectName;

    public float growAmount = 1.1f;
    public float scaleSpeed = 8f;
    public float glowIntensity = 2f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalScale = transform.localScale;
        targetScale = originalScale;

        // Make sure emission is enabled
        rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        
        // Smooth scale
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );
        if (isLooking)
        {
            float pulse = Mathf.Sin(Time.time * 3f) * 0.01f;
            transform.localScale = targetScale * (1f + pulse);
        }
        // Smooth emission fade
        Color currentEmission = rend.material.GetColor("_EmissionColor");

        Color targetEmission = isLooking
            ? Color.yellow * glowIntensity
            : Color.black;

        rend.material.SetColor(
            "_EmissionColor",
            Color.Lerp(currentEmission, targetEmission, Time.deltaTime * scaleSpeed)
        );
    }

    public void OnLookEnter()
    {
        objectName.SetActive(true);
        isLooking = true;
        targetScale = originalScale * growAmount;
    }

    public void OnLookExit()
    {
        objectName.SetActive(false);
        isLooking = false;
        targetScale = originalScale;
    }
}
