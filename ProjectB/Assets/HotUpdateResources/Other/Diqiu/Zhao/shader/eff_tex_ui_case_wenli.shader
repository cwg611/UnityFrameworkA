// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33839,y:31619,varname:node_4013,prsc:2|emission-1773-OUT;n:type:ShaderForge.SFN_Tex2d,id:4734,x:32893,y:31225,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:179,x:33183,y:31671,varname:node_179,prsc:2|A-4734-A,B-6392-RGB,C-4445-RGB,D-1929-OUT;n:type:ShaderForge.SFN_VertexColor,id:6392,x:32882,y:31859,varname:node_6392,prsc:2;n:type:ShaderForge.SFN_Color,id:4445,x:32815,y:32018,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:1929,x:32882,y:32168,varname:node_1929,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:5021,x:33171,y:31862,varname:node_5021,prsc:2|A-6392-A,B-4445-A;n:type:ShaderForge.SFN_Tex2d,id:1620,x:32330,y:31711,ptovrint:False,ptlb:LiuGuang,ptin:_LiuGuang,varname:node_236,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8950-OUT;n:type:ShaderForge.SFN_Color,id:1386,x:32330,y:31920,ptovrint:False,ptlb:LiuGuang_Color,ptin:_LiuGuang_Color,varname:node_4457,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:950,x:32672,y:31709,varname:node_950,prsc:2|A-1620-RGB,B-1386-RGB,C-5470-OUT,D-1386-A;n:type:ShaderForge.SFN_ValueProperty,id:5470,x:32540,y:31986,ptovrint:False,ptlb:LiuGuang_QiangDu,ptin:_LiuGuang_QiangDu,varname:node_2024,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_TexCoord,id:4998,x:31321,y:31632,varname:node_4998,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:7402,x:31749,y:31635,varname:node_7402,prsc:2|A-4998-UVOUT,B-9377-OUT,C-2124-OUT;n:type:ShaderForge.SFN_Subtract,id:5026,x:31484,y:31777,varname:node_5026,prsc:2|A-4998-UVOUT,B-2020-OUT;n:type:ShaderForge.SFN_Divide,id:9377,x:31619,y:31976,varname:node_9377,prsc:2|A-5026-OUT,B-9478-OUT;n:type:ShaderForge.SFN_Clamp01,id:8950,x:32099,y:31728,varname:node_8950,prsc:2|IN-2958-OUT;n:type:ShaderForge.SFN_Lerp,id:2958,x:31925,y:31728,varname:node_2958,prsc:2|A-7402-OUT,B-2020-OUT,T-9478-OUT;n:type:ShaderForge.SFN_Vector1,id:9968,x:30785,y:31768,varname:node_9968,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Time,id:7492,x:31167,y:32320,varname:node_7492,prsc:2;n:type:ShaderForge.SFN_Fmod,id:176,x:31631,y:32314,varname:node_176,prsc:2|A-4964-OUT,B-4964-OUT;n:type:ShaderForge.SFN_Vector1,id:4964,x:31421,y:32465,varname:node_4964,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:8231,x:31421,y:32314,varname:node_8231,prsc:2|A-7492-TSL,B-1168-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1168,x:31167,y:32496,ptovrint:False,ptlb:LiuGuang_Speed,ptin:_LiuGuang_Speed,varname:node_7478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_SwitchProperty,id:7645,x:32338,y:31398,ptovrint:False,ptlb:WuAlpha,ptin:_WuAlpha,varname:node_7279,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1620-A,B-1620-R;n:type:ShaderForge.SFN_Multiply,id:237,x:32683,y:31411,varname:node_237,prsc:2|A-7645-OUT,B-8466-R;n:type:ShaderForge.SFN_Tex2d,id:481,x:31321,y:31455,ptovrint:False,ptlb:UVNiuQu,ptin:_UVNiuQu,varname:node_8091,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2124,x:31595,y:31312,varname:node_2124,prsc:2|A-481-R,B-1750-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1750,x:30774,y:31512,ptovrint:False,ptlb:NiuQu_QiangDu,ptin:_NiuQu_QiangDu,varname:node_7323,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:2020,x:31150,y:31747,varname:node_2020,prsc:2|A-8190-OUT,B-9968-OUT;n:type:ShaderForge.SFN_Multiply,id:8190,x:30996,y:31614,varname:node_8190,prsc:2|A-1750-OUT,B-4810-OUT;n:type:ShaderForge.SFN_Vector1,id:4810,x:30785,y:31635,varname:node_4810,prsc:2,v1:0.15;n:type:ShaderForge.SFN_SwitchProperty,id:9478,x:31619,y:32172,ptovrint:False,ptlb:AniUVScale,ptin:_AniUVScale,varname:node_3370,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-643-OUT,B-176-OUT;n:type:ShaderForge.SFN_Slider,id:643,x:31167,y:32166,ptovrint:False,ptlb:UVScale,ptin:_UVScale,varname:node_5589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.6127513,max:10;n:type:ShaderForge.SFN_Tex2d,id:8466,x:32515,y:31183,ptovrint:False,ptlb:LiuGuang_Mask,ptin:_LiuGuang_Mask,varname:node_6025,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2725,x:33436,y:31622,varname:node_2725,prsc:2|A-179-OUT,B-5021-OUT,C-3740-OUT;n:type:ShaderForge.SFN_Multiply,id:462,x:32893,y:31538,varname:node_462,prsc:2|A-237-OUT,B-950-OUT;n:type:ShaderForge.SFN_Multiply,id:3740,x:33296,y:31425,varname:node_3740,prsc:2|A-4734-RGB,B-462-OUT;n:type:ShaderForge.SFN_Multiply,id:1773,x:33593,y:31811,varname:node_1773,prsc:2|A-2725-OUT,B-1745-R;n:type:ShaderForge.SFN_Tex2d,id:1745,x:33371,y:31982,ptovrint:False,ptlb:node_1745,ptin:_node_1745,varname:node_1745,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6271-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6271,x:33149,y:32023,varname:node_6271,prsc:2,uv:0,uaff:False;proporder:4734-4445-1620-1386-5470-7645-8466-1168-481-1750-9478-643-1745;pass:END;sub:END;*/

Shader "Shader Forge/eff_tex_ui_case_wenli" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _LiuGuang ("LiuGuang", 2D) = "white" {}
        _LiuGuang_Color ("LiuGuang_Color", Color) = (0.5,0.5,0.5,1)
        _LiuGuang_QiangDu ("LiuGuang_QiangDu", Float ) = 3
        [MaterialToggle] _WuAlpha ("WuAlpha", Float ) = 0
        _LiuGuang_Mask ("LiuGuang_Mask", 2D) = "white" {}
        _LiuGuang_Speed ("LiuGuang_Speed", Float ) = 5
        _UVNiuQu ("UVNiuQu", 2D) = "white" {}
        _NiuQu_QiangDu ("NiuQu_QiangDu", Float ) = 0
        [MaterialToggle] _AniUVScale ("AniUVScale", Float ) = 0.6127513
        _UVScale ("UVScale", Range(-1, 10)) = 0.6127513
        _node_1745 ("node_1745", 2D) = "white" {}
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform sampler2D _LiuGuang; uniform float4 _LiuGuang_ST;
            uniform float4 _LiuGuang_Color;
            uniform float _LiuGuang_QiangDu;
            uniform fixed _WuAlpha;
            uniform sampler2D _UVNiuQu; uniform float4 _UVNiuQu_ST;
            uniform float _NiuQu_QiangDu;
            uniform fixed _AniUVScale;
            uniform float _UVScale;
            uniform sampler2D _LiuGuang_Mask; uniform float4 _LiuGuang_Mask_ST;
            uniform sampler2D _node_1745; uniform float4 _node_1745_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_2020 = ((_NiuQu_QiangDu*0.15)+0.5);
                float node_4964 = 1.0;
                float _AniUVScale_var = lerp( _UVScale, fmod(node_4964,node_4964), _AniUVScale );
                float4 _UVNiuQu_var = tex2D(_UVNiuQu,TRANSFORM_TEX(i.uv0, _UVNiuQu));
                float2 node_8950 = saturate(lerp((i.uv0+((i.uv0-node_2020)/_AniUVScale_var)+(_UVNiuQu_var.r*_NiuQu_QiangDu)),float2(node_2020,node_2020),_AniUVScale_var));
                float4 _LiuGuang_var = tex2D(_LiuGuang,TRANSFORM_TEX(node_8950, _LiuGuang));
                float4 _LiuGuang_Mask_var = tex2D(_LiuGuang_Mask,TRANSFORM_TEX(i.uv0, _LiuGuang_Mask));
                float4 _node_1745_var = tex2D(_node_1745,TRANSFORM_TEX(i.uv0, _node_1745));
                float3 emissive = (((_MainTex_var.a*i.vertexColor.rgb*_TintColor.rgb*2.0)*(i.vertexColor.a*_TintColor.a)*(_MainTex_var.rgb*((lerp( _LiuGuang_var.a, _LiuGuang_var.r, _WuAlpha )*_LiuGuang_Mask_var.r)*(_LiuGuang_var.rgb*_LiuGuang_Color.rgb*_LiuGuang_QiangDu*_LiuGuang_Color.a))))*_node_1745_var.r);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
