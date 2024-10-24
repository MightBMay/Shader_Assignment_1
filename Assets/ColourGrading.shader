Shader"Custom/ColourGrading"
{
     Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}    // The main scene texture
        _Color ("Tint Color", Color) = (1, 1, 1, 1)   // The color to apply for grading
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

sampler2D _MainTex; // Scene texture
fixed4 _Color; // Grading color

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
                // Get the original color from the scene texture
    fixed4 col = tex2D(_MainTex, i.uv);

                // Apply the color grading by multiplying with the tint color
    col.rgb *= _Color.rgb; // Blend the scene color with the grading color

    return col;
}
            ENDCG
        }
    }
}