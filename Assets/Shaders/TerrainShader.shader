Shader "Custom/TerrainShader" {
	Properties {		
		_Depth ("_Depth", Float) = 180
		
		_SnowHeight ("Snow Height", Range(0, 1)) = 0
		_RockHeight ("Rock Height", Range(0, 1)) = 0
		_GrassHeight ("Grass Height", Range(0, 1)) = 0
		_BeachHeight ("Beach Height", Range(0, 1)) = 0
		_WaterHeight ("Water Height", Range(0, 1)) = 0

		_SnowColor ("Snow Texture", Color) = (0, 0, 0, 0)
		_RockColor ("Rock Texture", Color) = (0, 0, 0, 0)
		_GrassColor ("Grass Texture", Color) = (0, 0, 0, 0)
		_BeachColor ("Beach Texture", Color) = (0, 0, 0, 0)
		_WaterColor ("Water Texture", Color) = (0, 0, 0, 0)
		
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		fixed4 _SnowColor;
		fixed4 _RockColor;
		fixed4 _GrassColor;
		fixed4 _BeachColor;
		fixed4 _WaterColor;

		struct Input {
			float2 uv_SnowTex;
			float2 uv_RockTex;
			float2 uv_GrassTex;
			float2 uv_BeachTex;
			float2 uv_WaterTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		float _Depth;
		float _SnowHeight;
		float _RockHeight;
		float _GrassHeight;
		float _BeachHeight;
		float _WaterHeight;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {			

			fixed4 col = fixed4(0, 0, 0 ,0);

			if(IN.worldPos.y < _WaterHeight * _Depth) {
				col = _WaterColor;
			}
			else if(IN.worldPos.y < _BeachHeight * _Depth) {
				col = _BeachColor;
			}
			else if(IN.worldPos.y < _GrassHeight * _Depth) {
				col = _GrassColor;
			}
			else if(IN.worldPos.y < _RockHeight * _Depth) {
				col = _RockHeight;
			}	
			else if(IN.worldPos.y < _SnowHeight * _Depth) {
				col = _SnowHeight;
			}		
					
			o.Albedo = col.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = col.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
