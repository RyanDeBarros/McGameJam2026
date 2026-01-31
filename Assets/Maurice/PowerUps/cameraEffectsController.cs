using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffectsController : MonoBehaviour
{
    public Volume volume;
    [SerializeField]private bool distort;
    private ColorAdjustments colorAdjustments;
    private Bloom bloom;
    private ChromaticAberration chromatic;
    private LensDistortion lensDistortion;
    private Vignette vignette;

    void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments)) { }
        if (volume.profile.TryGet(out bloom)) { }
        if (volume.profile.TryGet(out chromatic)) { }
        if (volume.profile.TryGet(out lensDistortion)) { }
        if (volume.profile.TryGet(out vignette)) { }
    }

    void Update()
    {
        volume.weight = 0f;
        if (!distort) { return; }
        volume.weight = 1f;
        float t = Mathf.Sin(Time.time * 4f);

        // 🎨 Color
        colorAdjustments.saturation.value = 80f;
        colorAdjustments.hueShift.value = t * 50f;

        // 🔥 Bloom
        bloom.intensity.value = 5f + t * 2f;

        // 🌈 Chromatic Aberration
        chromatic.intensity.value = 0.7f + Mathf.Abs(t) * 0.3f;

        // 🌀 Lens Distortion
        lensDistortion.intensity.value = -0.4f + t * 0.2f;

        // 🌫 Vignette
        vignette.intensity.value = 0.3f;
    }
}
