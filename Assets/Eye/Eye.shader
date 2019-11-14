Shader "Unlit/Eye"
{
    Properties
    {
        _ScleraColor ("Eye Color", Color) = (1,1,1,1)
        [HDR] _IrisColor ("Iris Color", Color) = (0,0,1,1)
        _PupilColor ("Pupil Color", Color) = (0,0,0,1)
        _LidColor ("Lid Color", Color) = (0.5,0.5,0.1,1)

        _ScleraSqueeze ("Sclera Squeeze", Range(0, 1)) = 0
        _IrisCenter ("Iris Center", Vector) = (0.5, 0.5, 0, 0)
        _IrisRadius ("Iris Radius", Range(0, 1)) = 0.25
        _IrisSqueeze ("Iris Squeeze", Range(0, 1)) = 0
        _PupilRadius ("Pupil Radius", Range(0, 1)) = 0.125

        _EyelidColor ("Eyelid Color", Color) = (0.86, 0.68, 0.68, 1.0)
        _EyelidBias ("Eyelid Bias", Range(-0.5, 0.5)) = 0
        _EyelidBlink ("Eyelid Blink", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            fixed4 _ScleraColor, _IrisColor, _PupilColor, _EyelidColor;
            float2 _IrisCenter;
            float _ScleraSqueeze, _IrisSqueeze, _IrisRadius, _PupilRadius, _EyelidBias, _EyelidBlink;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 drawCircle(float4 fragColor, float2 uv, float radius, float2 center, fixed4 color, float squeeze, bool inv)
            {
                float squeezeDirection = (uv.x < center.x) ? squeeze : -squeeze;
                squeezeDirection *= radius;

                if (distance(uv, center + float2(squeezeDirection, 0)) <= radius)
                {
                    return inv ? fragColor : color;
                }
                
                return inv ? color : fragColor;
            }

            float4 drawEyelid(float4 fragColor, float2 uv, float4 color, float squeeze, float bias, float blink)
            {
                float2 center = float2(0.5, 0.5) + float2(bias * blink, 0);
                
                return drawCircle(fragColor, uv, 0.5, center, color, squeeze + blink, true);
            }


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 fragColor = float4(0,0,0,0);
                // sample the texture
                fragColor = drawCircle(fragColor, i.uv, 0.5, float2(0.5, 0.5), _ScleraColor, _ScleraSqueeze, false);
                fragColor = drawCircle(fragColor, i.uv, _IrisRadius, _IrisCenter, _IrisColor, _IrisSqueeze, false);
                fragColor = drawCircle(fragColor, i.uv, _PupilRadius, _IrisCenter, _PupilColor, _IrisSqueeze, false);
                fragColor = drawEyelid(fragColor, i.uv, _EyelidColor, _ScleraSqueeze, _EyelidBias, _EyelidBlink);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, fragColor);
                return fragColor;
            }
            ENDCG
        }
    }
}
