using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class NotificationManager : MonoBehaviour
{
    [Header("Notification UI")]
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] float notificationSpacing = 50f;
    [SerializeField] private int maxNotifications = 5;
    [SerializeField] private float scrollSpeed = 500f;
    [SerializeField] private float swipeSpeed = 1000f;

    [Header("Messages")]
    [SerializeField] private List<string> distractionMessages = new();

    private readonly List<Notification> notifications = new();
    private bool enableNonTutorial = true;

    private void Awake()
    {
        Assert.IsNotNull(notificationPrefab);
    }

    public void Notify(string message, float lifetime = 0f)
    {
        var go = Instantiate(notificationPrefab);
        Notification notification = go.GetComponent<Notification>();
        notification.SetMessage(message);
        if (lifetime > 0f)
            notification.lifetime = lifetime;
        if (notifications.Count == maxNotifications)
            DismissOldestNotification();
        SendNotificationUI(notification);
        notifications.Add(notification);
    }

    private void DismissOldestNotification()
    {
        Notification n = notifications.OrderByDescending(n => n.GetAge()).First();
        n.CancelTimer();
        Dismiss(n);
    }

    private void SendNotificationUI(Notification notification)
    {
        for (int i = 0; i < notifications.Count; ++i)
            StartCoroutine(AnimateNotificationScroll(notifications[i], Vector2.down, scrollSpeed, notificationSpacing));

        PhoneController.GetInstance().PlayNotificationSound();

        RectTransform notifRect = notification.GetComponent<RectTransform>();
        notifRect.SetParent(PhoneController.GetInstance().GetNotificationRoot(), false);
        notifRect.anchorMin = new Vector2(0, 1);
        notifRect.anchorMax = new Vector2(1, 1);
        notifRect.pivot = new Vector2(0f, 1f);
        notifRect.anchoredPosition = Vector2.up * notificationSpacing;
        StartCoroutine(AnimateNotificationScroll(notification, Vector2.down, scrollSpeed, notificationSpacing));
    }

    public void Dismiss(Notification notification)
    {
        int index = notifications.IndexOf(notification);
        DismissNotificationUI(index);
        notifications.RemoveAt(index);
    }

    private void DismissNotificationUI(int index)
    {
        for (int i = 0; i < index; ++i)
            StartCoroutine(AnimateNotificationScroll(notifications[i], Vector2.up, scrollSpeed, notificationSpacing));

        IEnumerator KillRoutine(Notification notification)
        {
            yield return AnimateNotificationScroll(notification, Vector2.left, swipeSpeed, PhoneController.GetInstance().GetScreenWidth());
            Destroy(notification.gameObject);
            yield return null;
        }

        StartCoroutine(KillRoutine(notifications[index]));
    }

    private IEnumerator AnimateNotificationScroll(Notification notification, Vector2 direction, float speed, float spacing)
    {
        float distance = 0f;
        while (distance < spacing)
        {
            float deltaDistance = speed * Time.deltaTime;
            if (distance + deltaDistance < spacing)
                distance += deltaDistance;
            else
            {
                deltaDistance = spacing - distance;
                distance = spacing;
            }

            notification.GetComponent<RectTransform>().anchoredPosition += direction * deltaDistance;

            yield return null;
        }
    }

    public static NotificationManager GetInstance()
    {
        return GameObject.FindObjectsByType<NotificationManager>(FindObjectsSortMode.None).First();
    }

    public static void NotifyMomDistance(float distanceMeters)
    {
        if (GetInstance().enableNonTutorial)
            GetInstance().Notify($"Mom {distanceMeters}m away!");
    }

    public static void NotifyBabyStart(int numBabiesToCollect)
    {
        GetInstance().Notify($"You have {numBabiesToCollect} bab{(numBabiesToCollect != 1 ? "ies" : "y")} to find.");
    }

    public static void NotifyBabyCollection(int babiesLeft)
    {
        if (GetInstance().enableNonTutorial)
            GetInstance().Notify($"{babiesLeft} bab{(babiesLeft != 1 ? "ies" : "y")} left!");
    }

    public static void NotifyBabyCompletion()
    {
        if (GetInstance().enableNonTutorial)
            GetInstance().Notify($"You collected all babies. CONGRATULATIONS...");
    }

    public static void NotifyBabyNearby()
    {
        if (GetInstance().enableNonTutorial)
            GetInstance().Notify("A baby is nearby!");
    }

    public static void NotifyDistraction()
    {
        if (GetInstance().enableNonTutorial)
            GetInstance().Notify(GetInstance().distractionMessages.GetRandomElement());
    }

    public static void DisableNonTutorialNotifications()
    {
        GetInstance().enableNonTutorial = false;
    }

    public static void EnableNonTutorialNotifications()
    {
        GetInstance().enableNonTutorial = true;
    }
}
