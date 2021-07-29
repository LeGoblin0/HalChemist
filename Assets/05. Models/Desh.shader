Shader "Custom/Desh"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Desh("데쉬 컬러",Color)=(1,1,1,1)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque"   "Queue" = "Transparent" }
        LOD 200

        cull off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade

        sampler2D _MainTex;
        fixed4 _Desh;

        struct Input
        {
            float2 uv_MainTex;
        };


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Emission = _Desh;
            if (c.a == 0)o.Alpha = 0;
            else             o.Alpha = _Desh.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
