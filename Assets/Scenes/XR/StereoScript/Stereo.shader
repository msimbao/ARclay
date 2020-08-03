Shader "ShaderMan/Stereo"
{

	Properties{
		_MainTex("MainTex", 2D) = "white"{}
		_scale("Scale", Range(0.3, 0.49)) = 0.375
		_ipdShift("IPD shift", Range(-0.03, 0.03)) = 0.0
	}

	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

		Pass
		{
			ZTest Always
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct VertexInput {
			fixed4 vertex : POSITION;
			fixed2 uv : TEXCOORD0;
			fixed4 tangent : TANGENT;
			fixed3 normal : NORMAL;
			//VertexInput
			};


			struct VertexOutput {
			fixed4 pos : SV_POSITION;
			fixed2 uv : TEXCOORD0;
			//VertexOutput
			};

			//Variables

			
			sampler2D _MainTex;
			uniform float _scale = 0.400;
			uniform float _ipdShift = 0.005;


			VertexOutput vert(VertexInput v)
			{
			VertexOutput o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv;
			//VertexFactory
			return o;
			}
			fixed4 frag(VertexOutput i) : SV_Target{

			{
			fixed dx = (0.5 - _scale) / 2.0;
			fixed dy = (0.8 - _scale) / 2.0;
			// Normalized pixel coordinates (from 0 to 1)
			fixed2 uv = i.uv;
			fixed3 col;
			if (uv.x < dx + _ipdShift || uv.y < dy
				|| uv.x > 1.0 - dx - _ipdShift || uv.y > 1.0 - dy
				|| (uv.x > dx + _ipdShift + _scale && uv.x < dx + _scale + 2.0 * dx - _ipdShift)) {
				col = fixed3(0.0, 0.0, 0.0);
				return fixed4(col, 1.0);
			}
			if (uv.x < 0.5)
				uv.x = (uv.x - dx - _ipdShift) * 1.0 / _scale;
			else
				uv.x = (uv.x - 0.5 - dx + _ipdShift) * 1.0 / _scale;
			uv.y = (uv.y - dy) * 0.5 / _scale;

			col = tex2D(_MainTex, uv);
			return fixed4(col, 1.0);
			}
			}ENDCG
		}
	}
}