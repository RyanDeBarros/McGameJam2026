using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContinuousColorChange : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float duration = 3f;

    private void Start()
    {
        StartCoroutine(ColorWheelLoop());
    }

    private IEnumerator ColorWheelLoop()
    {
        float hue = 0f;

        while (true)
        {
            hue += Time.deltaTime / duration;

            if (hue > 1f) hue -= 1f;

            image.color = Color.HSVToRGB(hue, 1f, 1f);

            yield return null;
        }
    }
}
