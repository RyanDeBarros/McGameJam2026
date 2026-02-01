using UnityEngine;
using System.Collections;

public class ScaleUpAndDown : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float endScale = 1.2f;

    private Coroutine scaleCoroutine;

    private void OnEnable()
    {
        scaleCoroutine = StartCoroutine(ScaleIt());
    }

    private void OnDisable()
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
    }

    IEnumerator ScaleIt()
    {
        Vector3 start = Vector3.one;
        Vector3 end = Vector3.one * endScale;

        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;

            float t = Mathf.PingPong(time / duration, 1f);
            transform.localScale = Vector3.Lerp(start, end, t);

            yield return null;
        }
    }
}
