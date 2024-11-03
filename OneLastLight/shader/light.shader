Shader "Unlit/oringin"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { 
        "Queue"="Transparent" 
        "RenderType"="Transparent" 
        }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            ZWrite Off 
            Stencil 
            { 
                Ref 1
                
                Pass Replace
            } 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color  : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color=v.color;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                // sample the texture
                // 获取纹理颜色
                fixed4 texColor = tex2D(_MainTex, i.uv);
                // 使用 _Color 乘以纹理颜色，实现类似 SpriteRenderer 的 Tint 效果
                fixed4 fincol=texColor * i.color;
                clip(fincol.a-0.01);
                return fincol;
            }
            ENDCG
        }
    }
}
