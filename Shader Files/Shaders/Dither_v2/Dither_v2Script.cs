using UnityEngine;

[ExecuteAlways]
public class Dither_v2Script : MonoBehaviour
{
    [Header("Dithering Settings")]
    [SerializeField] private Material dither_v2Material;

    [Range(0f, 1f)] public float ditherStrength = 0.15f;
    public BayerMatrixSize bayerMatrixSize = BayerMatrixSize.Bayer16x16;

    [Header("Brightness Mode")]
    public bool usePerceivedBrightness = false;

    [Range(0.2f, 1.0f)] public float perceptualGamma = 0.5f;

    public enum BayerMatrixSize
    {
        Bayer2x2 = 2,
        Bayer4x4 = 4,
        Bayer8x8 = 8,
        Bayer16x16 = 16
    }

    private static readonly int DitherStrengthID = Shader.PropertyToID("_DitherStrength");
    private static readonly int BayerSizeID = Shader.PropertyToID("_BayerSize");
    private static readonly int UsePerceivedBrightnessID = Shader.PropertyToID("_UsePerceivedBrightness");
    private static readonly int PerceptualGammaID = Shader.PropertyToID("_PerceptualGamma");

    void Update()
    {
        if (dither_v2Material == null) return;

        dither_v2Material.SetFloat(DitherStrengthID, ditherStrength);
        dither_v2Material.SetFloat(BayerSizeID, (float)bayerMatrixSize);
        dither_v2Material.SetFloat(UsePerceivedBrightnessID, usePerceivedBrightness ? 1.0f : 0.0f);
        dither_v2Material.SetFloat(PerceptualGammaID, perceptualGamma);
    }
}