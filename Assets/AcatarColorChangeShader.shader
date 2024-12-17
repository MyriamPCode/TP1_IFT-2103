// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/AvatarColorWithMask"
{
    Properties
    {
        _MainTex ("Avatar Texture", 2D) = "white" { }
        _SkinColor ("Skin Color", Color) = (1, 1, 1, 1)
        _ClothesColor ("Clothes Color", Color) = (1, 1, 1, 1)
        _HairColor ("Hair Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Tags { "Queue" = "Overlay" }
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha

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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _SkinColor;
            float4 _ClothesColor;
            float4 _HairColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Charge l'image de l'avatar
                half4 baseColor = tex2D(_MainTex, i.uv);

                // Modifie la couleur en fonction de la couleur de base de l'avatar
                // Si la couleur du pixel dans l'image est proche de la couleur peau (rouge)
                if (baseColor.r > 0.5 && baseColor.g < 0.3 && baseColor.b < 0.3) 
                {
                    baseColor *= _SkinColor;
                }
                // Si la couleur du pixel est proche de la couleur des vêtements (vert)
                else if (baseColor.g > 0.5 && baseColor.r < 0.3 && baseColor.b < 0.3)
                {
                    baseColor *= _ClothesColor;
                }
                // Si la couleur du pixel est proche de la couleur des cheveux (bleu)
                else if (baseColor.b > 0.5 && baseColor.r < 0.3 && baseColor.g < 0.3)
                {
                    baseColor *= _HairColor;
                }

                return baseColor;
            }
            ENDCG
        }
    }
}