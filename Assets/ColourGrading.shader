Shader"Custom/LUTColourGrading"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}   // Render texture of the camera's view
        _LUTTex ("LUT Texture", 2D) = "white" {}     // LUT texture (256x1)
        _Saturation ("Saturation", Range(0, 2)) = 1  // Saturation control (0 = greyscale, 1 = full color)
    }
    SubShader
    {
Cull Off

ZWrite Off

ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex; // Camera render texture
sampler2D _LUTTex; // LUT texture (256x1)
float _Saturation; // Saturation control

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex); // Object space to clip space conversion
    o.uv = v.uv;
    return o;
}

            // Function to calculate greyscale based on luminance
float3 ConvertToGrayscale(float3 color)
{
    return dot(color, float3(0.299, 0.587, 0.114)); // Standard luminance calculation
}

fixed4 frag(v2f i) : SV_Target
{
                // Get color from the main scene texture (camera view)
    fixed4 col = tex2D(_MainTex, i.uv);

                // Normalize color to the range [0, 1]
    float3 colorNormalized = saturate(col.rgb);

                // LUT texture dimensions
    float cellWidth = 16.0;
    float lutWidth = 256.0;

                // Calculate UV coordinates for LUT sampling
    float lutX = colorNormalized.r * (cellWidth - 1.0) + colorNormalized.g * (cellWidth - 1.0) * cellWidth;

                // LUT Y coordinate is always 0 since it's a 1-pixel high LUT
    float2 lutUV = float2(lutX / lutWidth, 0.0);

                // Sample the LUT for the color
    fixed4 lutColor = tex2D(_LUTTex, lutUV);

                // Convert LUT color to greyscale
    float3 greyscaleColor = ConvertToGrayscale(lutColor.rgb);

                // Blend between LUT color and greyscale based on the saturation parameter
    float3 finalColor = lerp(greyscaleColor.xxx, lutColor.rgb, _Saturation);

                // Return the final color with the adjusted saturation
    return fixed4(finalColor, lutColor.a);
}
            ENDCG
        }
    }
}
