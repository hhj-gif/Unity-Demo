Shader "HHJShader/DieShader02"
{
	Properties{
		//填写时属性
		_mainTex("mainTexture",2D)= "white"{}
		_noiseTex("noiseTexture",2D)= "white"{}
		_theshold("theshold",Range(0,1))=1
	}
	SubShader{
		//写渲染算法
		Pass{
			CGPROGRAM

			//声明顶点着色器
			#pragma vertex vert
			//声明片面着色器
			#pragma fragment frag
			#include"UnityCG.cginc"

			struct a2v{
				float4 pos:POSITION;//语义修饰
				float2 uv:TEXCOORD;			//
			};
			
			struct v2f
			{
				float4 pos:POSITION;//语义修饰
				float2 uv:TEXCOORD;			//
			};
			
			sampler2D _mainTex;
			sampler2D _noiseTex;
			float _theshold;
				
			 v2f vert(a2v a){
				v2f o;
				o.pos =  UnityObjectToClipPos(a.pos);
				o.uv = a.uv;
				return o;			
			}
			
		
			float4 frag(v2f a):SV_TARGET
			{
				float4 tex = tex2D(_mainTex,a.uv);
				float4 noise = tex2D(_noiseTex,a.uv);
				if(noise.r < _theshold){
					discard;
				}
				else if(noise.r = _theshold){
					tex = float4(1,0,1,1);
				}
				return tex;
			}
			ENDCG
		}
	}
}