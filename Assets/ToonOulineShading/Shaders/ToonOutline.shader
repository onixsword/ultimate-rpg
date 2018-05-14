Shader "CGLab/ToonOutline" 
{
	Properties 
	{
		_Color("Albedo Color", Color) = (1, 1, 1, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_NormalTex("Normal Tex", 2D) = "bump"{}
		_NormalAmount("Normal Amount", Range(-1.0, 3.0)) = 1.0
		_RampTex("Ramp Texture", 2D) = "white"{}
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_Outline("Outline", Range(0.002, 0.1)) = 0.003
		_RimColor("Rim Color", Color) = (0, 0.5, 0.5, 0.0)
		_RimPower("Rim Power", Range(0.5, 8.0)) = 3.0
		_Exposure("Exposure", Range(0.0, 10.0)) = 1.0
		_Specular("Specular", Range(0, 1)) = 0
		_OccludeColor("Occlusion Color", Color) = (0,0,1,1)
	}
	SubShader 
	{
		Tags{ "Queue" = "Geometry+5" }
		LOD 200

		Pass
		{
			ZWrite Off
			Blend One Zero
			ZTest Greater
			Color[_OccludeColor]
		}
			// Vertex lights
		Pass
		{
			Tags{ "LightMode" = "Vertex" }
			ZWrite On
			Lighting On
			SeparateSpecular On
			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			// Emission [_PPLAmbient]
			}
			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
				Combine texture * primary DOUBLE, texture * constant
			}
		}

		CGPROGRAM
			#pragma surface surf ToonRamp

			float4 _Color;
			sampler2D _RampTex;
			sampler2D _MainTex;
			sampler2D _NormalTex;
			sampler2D _SpecularTex;
			float _NormalAmount;
			float4 _RimColor;
			float _RimPower;
			half rim;
			float _Exposure;
			half _Specular;

			half4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten) {

				//Shiness
				half3 sh = normalize(lightDir + viewDir);
				half sdiff = max(0, dot(s.Normal, lightDir));
				float snh = max(0, dot(s.Normal, sh));
				float spec = pow(snh, 48.0) * _Specular;
				float3 cSpec = (s.Albedo * _LightColor0.rgb * sdiff + _LightColor0.rgb * spec) * atten;

				//Ramp
				float diff = dot(s.Normal, lightDir);
				float h = diff * 0.5 + 0.5;
				float2 rh = h;
				float3 ramp = tex2D(_RampTex, rh).rgb * _Exposure;
				float3 cRamp = s.Albedo * _LightColor0.rgb * (ramp);

				float4 c;
				c.rgb = cRamp + cSpec;
				c.a = s.Alpha;
				return c;
			}

			struct Input {
				float2 uv_MainTex;
				float2 uv_NormalTex;
				float3 viewDir; INTERNAL_DATA
			};

			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color.rgb;
				o.Specular = _Specular;
				o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
				o.Normal *= float3(_NormalAmount, _NormalAmount, 1);
				rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
				o.Emission = _RimColor.rgb * pow(rim, _RimPower);
			}
		ENDCG
			Pass{
				Cull Front
				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag

					#include "UnityCG.cginc"

					struct appdata {
						float4 vertex : POSITION;
						float3 normal : NORMAL;
					};

					struct v2f {
						float4 pos : SV_POSITION;
						fixed4 color : COLOR;
					};

					float _Outline;
					float4 _OutlineColor;

					v2f vert(appdata v) {
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
						float2 offset = TransformViewToProjection(norm.xy);
						o.pos.xy += offset * o.pos.z * _Outline;
						o.color = _OutlineColor;
						return o;
					}

					fixed4 frag(v2f i) : SV_Target{
						return i.color;
					}
				ENDCG
		}
	}
	FallBack "Diffuse"
}
