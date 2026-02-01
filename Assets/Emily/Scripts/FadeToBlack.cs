using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private float duration;

    void OnEnable()
    {
        cg.alpha = 0;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }
        cg.alpha = 1;
        gameObject.SetActive(false);
    }

}
