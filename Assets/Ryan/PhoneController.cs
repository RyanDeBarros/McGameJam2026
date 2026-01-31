using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    private bool open = false;
    private float openedFactor = 0f;

    [SerializeField] private float openedPhoneLinearDamping = 25f;
    private Transform player;

    [Header("Phone Animation")]
    [SerializeField] private RectTransform visual;
    [SerializeField] private RectTransform openedVisual;
    [SerializeField] private RectTransform closedVisual;
    [SerializeField] private float toggleAnimationSpeed = 5f;
    private Coroutine visualAnimation = null;
    [SerializeField] private RectTransform blackScreen;

    [Header("Notifications")]
    [SerializeField] private RectTransform notificationRoot;
    [SerializeField] private AudioSource notificationSound;
    [SerializeField] private List<float> openedDistractionDelays = new() { 5f, 4f, 3f, 2f, 1f };
    [SerializeField] private float closedDistractionDelayMin = 10f;
    [SerializeField] private float closedDistractionDelayMax = 50f;
    private int distractionDelayIndex = 0;
    private float distractionDelayLeft = 0f;

    private PlayerInput playerInput;
    private InputAction toggleAction;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        Assert.IsNotNull(visual);
        Assert.IsNotNull(openedVisual);
        Assert.IsNotNull(closedVisual);
        SetPhoneTransform();

        Assert.IsNotNull(blackScreen);
        blackScreen.gameObject.SetActive(true);

        Assert.IsNotNull(notificationRoot);
        Assert.IsNotNull(notificationSound);

        playerInput = GetComponent<PlayerInput>();
        Assert.IsNotNull(playerInput);
        toggleAction = playerInput.actions["TogglePhone"];
        Assert.IsNotNull(toggleAction);
    }

    private void Update()
    {
        distractionDelayLeft -= Time.deltaTime;

        if (distractionDelayLeft <= 0f)
        {
            if (open)
            {
                if (distractionDelayIndex + 1 < openedDistractionDelays.Count)
                    ++distractionDelayIndex;
                distractionDelayLeft = openedDistractionDelays[distractionDelayIndex];
            }
            else
                distractionDelayLeft = Random.Range(closedDistractionDelayMin, closedDistractionDelayMax);

            NotificationManager.NotifyDistraction();
        }
    }

    private void OnEnable()
    {
        toggleAction.performed += OnPhoneToggle;
    }

    private void OnDisable()
    {
        toggleAction.performed -= OnPhoneToggle;
    }

    private void OnPhoneToggle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TogglePhone();
    }

    public void TogglePhone()
    {
        if (open)
            ClosePhone();
        else
            OpenPhone();
    }

    public void OpenPhone()
    {
        if (open)
            return;
        open = true;

        if (visualAnimation != null)
            StopCoroutine(visualAnimation);
        visualAnimation = StartCoroutine(OpenPhoneAnimation());

        distractionDelayIndex = 0;
        distractionDelayLeft = openedDistractionDelays[distractionDelayIndex];

        player.GetComponent<Rigidbody>().linearDamping = openedPhoneLinearDamping;
    }

    private IEnumerator OpenPhoneAnimation()
    {
        while (openedFactor < 1f)
        {
            openedFactor += toggleAnimationSpeed * Time.deltaTime;
            openedFactor = Mathf.Clamp01(openedFactor);
            SetPhoneTransform();
            yield return null;
        }

        blackScreen.gameObject.SetActive(false);
    }

    public void ClosePhone()
    {
        if (!open)
            return;
        open = false;

        if (visualAnimation != null)
            StopCoroutine(visualAnimation);
        visualAnimation = StartCoroutine(ClosePhoneAnimation());

        distractionDelayLeft = Random.Range(closedDistractionDelayMin, closedDistractionDelayMax);

        player.GetComponent<Rigidbody>().linearDamping = 0f;
    }

    private IEnumerator ClosePhoneAnimation()
    {
        blackScreen.gameObject.SetActive(true);

        while (openedFactor > 0f)
        {
            openedFactor -= toggleAnimationSpeed * Time.deltaTime;
            openedFactor = Mathf.Clamp01(openedFactor);
            SetPhoneTransform();
            yield return null;
        }
    }

    private void SetPhoneTransform()
    {
        RectTransform v = visual;
        RectTransform closed = closedVisual;
        RectTransform opened = openedVisual;

        v.anchorMin = Vector2.Lerp(closed.anchorMin, opened.anchorMin, openedFactor);
        v.anchorMax = Vector2.Lerp(closed.anchorMax, opened.anchorMax, openedFactor);
        v.pivot = Vector2.Lerp(closed.pivot, opened.pivot, openedFactor);

        v.anchoredPosition = Vector2.Lerp(closed.anchoredPosition, opened.anchoredPosition, openedFactor);
        v.sizeDelta = Vector2.Lerp(closed.sizeDelta, opened.sizeDelta, openedFactor);
        v.localRotation = Quaternion.Slerp(closed.localRotation, opened.localRotation, openedFactor);
    }

    public Transform GetNotificationRoot()
    {
        return notificationRoot;
    }

    public float GetScreenWidth()
    {
        return blackScreen.rect.width;
    }

    public float GetScreenHeight()
    {
        return blackScreen.rect.height;
    }

    public void PlayNotificationSound()
    {
        notificationSound.Play();
    }

    public static PhoneController GetInstance()
    {
        return GameObject.FindObjectsByType<PhoneController>(FindObjectsSortMode.None).First();
    }
}
