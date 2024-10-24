Shader "Custom/StandardPBRSpecular"
{
   Properties{
		  _Colour("Colour", Color) =(1,1,1,1)
		  _SpecularColour("Specular Colour", Color) = (1,1,1,1)
		  _MainTex("Main Texture", 2D) = "white"{}
		  _MetallicTex("Metallic (r)", 2D) = "white"{}
		  _Metallic("Metallic", Range(0,1)) = 0
   }

   SubShader{
		  Tags{
			  "Queue" = "Geometry"
		  }

		  CGPROGRAM
		  #pragma surface surf  StandardSpecular
		  sampler2D _MainTex;
		  sampler2D _MetallicTex ;
		  half _Metallic;
		  fixed4 _Colour;
		  fixed4 _SpecularColour;

		  struct Input{
			  float2 uv_MainTex; 
			  float2 uv_MetallicTex;
		  };

		  void surf(Input IN, inout SurfaceOutputStandardSpecular o){
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;//* _Colour;
				o.Smoothness = tex2D(_MetallicTex, IN.uv_MetallicTex).r;
				o.Specular = _Metallic * _SpecularColour;
		  }
		  ENDCG
   }

}