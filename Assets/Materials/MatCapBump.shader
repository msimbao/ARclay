﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MatCap Bumped" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_BumpMap ("Bumpmap (RGB)", 2D) = "bump" {}
		_MatCap ("MatCap (RGB)", 2D) = "white" {}
	}
	
	Subshader {
		Tags { "RenderType"="Opaque" }
		Fog { Color [_AddFog] }
		
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma exclude_renderers xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_fog_exp2
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f { 
					float4 pos : SV_POSITION;
					float2	uv : TEXCOORD0;
					float3	TtoV0 : TEXCOORD1;
					float3	TtoV1 : TEXCOORD2;
				};
				
				uniform float4 _BumpMap_ST;

				inline float3 DetectSmoothEdge(float3 edge, float3 junkNorm, float3 sharpNorm, float3 edge0, float3 edge1, float3 edge2) {

    edge = max(0, edge - 0.965) * 28;
    float allof = edge.r + edge.g + edge.b;

    float border = min(1, allof);

    float3 edgeN = edge0*edge.r + edge1*edge.g + edge2*edge.b;

    float junk = min(1, (edge.g*edge.b + edge.r*edge.b + edge.r*edge.g)*2);

    return   normalize((sharpNorm*(1 - border)+ border*edgeN)*(1 - junk) + junk*(junkNorm));   

}
				
				v2f vert (appdata_tan v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos (v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord,_BumpMap);
					
					TANGENT_SPACE_ROTATION;
					o.TtoV0 = mul(rotation, UNITY_MATRIX_IT_MV[0].xyz);
					o.TtoV1 = mul(rotation, UNITY_MATRIX_IT_MV[1].xyz);
					return o;
				}
				
				uniform float4 _Color;
				uniform sampler2D _BumpMap;
				uniform sampler2D _MatCap;
				
				float4 frag (v2f i) : COLOR
				{
					float3 normal = UnpackNormal(tex2D(_BumpMap, i.uv));
					// normal = normalize(normal);
					
					half2 vn;
					vn.x = dot(i.TtoV0, normal);
					vn.y = dot(i.TtoV1, normal);
					
					float4 matcapLookup = tex2D(_MatCap, vn*0.5 + 0.5);
					
					matcapLookup.a = 1;
					
					return _Color*matcapLookup*2;
				}
			ENDCG
		}
	}
}