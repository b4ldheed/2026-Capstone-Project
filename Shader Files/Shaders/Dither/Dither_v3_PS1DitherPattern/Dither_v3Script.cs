using UnityEngine;

[ExecuteAlways]
public class Dither_v3Script : MonoBehaviour
{
    [Header("Dithering Settings")]
    [SerializeField] private Material ditherMaterial;

    [Range(0f, 1f)] public float ditherStrength = 0.15f;
    public BayerMatrixSize bayerMatrixSize = BayerMatrixSize.Bayer16x16;

    [Header("Brightness Mode")]
    public bool usePerceivedBrightness = false;

    [Range(0.2f, 1.0f)] public float perceptualGamma = 0.5f;

    [Header("Matrix Mode")]
    public bool usePS1Matrix = false;

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
    private static readonly int UsePS1MatrixID = Shader.PropertyToID("_UsePS1Matrix");

    void Update()
    {
        if (ditherMaterial == null) return;

        ditherMaterial.SetFloat(DitherStrengthID, ditherStrength);
        ditherMaterial.SetFloat(BayerSizeID, (float)bayerMatrixSize);
        ditherMaterial.SetFloat(UsePerceivedBrightnessID, usePerceivedBrightness ? 1.0f : 0.0f);
        ditherMaterial.SetFloat(PerceptualGammaID, perceptualGamma);
        ditherMaterial.SetFloat(UsePS1MatrixID, usePS1Matrix ? 1.0f : 0.0f);
    }
}