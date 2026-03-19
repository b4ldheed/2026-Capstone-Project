using UnityEngine;

[ExecuteAlways]
public class DitherScript : MonoBehaviour
{
    [Header("Dithering Settings")]
    [SerializeField] private Material ditherMaterial;

    [Range(0f, 1f)] public float ditherStrength = 0.15f;
    public BayerMatrixSize bayerMatrixSize = BayerMatrixSize.Bayer16x16;

    public enum BayerMatrixSize
    {
        Bayer2x2 = 2,
        Bayer4x4 = 4,
        Bayer8x8 = 8,
        Bayer16x16 = 16
    }

    private static readonly int DitherStrengthID = Shader.PropertyToID("_DitherStrength");
    private static readonly int BayerSizeID = Shader.PropertyToID("_BayerSize");

    void Update()
    {
        if (ditherMaterial == null) return;

        ditherMaterial.SetFloat(DitherStrengthID, ditherStrength);
        ditherMaterial.SetFloat(BayerSizeID, (float)bayerMatrixSize);
    }
}