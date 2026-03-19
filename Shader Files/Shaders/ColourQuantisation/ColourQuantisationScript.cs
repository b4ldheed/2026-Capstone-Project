using UnityEngine;

[ExecuteAlways]
public class ColourQuantisationScript : MonoBehaviour
{
    [Header("Colour Quantisation Settings")]
    [SerializeField] private Material quantisationMaterial;

    [Range(2f, 32f)] public float redSteps = 8f;
    [Range(2f, 32f)] public float greenSteps = 8f;
    [Range(2f, 32f)] public float blueSteps = 8f;
    [Range(0f, 1f)] public float effectStrength = 1f;

    private static readonly int RedStepsID = Shader.PropertyToID("_RedSteps");
    private static readonly int GreenStepsID = Shader.PropertyToID("_GreenSteps");
    private static readonly int BlueStepsID = Shader.PropertyToID("_BlueSteps");
    private static readonly int EffectStrengthID = Shader.PropertyToID("_EffectStrength");

    void Update()
    {
        if (quantisationMaterial == null) return;

        quantisationMaterial.SetFloat(RedStepsID, redSteps);
        quantisationMaterial.SetFloat(GreenStepsID, greenSteps);
        quantisationMaterial.SetFloat(BlueStepsID, blueSteps);
        quantisationMaterial.SetFloat(EffectStrengthID, effectStrength);
    }
}