Shader "HTW/BurnShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_BurnColor ("BurnColor", Color) = (0,0,0,1)
		_BurnTex ("BurnTexture (RGB)", 2D) = "black" {}
		_BurnMapTex("BurnMapTexture (RGB)", 2D) = "black" {}
		_BurnFactor ("BurnFactor", Range(0,1.1)) = 0.0
		_InterpolationOffset("InterpolationOffset", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BurnTex;
		sampler2D _BurnMapTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BurnTex;
			float2 uv_BurnMapTex;
		};

		half _Glossiness;
		half _Metallic;
		half _BurnFactor;
		half _InterpolationOffset;
		fixed4 _Color;
		fixed4 _BurnColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 b = tex2D (_BurnMapTex, IN.uv_BurnMapTex) ;
			fixed4 m = tex2D(_BurnTex, IN.uv_BurnTex) * _BurnColor;
			
			// evaluate and interpolate 
			float _value = smoothstep(_BurnFactor - _InterpolationOffset, _BurnFactor, b.r);
			o.Albedo = lerp(c.rgb, m.rgb, _value);
			
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//FallBack "Diffuse"
}
