// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


#include "UnityCG.cginc"
#include "UnityGBuffer.cginc"
#include "UnityStandardUtils.cginc"

uniform float4 _LightColor0; //From UnityCG
uniform float4 _Color; //Use the above variables in here
uniform float4 _SpecColor;
uniform float _Shininess;
uniform float _PointSize;

struct appdata 
{
	float4 vertex : POSITION;
	float4 normal : NORMAL;
	fixed4 color: COLOR;
	float4 uv : TEXCOORD0;

};

struct v2g
{
	float4 vertex : SV_POSITION;
	float4 normal : NORMAL;
	fixed4 color : COLOR;
	float4 up : TEXCOORD0;
	float4 right : TEXCOORD1;

};

struct g2f
{
	float4 vertex : SV_POSITION;
	float4 normal : NORMAL;
	fixed4 color : COLOR;
	float2 uv :TEXCOORD0;
};

v2g Vertex(appdata input)
{
	v2g output;
	output.vertex = input.vertex;
	output.color = input.color;
	output.normal = input.normal;
	output.up = input.uv;

	float3 view = normalize(UNITY_MATRIX_IT_MV[2].xyz);
	float3 upvec = normalize(UNITY_MATRIX_IT_MV[1].xyz);
	float3 R = normalize(cross(view, upvec));
	output.up = float4(upvec*1, 0);
	output.right = float4(R * 1, 0);
	
	return output;
}


// float4 Fragment(v2g input) : COLOR {
// if ( (input.up.x*input.up.x + input.up.y*input.up.y) > .1) {
// discard;
// }
// return input.color;
// }

[maxvertexcount(4)]
void Geometry(triangle v2g input[3], inout TriangleStream<g2f>outputStream) {
	g2f output;

	float size = _PointSize;

	float4 average = (input[0].vertex + input[1].vertex + input[2].vertex) /3;

	output.vertex = UnityObjectToClipPos(average + (-input[0].up + input[0].right) * size );
	output.color = input[0].color;
	output.normal = input[0].normal;
	output.uv = float2(-1.0f, 1.0f);
	outputStream.Append(output);

	output.vertex = UnityObjectToClipPos(average + (-input[0].up - input[0].right) * size);
	output.color = input[0].color;
	output.normal = input[0].normal;
	output.uv = float2(-1.0f, -1.0f);
	outputStream.Append(output);

	output.vertex = UnityObjectToClipPos(average + (+input[0].up + input[0].right) * size);
	output.color = input[0].color;
	output.normal = input[0].normal;
	output.uv = float2(1.0f, 1.0f);
	outputStream.Append(output);

	output.vertex = UnityObjectToClipPos(average + (+input[0].up - input[0].right) * size);
	output.color = input[0].color;
	output.normal = input[0].normal;
	output.uv = float2(1.0f, -1.0f);
	outputStream.Append(output);

	outputStream.RestartStrip();
}




fixed4 Fragment(g2f input) : SV_Target
{
	// float uvlen = input.uv.x*input.uv.x + input.uv.y*input.uv.y;
	// if ( uvlen > 1) {
	// discard;}
	if ( (input.uv.x*input.uv.x + input.uv.y*input.uv.y) > 1) {
discard;
	}
	float3 normalDirection = normalize(input.normal);
	float3 viewDirection = normalize(_WorldSpaceCameraPos - input.vertex.xyz);

	float3 vert2LightSource = _WorldSpaceLightPos0.xyz - input.vertex.xyz;
	float oneOverDistance = 1.0 / length(vert2LightSource);
	float attenuation = lerp(1.0, oneOverDistance, _WorldSpaceLightPos0.w); //Optimization for spot lights. This isn't needed if you're just getting started.
	float3 lightDirection = _WorldSpaceLightPos0.xyz - input.vertex.xyz * _WorldSpaceLightPos0.w;

	float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb; //Ambient component
	float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection)); //Diffuse component
	float3 specularReflection;
	if (dot(input.normal, lightDirection) < 0.0) //Light on the wrong side - no specular
	{
		specularReflection = float3(0.0, 0.0, 0.0);
		}
	else
	{
		//Specular component
		specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
	}

	float3 color = (ambientLighting + diffuseReflection) + specularReflection;
	return float4(color, 1.0);
}

