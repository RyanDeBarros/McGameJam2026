using UnityEngine;
using UnityEngine.UI;

public class VoiceHUD : MonoBehaviour
{
    public GameObject mainPlayer;
    public float smoothSpeed = 10f;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private float maxLoudness = 0.1f;
    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthBar.value = 0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (mainPlayer != null)
        {
            
            _healthBar.value = Mathf.Clamp(mainPlayer.GetComponent<MicInputAction>().loudness / maxLoudness, 0f, 1f);
            Debug.Log(_healthBar.value);
        }

    }
}
