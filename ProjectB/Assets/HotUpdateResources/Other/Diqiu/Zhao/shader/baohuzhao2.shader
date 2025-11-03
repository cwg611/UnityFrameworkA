// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:35079,y:32575,varname:node_4013,prsc:2|emission-8819-OUT;n:type:ShaderForge.SFN_Tex2d,id:9773,x:32880,y:32874,ptovrint:False,ptlb:node_9773,ptin:_node_9773,varname:node_9773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3807-OUT;n:type:ShaderForge.SFN_TexCoord,id:2063,x:32370,y:32766,varname:node_2063,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:718,x:32394,y:32947,varname:node_718,prsc:2,v1:3,v2:3;n:type:ShaderForge.SFN_Multiply,id:3807,x:32625,y:32906,varname:node_3807,prsc:2|A-2063-UVOUT,B-718-OUT;n:type:ShaderForge.SFN_Color,id:4871,x:33326,y:32740,ptovrint:False,ptlb:node_4871,ptin:_node_4871,varname:node_4871,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2058824,c2:0.4742391,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6269,x:33285,y:32538,ptovrint:False,ptlb:node_6269,ptin:_node_6269,varname:node_6269,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a767af214b5cedb4781ed8cb7ecff186,ntxv:0,isnm:False|UVIN-7432-UVOUT;n:type:ShaderForge.SFN_Panner,id:7432,x:33097,y:32562,varname:node_7432,prsc:2,spu:0,spv:0.4|UVIN-2718-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2718,x:32804,y:32559,varname:node_2718,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9625,x:33551,y:32619,varname:node_9625,prsc:2|A-6269-RGB,B-4871-RGB;n:type:ShaderForge.SFN_Add,id:8819,x:34672,y:33275,varname:node_8819,prsc:2|A-3884-OUT,B-4743-OUT,C-2181-OUT;n:type:ShaderForge.SFN_Multiply,id:3884,x:33963,y:32497,varname:node_3884,prsc:2|A-1704-OUT,B-9625-OUT,C-1217-RGB;n:type:ShaderForge.SFN_Slider,id:1704,x:33394,y:32487,ptovrint:False,ptlb:saoguanglaingdu,ptin:_saoguanglaingdu,varname:node_1704,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7861245,max:2;n:type:ShaderForge.SFN_Multiply,id:4541,x:33260,y:33128,varname:node_4541,prsc:2|A-9773-RGB,B-2978-RGB;n:type:ShaderForge.SFN_Color,id:2978,x:32746,y:33205,ptovrint:False,ptlb:node_2978,ptin:_node_2978,varname:node_2978,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.12429,c3:0.9485294,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4954,x:33567,y:32122,ptovrint:False,ptlb:node_4954,ptin:_node_4954,varname:node_4954,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2589-UVOUT;n:type:ShaderForge.SFN_Panner,id:2589,x:33377,y:32122,varname:node_2589,prsc:2,spu:0.2,spv:-0.3|UVIN-9531-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9531,x:33155,y:32122,varname:node_9531,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6571,x:33851,y:32040,varname:node_6571,prsc:2|A-7269-UVOUT,B-4954-G;n:type:ShaderForge.SFN_Panner,id:7269,x:33567,y:31923,varname:node_7269,prsc:2,spu:-0.12,spv:-0.2|UVIN-1270-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1270,x:33284,y:31906,varname:node_1270,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:1217,x:34069,y:32040,ptovrint:False,ptlb:node_1217,ptin:_node_1217,varname:node_1217,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6571-OUT;n:type:ShaderForge.SFN_Slider,id:5768,x:32801,y:33657,ptovrint:False,ptlb:node_5768,ptin:_node_5768,varname:node_5768,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2000743,max:1;n:type:ShaderForge.SFN_Tex2d,id:9383,x:32578,y:33416,ptovrint:False,ptlb:node_9383,ptin:_node_9383,varname:node_9383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:480,x:33071,y:33367,varname:node_480,prsc:2|A-2978-RGB,B-9383-RGB;n:type:ShaderForge.SFN_Add,id:4743,x:34270,y:33249,varname:node_4743,prsc:2|A-2576-OUT,B-273-OUT;n:type:ShaderForge.SFN_Multiply,id:273,x:34059,y:33438,varname:node_273,prsc:2|A-480-OUT,B-5768-OUT;n:type:ShaderForge.SFN_Multiply,id:2576,x:33579,y:33097,varname:node_2576,prsc:2|A-4541-OUT,B-5768-OUT;n:type:ShaderForge.SFN_Fresnel,id:8823,x:33790,y:33762,varname:node_8823,prsc:2|EXP-2573-OUT;n:type:ShaderForge.SFN_Vector1,id:2573,x:33505,y:33859,varname:node_2573,prsc:2,v1:4;n:type:ShaderForge.SFN_Multiply,id:5330,x:34077,y:33726,varname:node_5330,prsc:2|A-9383-RGB,B-8823-OUT;n:type:ShaderForge.SFN_Power,id:8483,x:34295,y:33773,varname:node_8483,prsc:2|VAL-5330-OUT,EXP-5945-OUT;n:type:ShaderForge.SFN_Vector1,id:5945,x:34009,y:33964,varname:node_5945,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:6309,x:34349,y:33990,ptovrint:False,ptlb:bianyuanyanse,ptin:_bianyuanyanse,varname:node_6309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:2181,x:34677,y:33803,varname:node_2181,prsc:2|A-8483-OUT,B-6309-RGB;proporder:9773-4871-6269-1704-2978-4954-1217-5768-9383-6309;pass:END;sub:END;*/

Shader "Shader Forge/baohuzhao" {
    Properties {
        _node_9773 ("node_9773", 2D) = "white" {}
        _node_4871 ("node_4871", Color) = (0.2058824,0.4742391,1,1)
        _node_6269 ("node_6269", 2D) = "white" {}
        _saoguanglaingdu ("saoguanglaingdu", Range(0, 2)) = 0.7861245
        _node_2978 ("node_2978", Color) = (0,0.12429,0.9485294,1)
        _node_4954 ("node_4954", 2D) = "white" {}
        _node_1217 ("node_1217", 2D) = "white" {}
        _node_5768 ("node_5768", Range(0, 1)) = 0.2000743
        _node_9383 ("node_9383", 2D) = "white" {}
        _bianyuanyanse ("bianyuanyanse", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_9773; uniform float4 _node_9773_ST;
            uniform float4 _node_4871;
            uniform sampler2D _node_6269; uniform float4 _node_6269_ST;
            uniform float _saoguanglaingdu;
            uniform float4 _node_2978;
            uniform sampler2D _node_4954; uniform float4 _node_4954_ST;
            uniform sampler2D _node_1217; uniform float4 _node_1217_ST;
            uniform float _node_5768;
            uniform sampler2D _node_9383; uniform float4 _node_9383_ST;
            uniform float4 _bianyuanyanse;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_393 = _Time + _TimeEditor;
                float2 node_7432 = (i.uv0+node_393.g*float2(0,0.4));
                float4 _node_6269_var = tex2D(_node_6269,TRANSFORM_TEX(node_7432, _node_6269));
                float2 node_2589 = (i.uv0+node_393.g*float2(0.2,-0.3));
                float4 _node_4954_var = tex2D(_node_4954,TRANSFORM_TEX(node_2589, _node_4954));
                float2 node_6571 = ((i.uv0+node_393.g*float2(-0.12,-0.2))+_node_4954_var.g);
                float4 _node_1217_var = tex2D(_node_1217,TRANSFORM_TEX(node_6571, _node_1217));
                float2 node_3807 = (i.uv0*float2(3,3));
                float4 _node_9773_var = tex2D(_node_9773,TRANSFORM_TEX(node_3807, _node_9773));
                float4 _node_9383_var = tex2D(_node_9383,TRANSFORM_TEX(i.uv0, _node_9383));
                float3 emissive = ((_saoguanglaingdu*(_node_6269_var.rgb*_node_4871.rgb)*_node_1217_var.rgb)+(((_node_9773_var.rgb*_node_2978.rgb)*_node_5768)+((_node_2978.rgb*_node_9383_var.rgb)*_node_5768))+(pow((_node_9383_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),4.0)),1.0)*_bianyuanyanse.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
