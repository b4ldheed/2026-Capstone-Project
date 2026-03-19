// Michael Attardi - March 2026

Shader "Custom/URP/ColourQuantisationShader"
{
    Properties
    {
        _RedSteps ("Red Steps", Range(2, 32)) = 8 // Number of allowed brightness levels for the Red channel.
        _GreenSteps ("Green Steps", Range(2, 32)) = 8 // Number of allowed brightness levels for the Green channel.
        _BlueSteps ("Blue Steps", Range(2, 32)) = 8 // Number of allowed brightness levels for the Blue channel.
        _EffectStrength ("Effect Strength", Range(0, 1)) = 1 // Blends between the original image and the quantised result.
    }

    SubShader
    {
        Tags // Universal Render Pipeline fullscreen-compatible shader.
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
        }

        // Fullscreen post-process settings.
        ZWrite Off
        ZTest Always
        Cull Off

        Pass
        {
            Name "ColourQuantisationPass"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            // URP fullscreen and blit (bit block transfer) helpers.
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            CBUFFER_START(UnityPerMaterial)
                // Number of quantisation levels per colour channel.
                float _RedSteps;
                float _GreenSteps;
                float _BlueSteps;

                // Blend amount for overall effect.
                float _EffectStrength;
            CBUFFER_END

            // Prevent invalid step counts and snap the value to a smaller set of levels.
            float QuantiseChannel(float value, float steps)
            {
                steps = max(steps, 2.0);
                return floor(value * steps) / steps;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                // Sample the current screen colour.
                float2 uv = input.texcoord;
                half4 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv);

                // Quantise each colour channel independently.
                float3 quantised;
                quantised.r = QuantiseChannel(col.r, _RedSteps);
                quantised.g = QuantiseChannel(col.g, _GreenSteps);
                quantised.b = QuantiseChannel(col.b, _BlueSteps);

                float3 result = lerp(col.rgb, quantised, saturate(_EffectStrength)); // Blend between the original and quantised image.
                return half4(result, col.a);
            }

            ENDHLSL
        }
    }

    FallBack Off
}