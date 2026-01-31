using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    private bool open = false;

    private PlayerInput playerInput;
    private InputAction toggleAction;

    private void Awake()
    {
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
        Debug.Log("Open Phone!");
    }

    public void ClosePhone()
    {
        if (!open)
            return;

        open = false;
        Debug.Log("Close Phone!");
    }
}
