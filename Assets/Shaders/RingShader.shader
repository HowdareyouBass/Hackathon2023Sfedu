
Shader "Hidden/RingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
 
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
                float4 vertex : SV_POSITION;
            };
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            sampler2D _MainTex;
 
    fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(1,1,1,1);

				i.uv -= 0.5;

				float d = length(i.uv);

				float r = 0.3;

				float c = smoothstep(r, r - 0.1, d);

				col.rgb = c;
			    return col;
			}
            ENDCG
        }
    }
}