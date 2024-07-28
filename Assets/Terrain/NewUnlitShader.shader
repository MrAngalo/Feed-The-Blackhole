Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Blocks[128];

            int getBlock(int2 pos)
            {
                int index = (pos.y << 5) + pos.x;
                int subIndex = index >> 3;
                int position = (index & 0x7) << 2;
                uint blockGroup = asuint(_Blocks[subIndex]);
                return (blockGroup >> position) & 0xF;
            }

            float2 getBlockOffset(int block) {
                int u = block & 0x3;
                int v = 3 - ((block >> 2) & 0x3);
                return float2(u*0.25, v*0.25);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                int2 pos = int2(i.uv);
                float2 fracUV = i.uv - pos;
                int block = getBlock(pos);
                float2 offset = getBlockOffset(block);
                float2 blockUV = 0.25 * fracUV + offset;
                fixed4 col = tex2D(_MainTex, blockUV);
                return col;
            }
            ENDCG
        }
    }
}
