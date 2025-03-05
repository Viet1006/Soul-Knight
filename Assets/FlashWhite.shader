Shader "Custom/FlashWhiteShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _FlashAmount ("Flash Amount", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off        // Hiển thị cả hai mặt
        ZWrite Off      // Tránh lỗi render đè lên sprite khác

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float _FlashAmount;

            v2f vert (appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                return OUT;
            }

            fixed4 frag (v2f IN) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, IN.texcoord);
                col.rgb = lerp(col.rgb, float3(1,1,1), _FlashAmount);
                return col;
            }
            ENDCG
        }
    }
}
