using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffectsController : MonoBehaviour
{
    public Volume volume;
    public static bool distort;

    private ColorAdjustments colorAdjustments;
    private Bloom bloom;
    private ChromaticAberration chromatic;
    private LensDistortion lensDistortion;
    private Vignette vignette;
    private FilmGrain grain;
    private MotionBlur blur;

    void Start()
    {
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chromatic);
        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out grain);
        volume.profile.TryGet(out blur);
    }

    void Update()
    {
        if (!distort)
        {
            volume.weight = 0f;
            return;
        }

        volume.weight = 1f;

        // Layered oscillation for chaotic movement
        float t1 = Mathf.Sin(Time.time * 4f);
        float t2 = Mathf.Sin(Time.time * 2.2f);
        float t = t1 + t2 * 0.5f;

        // 🎨 Color Adjustments
        colorAdjustments.saturation.value = 90f;
        colorAdjustments.hueShift.value = t * 70f;
        colorAdjustments.postExposure.value = t * 1.2f;

        // 🔥 Bloom
        bloom.intensity.value = 6f + Mathf.Abs(t) * 3f;
        bloom.scatter.value = 0.8f;

        // 🌈 Chromatic Aberration
        chromatic.intensity.value = Mathf.Clamp01(0.9f + Mathf.Abs(t) * 0.4f);

        // 🌀 Lens Distortion
        lensDistortion.intensity.value = Mathf.Lerp(-1f, 1f, (t + 1f) * 0.5f);
        lensDistortion.scale.value = 0.8f + t * 0.1f;

        // 🌫 Vignette
        vignette.intensity.value = 0.35f + Mathf.Abs(t) * 0.1f;

        // 📺 Film Grain
        grain.intensity.value = 1f;
        grain.response.value = 1f;

        // 🎥 Motion Blur
        blur.intensity.value = 0.8f;
    }
}
