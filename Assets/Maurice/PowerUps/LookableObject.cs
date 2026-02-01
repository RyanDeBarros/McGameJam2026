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

    // Hover animation settings
    public float hoverAmplitude = 0.05f;   // how high it floats
    public float hoverFrequency = 2f;      // speed of floating
    public float rotationSpeed = 20f;      // degrees per second

    Vector3 basePosition;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalScale = transform.localScale;
        targetScale = originalScale;

        basePosition = transform.position;

        rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float hoverOffset = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = basePosition + new Vector3(0, hoverOffset, 0);

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

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

        Color currentEmission = rend.material.GetColor("_EmissionColor");
        Color targetEmission = isLooking ? Color.yellow * glowIntensity : Color.black;

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
