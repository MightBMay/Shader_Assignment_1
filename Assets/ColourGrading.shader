Shader"Custom/LUTColourGrading"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}   // render texture of the camera's view
        _LUTTex ("LUT Texture", 2D) = "white" {}     // LUT texture
        _Saturation ("Saturation", Range(0, 2)) = 1  // saturation control to adjust LUT strength.
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

sampler2D _MainTex; 
sampler2D _LUTTex; 
float _Saturation; 

v2f vert(appdata v)
{
    v2f o;                                     // object space to clip space conversion.
    o.vertex = UnityObjectToClipPos(v.vertex); // not going to lie, i don't fully understand the math behind this,
                                               // but without it, the entire render texture turns magenta.
    o.uv = v.uv; 
    return o;
}

            // Function to calculate greyscale based on luminance
float3 ConvertToGrayscale(float3 color)
{
    return dot(color, float3(0.299, 0.587, 0.114)); // Standard luminance calculation- 
}

fixed4 frag(v2f i) : SV_Target
{
                // get colours from the main scene render texture (camera's view)
    fixed4 col = tex2D(_MainTex, i.uv);

                // normalize color to the range [0, 1]
    //float3 colorNormalized = saturate(col.rgb);

                // LUT texture dimensions  (no height dimension, as the LUT used for my game are all 1 pixel tall.
    float cellWidth = 16.0;
    float lutWidth = 256.0;

                // calculate UV coordinates for LUT sampling
    float lutX = col.r * (cellWidth - 1.0) + col.g * (cellWidth - 1.0) * cellWidth;

                // LUT Y coordinate is always 0 since it's a 1-pixel high LUT
    float2 lutUV = float2(lutX / lutWidth, 0.0);

                // sample LUT for color
    fixed4 lutColor = tex2D(_LUTTex, lutUV);

                // convert LUT color to greyscale
    float3 greyscaleColor = ConvertToGrayscale(lutColor.rgb);

                // blend between LUT color and greyscale based on the saturation
    float3 finalColor = lerp(greyscaleColor.xxx, lutColor.rgb, _Saturation);

                //return final color with adjusted saturation
    return fixed4(finalColor, lutColor.a);
}
            ENDCG
        }
    }
}
