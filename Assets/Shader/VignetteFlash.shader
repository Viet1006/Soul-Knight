Shader "Custom/UI_RedVignette"
{
    Properties
    {
        _EdgeColor ("Edge Color", Color) = (1,0,0,1)
        _EdgeSize ("Edge Size", Range(0, 0.5)) = 0.2
        _EdgeSmooth ("Edge Smoothness", Range(0, 1)) = 0.3
        _Intensity ("Effect Intensity", Range(0, 1)) = 1
    }

    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "Queue"="Transparent+1"  // Cao hơn các UI thông thường
            "IgnoreProjector"="True" 
            "PreviewType"="Plane"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest Always
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ UNITY_UI_CLIP_RECT
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            float4 _EdgeColor;
            float _EdgeSize;
            float _EdgeSmooth;
            float _Intensity;
            float4 _ClipRect;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Clip rectangle (cho UI)
                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(i.uv.xy - _ClipRect.xy)) * 1000);
                clip(min(m.x, m.y) - 0.001);
                #endif
                
                float2 centerVec = i.uv - 0.5;
                float distanceFromCenter = length(centerVec) * 2.0;
                
                float alpha = smoothstep(1.0 - _EdgeSize - _EdgeSmooth, 
                                      1.0 - _EdgeSize, 
                                      distanceFromCenter);
                
                alpha = saturate(alpha * _Intensity) * i.color.a;
                return fixed4(_EdgeColor.rgb * i.color.rgb, alpha);
            }
            ENDCG
        }
    }
}