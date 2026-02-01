using UnityEngine;

public class BabyMovement : MonoBehaviour
{
    [SerializeField] private float hoverAmplitude = 0.5f;   // How high it moves up/down
    [SerializeField] private float hoverSpeed = 2f;          // How fast it hovers
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 90f, 0f); // Degrees per second

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position
        startPosition = transform.position;
    }

    void Update()
    {
        // Hover 
        float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
        transform.position = startPosition + Vector3.up * yOffset;

        // Rotation
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
