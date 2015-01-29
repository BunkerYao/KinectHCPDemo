Shader "Custom/ColorSeperateBlend" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "black" {}
}
	SubShader {
		pass {
			Name "p_NoBlend"
			blend off
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform fixed4 _colorFactors;
			
			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 src = tex2D(_MainTex, i.uv);
				return fixed4(src.r * _colorFactors.r,
					  	  	  src.g * _colorFactors.g,
					  	  	  src.b * _colorFactors.b,
					  	  	  1.0f);
			}
			ENDCG
		}
		
		pass {
		    name "p_Blend"
			blend one one
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform fixed4 _colorFactors;
			
			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 src = tex2D(_MainTex, i.uv);
				return fixed4(src.r * _colorFactors.r,
					  	  	  src.g * _colorFactors.g,
					  	  	  src.b * _colorFactors.b,
					  	  	  1.0f);
			}
			ENDCG
		}
	} 
	FallBack off
}
