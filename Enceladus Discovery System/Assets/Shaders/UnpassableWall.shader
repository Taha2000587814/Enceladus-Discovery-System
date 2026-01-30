Shader "Custom/UnpassableWall"
{
	Properties
	{
		_BaseColor("Base Color", Color) = (0.5,0.5,0.5,1)
		_GlitchColor("Glitch Color", Color) = (1,0,0,1)
		_Severity("Severity", Range(0,1)) = 1
		_NoiseScale("Noise Scale", Float) = 8
		_DistortStrength("Distort Strength", Float) = 0.2
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

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
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _Severity;
			float _NoiseScale;
			float _DistortStrength;

			v2f vert(appdata v)
			{
				v2f o;
				float noise = sin(v.vertex.x * _NoiseScale + _Time.y * 40) * cos(v.vertex.y * _NoiseScale + _Time.y * 35);
				v.vertex.xyz += float3(noise, noise, -noise) * _DistortStrength * _Severity;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv + float2(cos(_Time.y * 25), sin(_Time.y * 30)) * 0.05 * _Severity;
				return o;
			}

			fixed4 _BaseColor;
			fixed4 _GlitchColor;

			fixed4 frag(v2f i) : SV_Target
			{
				float glitch = abs(sin(_Time.y * 60));
				fixed4 col = lerp(_BaseColor, _GlitchColor, glitch * _Severity);

				// RGB channel separation
				col.g = lerp(col.g, col.r, glitch * 0.5);
				col.b = lerp(col.b, col.g, glitch * 0.5);

				col.a = 1.0; // stays solid
				return col;
			}
			ENDCG
		}
	}
}
