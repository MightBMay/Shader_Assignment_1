Shader "Custom/CelShading"
{
    Properties
    {
        _Colour ("Color", Color) = (1,1,1,1)
        _MainTex("Ramp Texture", 2D) = "white"{}
        _RampTex("Ramp Texture", 2D) = "white"{}
        _Brightness("Brightness", Range(0,1))=0
        _Cutoff1("cutoff1", Range(0,1)) =0 
         _Cutoff2("cutoff2", Range(0,1)) =0 
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf ToonRamp

        float4 _Colour;
        fixed _Cutoff1;
        fixed _Cutoff2;
        fixed _Brightness;
        sampler2D _RampTex;
        sampler2D _MainTex;


        float4 LightingToonRamp(SurfaceOutput s, fixed2 lightDir, fixed atten){
            float dif = dot (s.Normal,lightDir);
            float h = dif * .5 +.5;
            float2 rh = h;
            float3 ramp = tex2D(_RampTex, rh).rgb;
            float4 c;
            c.rgb = s.Albedo*_LightColor0.rgb*(ramp); 
            c.a = s.Alpha;
            return c*_Brightness;
        }

        struct Input{
           float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o){
            o.Albedo = _Colour.rgb;
        }
        ENDCG
        
       
    }

}
