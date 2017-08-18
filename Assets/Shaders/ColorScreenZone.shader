// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Camera/ColorScreenZone"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_TransitionTex("Transition Texture", 2D) = "white" {}
	_Color("Screen Color", Color) = (1,1,1,1)
		_Cutoff("Cutoff", Range(0, 1)) = 0
	}

		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float2 uv1 : TEXCOORD1;
		float4 vertex : SV_POSITION;
	};

	float4 _MainTex_TexelSize;

	v2f simplevert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		o.uv1 = v.uv;

#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			o.uv1.y = 1 - o.uv1.y;
#endif

		return o;
	}

	sampler2D _MainTex;
	sampler2D _TransitionTex;
	float _Cutoff;
	fixed4 _Color;

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 transit = tex2D(_TransitionTex, i.uv1);

	fixed4 col = tex2D(_MainTex, i.uv);

	if (transit.b < _Cutoff || _Cutoff == 1)
		col = lerp(col, _Color, _Color.a);

	return col;
	}
		ENDCG
	}
	}
}
