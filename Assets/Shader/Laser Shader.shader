Shader "Unlit/LaserShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CoreColor ("Core Color", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (0,1,1,1)
        _Width ("Width", Range(0, 0.5)) = 0.1
        _PulseSpeed ("Pulse Speed", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
            float4 _CoreColor;
            float4 _GlowColor;
            float _Width;
            float _PulseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            float rand(float2 co) {
                return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Tính toán khoảng cách từ điểm hiện tại đến trung tâm (0.5,0.5)
                float2 center = float2(0.5, 0.5);
                float dist = abs(i.uv.y - 0.5);

                
                // Core laser (phần sáng nhất ở giữa)
                float core = smoothstep(_Width, 0.0, dist);
                fixed4 col = _CoreColor * core;
                
                // Glow xung quanh
                float glow = smoothstep(_Width*2.0, 0.0, dist);
                col += _GlowColor * glow * 0.5;
                
                // Hiệu ứng nhấp nháy
                col *= 0.8 + 0.2 * sin(_Time.y * _PulseSpeed);
                
                // Alpha blending
                col.a = max(core, glow * 0.7);
                
                return col;
            }
            ENDCG
        }
    }
}