Shader "Custom/ToonyColor"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RampThreshold ("RampThreshold", Range(0,1)) = 0.5
        _RampSmooth ("RampSmooth", float) = 0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Toon addshadow fullforwardshadows exclude_path:defered execlude_path:prepass

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        fixed4 _Color;
        sampler2D _MainTex;
        float _RampThreshold;
        float _RampSmooth;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        inline fixed4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        {
            half3 normalDir = normalize(s.Normal);
            float ndl = max(0, dot(normalDir, lightDir));
            half ramp = smoothstep(_RampThreshold - _RampSmooth * 0.5, _RampThreshold + _RampSmooth * 0.5, ndl);
            ramp *= atten;

            fixed3 lightColor = _LightColor0.rgb;

            fixed4 color;
            fixed3 diffuse = s.Albedo * lightColor * ramp;

            color.rgb = diffuse;
            color.a = s.Alpha;
            return color;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb * _Color.rgb;
            o.Alpha = c.a * _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
