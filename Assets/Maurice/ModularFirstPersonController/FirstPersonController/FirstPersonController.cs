
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    public bool lsd_mode;
    private Rigidbody rb;
    public static bool disable = false;

    public Camera playerCamera;

    public float fov = 60f;
    public bool mouseInvertCameraY = false;
    public bool controllerInvertCameraY = true;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float controllerSensitivity = 4f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;


    // Internal Variables
    private bool isZoomed = false;

    public bool playerCanMove = true;
    public static float walkSpeed = 10f;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;

    private void Awake()
    {
        lsd_mode = false;
        rb = GetComponent<Rigidbody>();

        crosshairObject = GetComponentInChildren<Image>();

        // Set internal variables
        playerCamera.fieldOfView = fov;
    }

    void Start()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (disable) return;

        // Control camera movement
        if (cameraCanMove)
        {
            float horizontalInput = 0f;
            float verticalInput = 0f;
            float sensitivity = 0f;
            bool invertCameraY;

            if (IsControllerConnected())
            {
                horizontalInput = Input.GetAxis("Right Stick X");
                verticalInput = Input.GetAxis("Right Stick Y");
                sensitivity = controllerSensitivity;
                invertCameraY = controllerInvertCameraY;
            }
            else
            {
                horizontalInput = Input.GetAxis("Mouse X");
                verticalInput = Input.GetAxis("Mouse Y");
                sensitivity = mouseSensitivity;
                invertCameraY = mouseInvertCameraY;
            }

            yaw = transform.localEulerAngles.y + horizontalInput * sensitivity;

            if (!invertCameraY)
                pitch -= verticalInput * sensitivity;
            else
                pitch += verticalInput * sensitivity;

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (disable) return;
        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Checks if player is walking and isGrounded
            // Will allow head bob
            if (targetVelocity.x != 0 || targetVelocity.z != 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.linearVelocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    private bool IsControllerConnected()
    {
        string[] joysticks = Input.GetJoystickNames();

        foreach (string joystick in joysticks)
        {
            if (!string.IsNullOrEmpty(joystick))
                return true;
        }

        return false;
    }
}