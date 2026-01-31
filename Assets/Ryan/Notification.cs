using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class Notification : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI message;

    private void Awake()
    {
        Assert.IsNotNull(message);
    }

    public void SetMessage(string m)
    {
        message.text = m;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        NotificationManager.GetInstance().Dismiss(this);
    }
}
