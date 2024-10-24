Shader "Testing/test1"
{
	Properties{
		_Colour ("Colours", Color) = (0.5,1,1,1)
		//_Emission("Emission Colour", Color) = (1,1,1,1)
		//_Normal ("Normal", Color) = (1,1,1,1)
	}
	SubShader{
		CGPROGRAM
		#pragma surface surf Lambert

		struct Input{
			float2 uvMainTex;
		};

		fixed4 _Colour;
		//fixed4 _Emission;
		//fixed4 _Normal;

		void surf(Input IN,inout SurfaceOutput o){
			o.Albedo = _Colour.rgb;
			//o.Emission = _Emission.rgb;
			//o.Normal = _Normal;
		}
		ENDCG
	}
	
}