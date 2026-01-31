using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] float notificationSpacing = 50f;
    [SerializeField] private int maxNotifications = 4;

    private readonly List<Notification> notifications = new();
    private readonly List<Notification> waitingQueue = new();

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
        NotifyBabyStart(6);
        yield return new WaitForSeconds(1f);
        NotifyMomDistance(30);
        yield return new WaitForSeconds(1f);
        NotifyBabyCollection(5);
        yield return new WaitForSeconds(1f);
        NotifyDistraction("SiNgLe MaReS iN yOuR aReA");
        yield return new WaitForSeconds(1f);
        NotifyBabyNearby();
        yield return new WaitForSeconds(1f);
        NotifyDistraction("New iOS update");
        yield return new WaitForSeconds(1f);
        NotifyBabyCollection(4);
    }

    public void Notify(string message)
    {
        var go = Instantiate(notificationPrefab);
        Notification notification = go.GetComponent<Notification>();
        notification.SetMessage(message);
        notification.DisableVisual();
        waitingQueue.Add(notification);
        OnNotificationCountChanged();
    }

    private void OnNotificationCountChanged()
    {
        while (notifications.Count < maxNotifications && waitingQueue.Count > 0)
        {
            Notification notification = waitingQueue.First();
            waitingQueue.RemoveAt(0);

            SendNotificationUI(notification);
            notifications.Add(notification);
            notification.EnableVisual();
            notification.StartTimer();
        }
    }

    private void SendNotificationUI(Notification notification)
    {
        for (int i = 0; i < notifications.Count; ++i)
        {
            RectTransform rt = notifications[i].GetComponent<RectTransform>();
            rt.anchoredPosition += Vector2.down * notificationSpacing;
            // TODO animate motion
        }

        PhoneController.GetInstance().PlayNotificationSound();

        RectTransform notifRect = notification.GetComponent<RectTransform>();
        notifRect.SetParent(PhoneController.GetInstance().GetNotificationRoot(), false);
        notifRect.anchorMin = new Vector2(0, 1);
        notifRect.anchorMax = new Vector2(1, 1);
        notifRect.pivot = new Vector2(0f, 1f);
        notifRect.anchoredPosition = Vector2.zero;
        // TODO animate notification send
    }

    public void Dismiss(Notification notification)
    {
        int index = notifications.IndexOf(notification);
        DismissNotificationUI(index);
        notifications.RemoveAt(index);

        OnNotificationCountChanged();
    }

    private void DismissNotificationUI(int index)
    {
        for (int i = 0; i < index; ++i)
        {
            RectTransform rt = notifications[i].GetComponent<RectTransform>();
            rt.anchoredPosition += Vector2.up * notificationSpacing;
            // TODO animate motion
        }

        Destroy(notifications[index].gameObject); // TODO animate dismissal
    }

    public static NotificationManager GetInstance()
    {
        return GameObject.FindObjectsByType<NotificationManager>(FindObjectsSortMode.None).First();
    }

    public static void NotifyMomDistance(float distanceMeters)
    {
        GetInstance().Notify($"Mom {distanceMeters}m away!"); // TODO better message format
    }

    public static void NotifyBabyStart(int numBabiesToCollect)
    {
        GetInstance().Notify($"You have {numBabiesToCollect} bab{(numBabiesToCollect != 1 ? "ies" : "y")} to collect."); // TODO better message format
    }

    public static void NotifyBabyCollection(int babiesLeft)
    {
        GetInstance().Notify($"You collected another baby. You have {babiesLeft} bab{(babiesLeft != 1 ? "ies" : "y")} left."); // TODO better message format
    }

    public static void NotifyBabyNearby()
    {
        GetInstance().Notify($"A baby is nearby!"); // TODO better message format
    }

    public static void NotifyDistraction(string message)
    {
        // TODO different icon?
        GetInstance().Notify(message); // TODO randomly select from list of distraction strings
    }
}
