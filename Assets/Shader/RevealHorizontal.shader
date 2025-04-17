Shader "Custom/CenterReveal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RevealProgress ("Reveal Progress", Range(0, 1)) = 0
        _Softness ("Soft Edge", Range(0, 0.5)) = 0.1 // Làm mờ viền
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

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
            float _RevealProgress;
            float _Softness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Tính khoảng cách từ UV đến trung tâm (0.5)
                float distanceFromCenter = abs(i.uv.x - 0.5) * 2; // Chuẩn hóa 0 -> 1
                
                // Hiệu ứng reveal từ giữa ra
                float reveal = smoothstep(
                    _RevealProgress - _Softness,
                    _RevealProgress + _Softness,
                    1 - distanceFromCenter
                );
                
                col.a *= reveal;
                return col;
            }
            ENDCG
        }
    }
}