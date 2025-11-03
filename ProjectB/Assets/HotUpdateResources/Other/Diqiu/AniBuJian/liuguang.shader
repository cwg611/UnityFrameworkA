// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "liuguang"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[Toggle(_KEYWORD0_ON)] _Keyword0("Keyword 0", Float) = 0
		_Color("Color", Color) = (1,1,1,0)
		_Rotation("Rotation", Float) = 0
		_Xspeed("Xspeed", Float) = 0
		_Yspeed("Yspeed", Float) = 0
		_Contrast("Contrast", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _KEYWORD0_ON
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _Contrast;
		uniform sampler2D _TextureSample0;
		uniform float _Xspeed;
		uniform float _Yspeed;
		uniform float _Rotation;
		uniform float4 _Color;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 appendResult18 = (float4(_Xspeed , _Yspeed , 0.0 , 0.0));
			float cos9 = cos( ( (0.0 + (_Rotation - 0.0) * (6.28 - 0.0) / (1.0 - 0.0)) / 360.0 ) );
			float sin9 = sin( ( (0.0 + (_Rotation - 0.0) * (6.28 - 0.0) / (1.0 - 0.0)) / 360.0 ) );
			float2 rotator9 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos9 , -sin9 , sin9 , cos9 )) + float2( 0.5,0.5 );
			float2 panner19 = ( 1.0 * _Time.y * appendResult18.xy + rotator9);
			float4 tex2DNode3 = tex2D( _TextureSample0, panner19 );
			o.Emission = ( i.vertexColor * pow( _Contrast , tex2DNode3.r ) * _Color ).rgb;
			#ifdef _KEYWORD0_ON
				float staticSwitch7 = tex2DNode3.a;
			#else
				float staticSwitch7 = tex2DNode3.r;
			#endif
			o.Alpha = ( i.vertexColor.a * _Color.a * staticSwitch7 );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
48.8;79.2;1523;656;2130.054;660.4927;2.056335;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-1413.003,-202.7081;Inherit;False;Property;_Rotation;Rotation;4;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1212.179,-39.95518;Inherit;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;0;False;0;False;360;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;12;-1240.403,-203.5081;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;6.28;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1012.668,-25.5705;Inherit;False;Property;_Xspeed;Xspeed;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1142.137,-334.2373;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;14;-1031.814,-153.1473;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1012.668,64.42947;Inherit;False;Property;_Yspeed;Yspeed;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;18;-856.9681,1.529506;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RotatorNode;9;-862.728,-208.9234;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;19;-631.7098,-70.64027;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-332.7642,-246.721;Inherit;False;Property;_Contrast;Contrast;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-428.7802,-92.29037;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;4;-127.7802,129.7096;Inherit;False;Property;_Color;Color;3;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;2;-67.7802,-344.2904;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;21;-66.99609,-134.0891;Inherit;False;False;2;0;FLOAT;0;False;1;COLOR;1,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;7;-91.3732,-14.29037;Inherit;False;Property;_Keyword0;Keyword 0;2;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;194.2198,-146.2904;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;200.9933,20.88387;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;393,-187;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;liuguang;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;11;0
WireConnection;14;0;12;0
WireConnection;14;1;15;0
WireConnection;18;0;16;0
WireConnection;18;1;17;0
WireConnection;9;0;10;0
WireConnection;9;2;14;0
WireConnection;19;0;9;0
WireConnection;19;2;18;0
WireConnection;3;1;19;0
WireConnection;21;0;22;0
WireConnection;21;1;3;0
WireConnection;7;1;3;1
WireConnection;7;0;3;4
WireConnection;8;0;2;0
WireConnection;8;1;21;0
WireConnection;8;2;4;0
WireConnection;1;0;2;4
WireConnection;1;1;4;4
WireConnection;1;2;7;0
WireConnection;0;2;8;0
WireConnection;0;9;1;0
ASEEND*/
//CHKSM=E322CF2966B987F6A12391E0FD6B741493CBEFB6