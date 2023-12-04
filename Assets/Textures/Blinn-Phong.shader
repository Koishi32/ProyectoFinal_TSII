Shader "MotoresVideojuegos/Blinn-Phong"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Kao ("Ambient Color", Color) = (1,1,1,1)
        _Kdo ("Diffuse Color", Color) = (1,1,1,1)
        _Kso ("Specular Color", Color) = (1, 1, 1, 1)
        _Q ("q", Float) = 10 //Shininess
        _DeltaTime ("Delta Time", Float) = 0.0
        _MiRangoFloat ("Rango",Range(0.0,6.28)) = 0.0
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };

            uniform float4 _LightColor0;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float  _DeltaTime;
            float4 _Kao;
            float4 _Kdo;
            float4 _Kso;
            float _Q;
            float _MiRangoFloat;

            v2f vert (appdata v)
            {
                v2f o;
    
                float frequency = 2.0; // Frecuencia de las ondas
                float waveX = sin(_DeltaTime + v.vertex.y * frequency) * _MiRangoFloat;
                float waveZ = cos(_DeltaTime + v.vertex.x * frequency) * _MiRangoFloat;
                v.vertex.x += waveX;
                v.vertex.z += waveZ;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.normal = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;

                float3 N = normalize(i.normal);
                float3 V = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);

                // Ambient component
                float4 Ka = UNITY_LIGHTMODEL_AMBIENT * _Kao;

                // Diffuse component
                float3 vert2LightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
                float oneOverDistance = 1.0 / length(vert2LightSource);
                float attenuation = lerp(1.0, oneOverDistance, _WorldSpaceLightPos0.w); 
                float3 L = _WorldSpaceLightPos0.xyz - i.posWorld.xyz * _WorldSpaceLightPos0.w;
                float3 Kd = attenuation * _LightColor0.rgb * _Kdo.rgb * max(0.0, dot(N, L));

                // Specular component
                float3 Ks;
                if (dot(i.normal, L) < 0.0) 
                {
                    Ks = float3(0.0, 0.0, 0.0);
                }
                else
                {
                    float3 H = (L + V) / length(L + V);
                    Ks = attenuation * _LightColor0.rgb * _Kso.rgb * pow(max(0.0, dot(N, H)), _Q);
                }

                col = (Ka + float4(Kd, 1.0)) * tex2D(_MainTex, i.uv) + float4(Ks, 1.0);

                return col;
            }
            ENDCG
        }
    }
}
