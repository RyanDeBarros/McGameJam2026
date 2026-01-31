using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Transform visualRoot;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private float lifetime = 5f;

    private Coroutine lifeRoutine;

    private void Awake()
    {
        Assert.IsNotNull(visualRoot);
        Assert.IsNotNull(message);
    }

    public void SetMessage(string m)
    {
        message.text = m;
    }

    public void DisableVisual()
    {
        visualRoot.gameObject.SetActive(false);
    }

    public void EnableVisual()
    {
        visualRoot.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(visualRoot.GetComponent<RectTransform>());
        message.ForceMeshUpdate();
    }

    public void StartTimer()
    {
        lifeRoutine ??= StartCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        NotificationManager.GetInstance().Dismiss(this);
    }
}
