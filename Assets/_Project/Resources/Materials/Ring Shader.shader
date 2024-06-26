Shader "Custom/Ring Sheder"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _AreaColor ("Area Color", Color) = (1, 1, 1)
        _Center ("Center", Vector) = (0, 0, 0)
        _Radius ("Radius", Range(0, 500)) = 75
        _Border ("Border", Range(0, 100)) = 12
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        fixed3 _AreaColor;
        float3 _Center;
        float _Radius;
        float _Border;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            float dist = distance(_Center, IN.worldPos);

            if(dist > _Radius && dist < (_Radius + _Border))
            {
                o.Albedo = _AreaColor;
            }
            else
            {
                o.Albedo = c.rgb;
            }

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
