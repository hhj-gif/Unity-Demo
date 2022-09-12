Shader "HHJShader/DieShader"
{
	Properties{
		//��дʱ����
		_Color ("Color", Color) = (1,1,1,1)
		_mainTex("mainTexture",2D)= "white"{}
		_speed("speed",Range(0,100)) = 1
		_acceleration("acceleration",Range(0,100)) = 0
	}
	SubShader{
		//д��Ⱦ�㷨
		Pass{
			CGPROGRAM

			//����������ɫ��
			#pragma vertex verta
			
			//��������������
			#pragma geometry geom
			//����Ƭ����ɫ��
			#pragma fragment frag
			#include"UnityCG.cginc"

			struct a2g{
				float4 pos:POSITION;//��������
				float2 uv:TEXCOORD;			//
			};
			
			struct g2v{
				float4 pos:POSITION;//��������
				float2 uv:TEXCOORD;			//
			};
		
			struct v2f
			{
				float4 pos:POSITION;//��������
				float2 uv:TEXCOORD;			//
			};
			
			sampler2D _mainTex;
			float _speed;
			float _acceleration;
			float _wight;		
			 g2v verta(a2g a){
				g2v o;
				o.pos = a.pos;
				o.uv = a.uv;
				return o;			
			}
			
			[maxvertexcount(1)]
			void geom(triangle g2v IN[3], inout PointStream<v2f> pointStream){
				v2f f;
				float3 a = IN[1].pos - IN[0].pos;
				float3 b = IN[2].pos - IN[0].pos;
				float3 dir = normalize(cross(a,b));
				float3 pos = (IN[0].pos + IN[1].pos + IN[2].pos)/3;
				float time = _Time.y;
				pos = pos + dir * (0.5*_acceleration * pow(time,2)+_speed*time);
				
				f.pos = UnityObjectToClipPos(pos);
				f.uv  = (IN[0].uv + IN[1].uv + IN[2].uv)/3;
				pointStream.Append(f);
			}
		
			float4 frag(v2f a):SV_TARGET
			{
				float tex = tex2D(_mainTex,a.uv);
				return tex;
			}
			ENDCG
		}
	}
}