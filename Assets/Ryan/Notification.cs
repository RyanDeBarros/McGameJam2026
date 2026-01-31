using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private float lifetime = 5f;

    private Coroutine lifeRoutine;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Assert.IsNotNull(message);

        canvasGroup = GetComponent<CanvasGroup>();
        Assert.IsNotNull(canvasGroup);
    }

    public void SetMessage(string m)
    {
        message.text = m;
    }

    public void DisableVisual()
    {
        canvasGroup.alpha = 0f;
    }

    public void EnableVisual()
    {
        canvasGroup.alpha = 1f;
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
