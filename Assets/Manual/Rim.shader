Shader "Custom/Rim"
{
    Properties{
        _Colour("colour", Color) = (1,1,1,1)
        _RimColour ("Rim Colour", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(0.5,8))=3.0
    }

    SubShader{
        CGPROGRAM
        #pragma surface surf Lambert
        struct Input{
            float3 viewDir;
        };
        float4 _Colour;
        float4 _RimColour;
        float _RimPower;

        void surf (Input IN, inout SurfaceOutput o){
            half rim = 1-  saturate (dot( normalize(IN.viewDir),o.Normal));
            o.Albedo = _Colour.rgb;
            o.Emission = _RimColour.rgb * pow(rim, _RimPower); 
        } 
        ENDCG
    }
    FallBack "Diffuse"
}
