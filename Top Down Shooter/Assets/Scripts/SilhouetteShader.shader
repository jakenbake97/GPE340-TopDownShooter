Shader "Custom/Silhouette" {
    Properties //shader properties to be edited in the inspector
    { 
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _ShadeColor ("Silhouette Color", Color) = (0.47, 0.48, 0.46, 1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        GrabPass
        {
            "_BackgroundTexture"
        }

        ZWrite Off
        ZTest Greater


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
                float4 grabPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            fixed4 _ShadeColor;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target //color the character gray if they are obstructed
            {
                float4 color = _ShadeColor;
                return color;
            }
            ENDCG
        }
        ZWrite On
        ZTest LEqual 

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o) //standard surface shader with color, metallic, smoothness, and alpha properties
        { 
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        
        ENDCG
    }
    FallBack "Diffuse"
}
