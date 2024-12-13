Shader "Unlit/Vortex"
{
    Properties 
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _Angle  ("Angle", Range(-10, 10)) = 45.0
        _Scale  ("Scale", Range(-10, 10)) = 45.0
    }

    SubShader 
    {
         //һЩ���ܴ���
        Cull Off
        //�ƹ�
        Lighting Off
        //���
        ZWrite Off
        //��
        Fog { Mode Off }

        Pass 
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Tools/Distort.hlsl"

            sampler2D _MainTex;
            float _Angle;
            float _Scale;
            float2 _Center;

            struct v2f 
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            // ������ɫ��
            v2f vert(appdata_full v)
            {
                v2f o;
                //ģ�Ϳռ�ת�ü��ռ�
                o.position = UnityObjectToClipPos (v.vertex);
                //��ȡuv
                o.uv = v.texcoord;
                return o;
            }

            //Ƭ����ɫ��
            float4 frag (v2f o) : SV_Target
            {
                return tex2D(_MainTex,DistortVortex(o.uv,_Angle,_Scale,_Center ));

                // return tex2D(_MainTex,NoiseWhite(o.uv ));
            }

            ENDCG
        }
    }
}
