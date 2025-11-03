// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "My/Single Texture HalfLambertDiffuse" {
	

	Properties {
		_Color ("Color Tint", Color) = (1, 1, 1, 1)
		_ColorGlitter ("_ColorGlitter", Color) = (0, 0, 0, 0)
		_MainTex ("Main Tex", 2D) = "white" {}
		_Specular ("Specular", Color) = (1, 1, 1, 1)
		_Gloss ("Gloss", Range(8.0, 256)) = 20
	}
	SubShader {		
		Pass { 
			Tags { "LightMode"="ForwardBase" }
		
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag


					#pragma multi_compile_fwdbase//阴影相关  非常重要，不然没法正确计算阴影
			#include "AutoLight.cginc"//阴影相关


			#include "Lighting.cginc"
			
			fixed4 _Color;
			fixed4 _ColorGlitter;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Specular;
			float _Gloss;
			
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
				float2 uv : TEXCOORD2;

				SHADOW_COORDS(3)//阴影相关	
			};
			
			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				// Or just call the built-in function
//				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				TRANSFER_SHADOW(o);//阴影相关					

				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target {
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
				

				fixed halfLambert = dot(worldNormal, worldLightDir) * 0.5 + 0.5;//半兰伯特
				fixed shadow = SHADOW_ATTENUATION(i);//阴影相关

				// Use the texture to sample the diffuse color
				fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _Color.rgb;
				
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo * shadow;
				
				fixed3 diffuse = _LightColor0.rgb * albedo * halfLambert;////半兰伯特
				
				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				fixed3 halfDir = normalize(worldLightDir + viewDir);
				fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(worldNormal, halfDir)), _Gloss);
				
				//specular.rgb+=_ColorGlitter;

				return fixed4(ambient + diffuse+_ColorGlitter.rgb, 1.0);
				//return fixed4(ambient + diffuse, 1.0);
			}
			
			ENDCG
		}
	} 
	FallBack "Specular"


}
