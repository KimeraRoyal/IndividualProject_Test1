Shader "Unlit/Sprite Render Texture"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _RenderTex ("Render Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Blend One OneMinusSrcAlpha
        
        Cull Off
        ZWrite Off

        Pass
        {
        CGPROGRAM
            #include "UnityCG.cginc"
            
            #pragma vertex vert
            #pragma fragment frag
            
			sampler2D _MainTex;
			float4 _MainTex_ST;
            
			sampler2D _RenderTex;
			float4 _RenderTex_ST;

			fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

			struct v2f
            {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};
            
			v2f vert(appdata v)
            {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _RenderTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
            {
				fixed4 col = tex2D(_RenderTex, i.uv);
				col *= _Color;
				col *= i.color;
				return col;
			}
        ENDCG
        }
    }
}
