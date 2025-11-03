// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "UV-Custom/hailang"
{
	Properties
	{
		[HDR]_Color("Color ", Color) = (0,0,0,0)
		_FresnelRange("FresnelRange", Range( 0 , 5)) = 2
		_WaveTexA("WaveTex A", 2D) = "white" {}
		_Float1("Float 1", Float) = 0
		_Float2("Float 2", Float) = 0
		_WaveTexB("WaveTex B", 2D) = "white" {}
		_Float3("Float 3", Float) = 0
		_Float4("Float 4", Float) = 0
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
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float3 viewDir;
			half3 worldNormal;
		};

		uniform half4 _Color;
		uniform sampler2D _WaveTexA;
		uniform half _Float1;
		uniform half _Float2;
		uniform sampler2D _WaveTexB;
		uniform half _Float3;
		uniform half _Float4;
		uniform half _FresnelRange;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half2 appendResult19 = (half2(_Float1 , _Float2));
			half2 panner51 = ( 1.0 * _Time.y * appendResult19 + i.uv_texcoord);
			half2 appendResult30 = (half2(_Float3 , _Float4));
			half2 panner31 = ( 1.0 * _Time.y * appendResult30 + i.uv_texcoord);
			half temp_output_47_0 = ( tex2D( _WaveTexA, panner51 ).r * tex2D( _WaveTexB, panner31 ).r );
			o.Emission = ( _Color * i.vertexColor * temp_output_47_0 ).rgb;
			half3 ase_worldNormal = i.worldNormal;
			half dotResult4 = dot( i.viewDir , ase_worldNormal );
			o.Alpha = ( _Color.a * i.vertexColor.a * temp_output_47_0 * pow( ( 1.0 - saturate( abs( dotResult4 ) ) ) , _FresnelRange ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;100;1366;607;574.4253;602.0754;1.806015;True;False
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;2;52.15636,174.9545;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;3;31.89037,333.2153;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;39;-44.10343,-225.1877;Inherit;False;Property;_Float2;Float 2;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-78.16759,-319.0304;Inherit;False;Property;_Float1;Float 1;4;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;4;277.4506,270.4239;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-149.9015,120.4283;Inherit;False;Property;_Float4;Float 4;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-138.8611,24.74541;Inherit;False;Property;_Float3;Float 3;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;146.887,-268.3456;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-98.59678,-96.95765;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;5;424.6608,292.1295;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;30;26.25465,59.83298;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;63.2744,-426.8951;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;6;566.6608,296.1295;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;31;231.1919,-23.80556;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;51;309.9842,-304.7472;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;14;510.7521,-246.6841;Inherit;True;Property;_WaveTexA;WaveTex A;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;42;506.7236,-41.50792;Inherit;True;Property;_WaveTexB;WaveTex B;6;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;565.5818,381.29;Inherit;False;Property;_FresnelRange;FresnelRange;2;0;Create;True;0;0;0;False;0;False;2;2;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;716.6606,295.1295;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;851.0375,-90.74049;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;840.5036,-369.1213;Inherit;False;Property;_Color;Color ;0;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;8;871.0811,299.2091;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;10;1655.527,-11.14433;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;1134.321,-147.5894;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;1106.985,18.30802;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1436.001,-168.7073;Half;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;UV-Custom/hailang;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;19;0;38;0
WireConnection;19;1;39;0
WireConnection;5;0;4;0
WireConnection;30;0;40;0
WireConnection;30;1;41;0
WireConnection;6;0;5;0
WireConnection;31;0;29;0
WireConnection;31;2;30;0
WireConnection;51;0;16;0
WireConnection;51;2;19;0
WireConnection;14;1;51;0
WireConnection;42;1;31;0
WireConnection;7;0;6;0
WireConnection;47;0;14;1
WireConnection;47;1;42;1
WireConnection;8;0;7;0
WireConnection;8;1;9;0
WireConnection;12;0;11;0
WireConnection;12;1;10;0
WireConnection;12;2;47;0
WireConnection;13;0;11;4
WireConnection;13;1;10;4
WireConnection;13;2;47;0
WireConnection;13;3;8;0
WireConnection;0;2;12;0
WireConnection;0;9;13;0
ASEEND*/
//CHKSM=FB075CFACBD812B4D3A079479E9E16591CAA4ADC