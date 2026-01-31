using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEditor.Progress;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] float notificationSpacing = 100f;

    private readonly List<Notification> notifications = new();

    private void Awake()
    {
        Assert.IsNotNull(notificationPrefab);
    }

    private void Start()
    {
        StartCoroutine(StartNotifs());
    }

    private IEnumerator StartNotifs()
    {
        Notify("Message 1!");
        yield return new WaitForSeconds(3f);
        Notify("Message 2!!");
        yield return new WaitForSeconds(3f);
        Notify("Message 3!!!");
    }

    public void Notify(string message)
    {
        var go = Instantiate(notificationPrefab);
        Notification notification = go.GetComponent<Notification>();
        notification.SetMessage(message);
        SendNotificationUI(notification);
        notifications.Add(notification);
    }

    private void SendNotificationUI(Notification notification)
    {
        for (int i = 0; i < notifications.Count; ++i)
            notifications[i].transform.position += Vector3.down * notificationSpacing; // TODO animate motion

        RectTransform notifRect = notification.GetComponent<RectTransform>();
        notifRect.SetParent(PhoneController.GetInstance().GetNotificationRoot(), false);
        notifRect.anchorMin = new Vector2(0, 1);
        notifRect.anchorMax = new Vector2(1, 1);
        notifRect.pivot = new Vector2(0.5f, 1f);
        notifRect.anchoredPosition = Vector2.zero;
        // TODO animate notification send
    }

    public void Dismiss(Notification notification)
    {
        if (!PhoneController.GetInstance().IsOpen())
            return;

        int index = notifications.IndexOf(notification);
        DismissNotificationUI(index);
        notifications.RemoveAt(index);
    }

    private void DismissNotificationUI(int index)
    {
        for (int i = 0; i < index; ++i)
            notifications[i].transform.position += Vector3.up * notificationSpacing; // TODO animate motion

        Destroy(notifications[index]); // TODO animate dismissal
    }

    public static NotificationManager GetInstance()
    {
        return GameObject.FindObjectsByType<NotificationManager>(FindObjectsSortMode.None).First();
    }
}
