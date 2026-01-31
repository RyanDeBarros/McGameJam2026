using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    private bool open = false;
    private float openedFactor = 0f;

    [Header("Phone Animation")]
    [SerializeField] private RectTransform visual;
    [SerializeField] private RectTransform openedVisual;
    [SerializeField] private RectTransform closedVisual;
    [SerializeField] private float toggleAnimationSpeed = 5f;
    private Coroutine visualAnimation = null;

    private PlayerInput playerInput;
    private InputAction toggleAction;

    private void Awake()
    {
        Assert.IsNotNull(visual);
        Assert.IsNotNull(openedVisual);
        Assert.IsNotNull(closedVisual);
        SetPhoneTransform();

        playerInput = GetComponent<PlayerInput>();
        Assert.IsNotNull(playerInput);
        toggleAction = playerInput.actions["TogglePhone"];
        Assert.IsNotNull(toggleAction);
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
    }

    public void ClosePhone()
    {
        if (!open)
            return;
        open = false;

        if (visualAnimation != null)
            StopCoroutine(visualAnimation);
        visualAnimation = StartCoroutine(ClosePhoneAnimation());
    }

    private IEnumerator ClosePhoneAnimation()
    {
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
        visual.transform.localScale = Vector3.Lerp(closedVisual.localScale, openedVisual.localScale, openedFactor);
        visual.transform.position = Vector3.Lerp(closedVisual.position, openedVisual.position, openedFactor);
        visual.transform.rotation = Quaternion.Slerp(closedVisual.rotation, openedVisual.rotation, openedFactor);
    }
}
