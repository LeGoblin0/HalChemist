Shader "Custom/Hit"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"   "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade


        sampler2D _MainTex;
        float _HitCol = 0;

        struct Input
        {
            float2 uv_MainTex;
        };


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Emission = c.rgb + _HitCol;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
