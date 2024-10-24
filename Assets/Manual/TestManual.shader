Shader "Testing/Tutorial" // name Shader and give it a path
{
    //create variables to access in the editor.
    Properties { 
        _myColor ("Example Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white"{}
        /*
        _myColor ("Example Color", Color) = (1,1,1,1)
        _myRange ("Example Range", Range(0,5)) = 1
        _myTex ("Example Texture", 2D) = "white" {}
        _myTex2 ("Example Texture2", 2D) = "gray" {}
        _myCube ("Example Cube", CUBE) = "" {}
        _myFloat ("Example Float", Float) = 0.5
        _myVector ("Example Vector", Vector) = (0.5,1,1,1)
        */
    }

    SubShader { // start Shader

      CGPROGRAM// idfk why we need this
        #pragma surface surf Lambert 
        
        fixed4 _myColor; // 
        sampler2D _MainTex;

        //input given to surf()
        struct Input {
            float2 uv_MainTex;
            float3 worldRefl;
        };
        
        // what to do for suface. 
        void surf (Input IN, inout SurfaceOutput o) {
            
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _myColor;
            //o.Emission = texCUBE (_myCube, IN.worldRefl).rgb;
          }
      
      ENDCG
    }
    Fallback "Diffuse"
}

