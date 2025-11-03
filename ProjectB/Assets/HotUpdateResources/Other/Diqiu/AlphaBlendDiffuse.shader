Shader "My/AlphaBlendDiffuse"
{
    Properties
    {
		_Color("Color Tint（贴图染色）",Color)=(1,1,1,1)
        _MainTex ("Texture（主贴图）", 2D) = "white" {}
	    //bump为unity内置的法线纹理，当未配置任何法线纹理时，bump对应模型自带的法线信息
	    _NormalMap("Normal Map（法线贴图）",2D)="bump"{}
		_BumpScale("Bump Scale（凹凸程度）",float) = 1.0
		_Cutoff("Alpha（整体透明度）",range(0,1)) = 0.5
	}
	SubShader
	{
		//透明度混合需要定义的标签
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
 
		//1.Base Pass背面（顺序，透明物体先渲染背面再渲染正面）
		Pass
		{
			//提示此Pass为前向渲染中的Base Pass，计算环境光，自发光，平行光中的阴影，不计算其他叠加光照效果
			Tags{ "LightMode" = "ForwardBase" }
			//透明度混合需要关闭深度写入
			ZWrite Off
			//开启混合操作并设置混合类型，此处类型为透明度混合
			Blend SrcAlpha OneMinusSrcAlpha
			//透明物体要考虑双面渲染，第一个Pass只渲染背面，剔除正面
			Cull Front
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//Base Pass指令，用于得到对应的光照变量
			#pragma multi_compile_fwdbase
 
			#include "UnityCG.cginc"
			//包含接收阴影的宏
			#include "AutoLight.cginc"
            #include "Lighting.cginc"
 
			fixed4 _Color;
		    sampler2D _MainTex;
			//用于控制对应纹理的缩放和偏移，格式固定为xx_ST
			float4 _MainTex_ST;
			sampler2D _NormalMap;
			float4 _NormalMap_ST;
			float _BumpScale;
			fixed _Cutoff;//[0,1]范围内用fixed
 
            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal:NORMAL;
				float4 tangent:TANGENT;//与法线不同，w需要用于控制朝向
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
				//节约空间，xy分量存主贴图uv;zw存法线贴图的
                float4 uv : TEXCOORD0;
                float4 pos : SV_POSITION;//变量名为pos,有关阴影计算的宏中使用了此变量
				//寄存器中没法存矩阵，所以分别存矩阵的每一行
				float4 TtoW0 : TEXCOORD1;
				float4 TtoW1 : TEXCOORD2;
				float4 TtoW2 : TEXCOORD3;
				SHADOW_COORDS(4)//此阴影纹理坐标存储在TEXCOORD4中
            };
 
			//此处采用在世界空间中计算法线
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
				//用两个分量分别存储贴图的缩放和偏移
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.uv, _NormalMap);
 
				float3 worldPos= mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				//叉积计算第三个标准正交基轴向，w指示朝向的正负
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;
				//节约空间，顺便将世界空间中的顶点位置存在w分量中
				o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
				//计算阴影纹理坐标
				TRANSFER_SHADOW(o);
 
				return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
				//还原世界坐标
				float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
				//还原出矩阵，用于将纹理从顶点空间（切线空间）变为世界空间，统一计算
				float3x3 TtoW= float3x3(i.TtoW0.xyz, i.TtoW1.xyz, i.TtoW2.xyz);
				//得到世界空间中的光源方向和视线方向
				fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				//从zw分量中采样出法线并进行凹凸程度的缩放，但此时法线依然处于顶点空间（切线空间）
				fixed3 tanNormal = UnpackNormalWithScale(tex2D(_NormalMap, i.uv.zw), _BumpScale);
				//通过之前构造的变换矩阵将法线从顶点空间变换到世界空间
				fixed3 worldNormal = mul(TtoW, tanNormal);
				//采样主纹理并染色，得到反射率
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed3 albedo = col.rgb*_Color.rgb;
				//计算环境光
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
				//计算漫反射
				fixed3 diffuse = _LightColor0.rgb*albedo*saturate(dot(lightDir, worldNormal));
				//计算光照和阴影衰减值，结果为第一个参数
				UNITY_LIGHT_ATTENUATION(atten, i, worldPos);
 
				//返回计算结果
                return fixed4(ambient + diffuse * atten, col.a * _Cutoff);
            }
            ENDCG
        }
		//2.Base Pass正面
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }
 
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			//透明物体要考虑双面渲染，此Pass只渲染正面，剔除背面
			Cull Back
 
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
 
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
 
			fixed4 _Color;
		    sampler2D _MainTex;
		    float4 _MainTex_ST;
		    sampler2D _NormalMap;
		    float4 _NormalMap_ST;
		    float _BumpScale;
		    fixed _Cutoff;
 
		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal:NORMAL;
			float4 tangent:TANGENT;
			float2 uv : TEXCOORD0;
		};
 
		struct v2f
		{
			float4 uv : TEXCOORD0;
			float4 pos : SV_POSITION;
 
			float4 TtoW0 : TEXCOORD1;
			float4 TtoW1 : TEXCOORD2;
			float4 TtoW2 : TEXCOORD3;
			SHADOW_COORDS(4)
		};
 
		v2f vert(appdata v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
			o.uv.zw = TRANSFORM_TEX(v.uv, _NormalMap);
 
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
			fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
 
			fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;
 
			o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
			o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
			o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
 
			TRANSFER_SHADOW(o);
 
			return o;
		}
 
		fixed4 frag(v2f i) : SV_Target
		{
 
			float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
 
			float3x3 TtoW = float3x3(i.TtoW0.xyz, i.TtoW1.xyz, i.TtoW2.xyz);
 
			fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
			fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
 
			fixed3 tanNormal = UnpackNormalWithScale(tex2D(_NormalMap, i.uv.zw), _BumpScale);
 
			fixed3 worldNormal = mul(TtoW, tanNormal);
 
			fixed4 col = tex2D(_MainTex, i.uv);
			fixed3 albedo = col.rgb*_Color.rgb;
 
			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
 
			fixed3 diffuse = _LightColor0.rgb*albedo*saturate(dot(lightDir, worldNormal));
 
			UNITY_LIGHT_ATTENUATION(atten, i, worldPos);
 
			return fixed4(ambient + diffuse * atten, col.a * _Cutoff);
		}
			ENDCG
		}
		//3.Add Pass正常渲染
		Pass
		{
			//提示此Pass为前向渲染中的Add Pass，计算其他叠加光照效果，每个光源计算一次
			Tags{ "LightMode" = "ForwardAdd" }
 
			ZWrite Off
			Blend SrcAlpha One
			Cull Back
 
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			//Add Pass指令，用于得到对应的光照变量
            //#pragma multi_compile_fwdadd
			//阴影情况下使用:
			#pragma multi_compile_fwdadd_fullshadows
 
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
 
			fixed4 _Color;
		    sampler2D _MainTex;
		    float4 _MainTex_ST;
		    sampler2D _NormalMap;
		    float4 _NormalMap_ST;
		    float _BumpScale;
		    fixed _Cutoff;
 
		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal:NORMAL;
			float4 tangent:TANGENT;
			float2 uv : TEXCOORD0;
		};
 
		struct v2f
		{			
			float4 uv : TEXCOORD0;
			float4 pos : SV_POSITION;
 
			float4 TtoW0 : TEXCOORD1;
			float4 TtoW1 : TEXCOORD2;
			float4 TtoW2 : TEXCOORD3;
			SHADOW_COORDS(4)
		};
 
		v2f vert(appdata v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
			o.uv.zw = TRANSFORM_TEX(v.uv, _NormalMap);
 
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
			fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
 
			fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;
 
			o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
			o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
			o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
 
			TRANSFER_SHADOW(o);
 
			return o;
		}
 
		fixed4 frag(v2f i) : SV_Target
		{
 
			float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
 
			float3x3 TtoW = float3x3(i.TtoW0.xyz, i.TtoW1.xyz, i.TtoW2.xyz);
 
			fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
			fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
 
			fixed3 tanNormal = UnpackNormalWithScale(tex2D(_NormalMap, i.uv.zw), _BumpScale);
 
			fixed3 worldNormal = mul(TtoW, tanNormal);
 
			fixed4 col = tex2D(_MainTex, i.uv);
			fixed3 albedo = col.rgb*_Color.rgb;
 
			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
 
			fixed3 diffuse = _LightColor0.rgb*albedo*saturate(dot(lightDir, worldNormal));
 
			UNITY_LIGHT_ATTENUATION(atten, i, worldPos);
 
			return fixed4(ambient + diffuse * atten, col.a * _Cutoff);
		}
			ENDCG
		}
    }
	//无阴影
	//FallBack "Transparent/VertexLit"
	//强制产生阴影
	FallBack "VertexLit"
}