
Shader "Hidden/RingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _R1 ("Radius 1", Float) = 0.5
        _R2 ("Radius 2", Float) = 0.2
    }
    SubShader
    {
 
        Tags { "RenderType"="Transparent"
            "Queue"="Transparent" }

        Pass
        {        
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
            };
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
        float4 _Color;
        float _R1;
        float _R2;
 
    fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(1,1,1,1);

				i.uv -= 0.5;

				float d = length(i.uv);

                col.rgb = _Color;
                col.a = 0;
                
                if (d > _R2 && d < _R1)
                {
                    col.a = 1;
                }

			    return col;
			}
            ENDCG
        }
    }
}