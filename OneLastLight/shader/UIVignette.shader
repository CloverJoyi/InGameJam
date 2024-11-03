Shader "Hidden/AzhaoVignette"
{
	CGINCLUDE
#include "UnityCG.cginc"

	sampler2D _MainTex;
	float4 _MainTex_TexelSize;

	float _VignetteIntensity;
	float _VignetteSmoothness;

	half4 fragVignette(v2f_img i) : SV_Target
	{
		half4 col = tex2D(_MainTex, i.uv);


		//暗角
		float2 screenUV = abs(i.uv - float2(0.5f,0.5f))*_VignetteIntensity;
		screenUV = pow(saturate(screenUV), _VignetteSmoothness);
		float dist = length(screenUV);
		float vfactor = pow(saturate(1 - dist * dist), _VignetteSmoothness);
		col.rgb *= vfactor;

		return col;
	}


		ENDCG
		Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_VignetteIntensity("VignetteIntensity", Range(0, 3)) = 1
		_VignetteSmoothness("VignetteSmoothness", Range(0, 5)) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		//0校色
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment fragVignette            


			ENDCG
		}
	}
}
