// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Head"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Lava ("Lava", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Speed ("Speed", Float) = 1
        _Speed2 ("Speed 2", Float) = 2
        _DistortSpeed ("Distort Speed", Float) = 2
        _Distort ("Distort Amount", Float) = 1
        _CameraOffset ("Camera Offset", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.5

        sampler2D _MainTex;
        sampler2D _Lava;

        struct Input
        {
            float2 uv_MainTex;
            half3 objPos;
            half3 camPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Speed, _Speed2, _DistortSpeed, _Distort, _CameraOffset;

        float remap (float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.objPos = mul(unity_ObjectToWorld, float4(0,0,0,-1));
            o.camPos = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1.0));
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            half3 a = normalize(IN.objPos - half3(1, 0, 0));
            half3 b = normalize(IN.objPos - half3(IN.camPos.x, 0, IN.camPos.z));
            float angle = atan2(dot(a, b), cross(a, b)).y;
            // Albedo comes from a texture tinted by color
            float distort = tex2D (_Lava, IN.uv_MainTex + float2(0, _Time.x * _DistortSpeed)).b;
            float d = remap(distort, 0, 1, -0.8, 1) * _Distort;
            float blobs = tex2D (_Lava, IN.uv_MainTex + float2(_CameraOffset * angle, d + _Time.x * _Speed)).g;
            float blobs2 = tex2D (_Lava, IN.uv_MainTex + float2(-_CameraOffset * angle + 0.5, d + _Time.x * _Speed2)).g;
            float resting = tex2D (_Lava, IN.uv_MainTex).r;
            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            

            o.Albedo = saturate(remap(resting + blobs + blobs2, 0.3, 0.35, 0, 1)) * _Color;
            // o.Albedo = resting + blobs;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
