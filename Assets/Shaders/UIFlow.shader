Shader "UI/FlowLight"
{
    Properties
    {
        [PerRendererData] _MainTex("Main Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

            // 流光参数
            _FlowColor("Flow Color", Color) = (1,1,1,1)
            _FlowWidth("Flow Width", Range(0, 1)) = 0.2
            _FlowSpeed("Flow Speed", Range(0, 10)) = 1
            _FlowAngle("Flow Angle", Range(0, 360)) = 45
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    float3 worldPos : TEXCOORD1;
                };

                sampler2D _MainTex;
                fixed4 _Color;
                fixed4 _FlowColor;
                float _FlowWidth;
                float _FlowSpeed;
                float _FlowAngle;

                v2f vert(appdata_t IN)
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(IN.vertex);
                    OUT.texcoord = IN.texcoord;
                    OUT.color = IN.color * _Color;
                    OUT.worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
                    return OUT;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    // 基础颜色采样
                    fixed4 col = tex2D(_MainTex, IN.texcoord) * IN.color;

                // 计算流光方向
                float angleRad = radians(_FlowAngle);
                float2 flowDir = float2(cos(angleRad), sin(angleRad));

                // UV 动画
                float move = _Time.y * _FlowSpeed;
                float flow = dot(IN.worldPos.xy, flowDir) + move;

                // 生成流光条纹
                float stripe = sin(flow * 10);
                stripe = smoothstep(1 - _FlowWidth, 1, abs(stripe));

                // 混合流光颜色
                fixed4 finalColor = lerp(col, _FlowColor, stripe * _FlowColor.a);

                // 保持原有透明度
                finalColor.a = col.a;
                return finalColor;
            }
            ENDCG
        }
        }
}