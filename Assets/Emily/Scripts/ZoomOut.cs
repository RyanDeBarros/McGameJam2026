using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject startObj;
    [SerializeField] private GameObject endObj;
    private Vector3 start;
    private Vector3 end;
    [SerializeField] private float duration = 3f;

    [SerializeField] private GameObject enabledObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        start = startObj.transform.position;
        end = endObj.transform.position;
    }

    void OnEnable()
    {
        StartCoroutine(ZoomingOut());
    }

    private IEnumerator ZoomingOut()
    {
        float elapsed = 0;
        end = new Vector3(0,0.75f,0) + end;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        transform.position = end;
        enabledObject.SetActive(true);
        gameObject.SetActive(false);
    }

    
}
