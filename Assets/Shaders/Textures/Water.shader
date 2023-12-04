Shader "ClaseTS/Water"
{
    Properties
    {
        _MainTex ("Etiqueta", 2D) = "white" {}
        _MiEntero ("Entero", Integer) = 1
        _MiFlotante ("Flotante", Float) = 0.5
        _MiRangoFloat ("Rango",Range(0.0,6.28)) = 0.0
        _MiText ("Textura2", 2D) = "red" {}
        _MiArrayText ("ArrayText", 2DArray) = "" {}
        _Mi3DText ("Textura 3D", 3D) = "" {}
        _MiCubo ("Cubo", Cube ) = "" {}
        _MiArrayCubo ("Cube Array", CubeArray) = "" {}
        _MiColor ("Mi Color", Color) = (1.0,0.0,0.0,1)
        _MiVector ("Mi Vector", Vector) = (0.5,0.5,0.25,1)
        _DeltaTime ("DeltaTime", Float) = 0.0
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _MiEntero;
            float4 _MiColor;
            float _MiRangoFloat;
            float _DeltaTime;


            v2f vert (appdata v)
            {
                v2f o;

                // Parámetros de las ondas
                float frequency = 2.0; // Frecuencia de las ondas

                // Generar ondas en la superficie
                float waveX = sin(_DeltaTime + v.vertex.y * frequency) * _MiRangoFloat;
                float waveZ = cos(_DeltaTime + v.vertex.x * frequency) * _MiRangoFloat;

                v.vertex.x += waveX;
                v.vertex.z += waveZ;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

                fixed4 col = _MiColor * tex2D(_MainTex, i.uv);

                //fixed4 text = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
