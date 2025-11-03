Shader "MyShader/RimShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Diffuse("Diffuse", Color) = (1,1,1,1)
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.0, 10)) = 0.3
		_RimIntensity("Rim Intensity", Range(0.0, 50)) = 3
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "Lighting.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal:NORMAL;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float3 worldNormal : NORMAL;
					float4 worldPos : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed4 _Diffuse;
				float4 _RimColor;
				float _RimPower;
				float _RimIntensity;


				v2f vert(appdata v)
				{
					v2f o;
					float4 worldPos;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);

					//转化为世界空间下的顶点坐标
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
					//转化为世界空间下的法线向量
					o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

					//----------------半兰伯特模型---------------------------------------

					fixed4 color = tex2D(_MainTex, i.uv);

				//归一化世界法线
				fixed3 worldNormal = normalize(i.worldNormal);
				//光照方向归一化
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

				//环境光计算
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * _Diffuse.xyz;

				//半兰伯特模型计算公式
				fixed halfLambert = dot(worldNormal, worldLightDir) * 0.5 + 0.5;

				//最终漫反射结果
				fixed3 diffuse = _LightColor0.xyz * _Diffuse.xyz * halfLambert + ambient;

				//----------------边缘光计算----------------------------------------

				//获取摄像机世界空间下的视角的方向，并归一化
				float3 worldViewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);

				//通过点乘来判断该点位置是否在边缘，越是边缘夹角越接近90度
				float rim = 1 - max(0, dot(worldViewDir, worldNormal));

				//计算边缘光
				fixed3 rimColor = _RimColor * pow(rim, 1 / _RimPower) * _RimIntensity;

				//最终光照结果
				fixed3  finalColor = color.rgb * diffuse + rimColor;

				return fixed4(finalColor,1);
				}
				ENDCG
			}
		}
			FallBack "Diffuse"
}