using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private float lifetimeMin = 2.5f;
    [SerializeField] private float lifetimeMax = 3.5f;
    public float lifetime;
    private float age = 0f;

    private Coroutine lifeRoutine;

    private void Awake()
    {
        Assert.IsNotNull(message);

        lifetime = Random.Range(lifetimeMin, lifetimeMax);
    }

    public void Start()
    {
        IEnumerator LifeRoutine()
        {
            yield return new WaitForSeconds(lifetime);
            NotificationManager.GetInstance().Dismiss(this);
        }

        lifeRoutine = StartCoroutine(LifeRoutine());
    }

    private void Update()
    {
        age += Time.deltaTime;
    }

    public void SetMessage(string m)
    {
        message.text = m;
    }

    public void CancelTimer()
    {
        if (lifeRoutine != null)
            StopCoroutine(lifeRoutine);
    }

    public float GetAge()
    {
        return age;
    }
}
