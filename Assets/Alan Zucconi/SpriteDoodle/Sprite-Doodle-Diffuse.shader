// Sprite Doodle Shader Effect
// https://www.alanzucconi.com/?p=9256
Shader "Alan Zucconi/Sprites/Diffuse (Doodle)"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		[Space]

		_NoiseSnap  ("Noise Snap", Range(0.001,1)) = 1
		_NoiseScale ("Noise Scale", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
        #pragma multi_compile _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #include "UnitySprites.cginc"

		#include "random3.cginc"
		float _NoiseSnap;
		float _NoiseScale;

        struct Input
        {
            float2 uv_MainTex;
            fixed4 color;
        };


		inline float snap (float x, float snap)
		{
			return snap * round(x / snap);
		}

        void vert (inout appdata_full v, out Input o)
        {
            v.vertex = UnityFlipSprite(v.vertex, _Flip);

			// ---
			// Snaps time
			float time = snap(_Time.y, _NoiseSnap);
			// Calculates noise displacement
			float2 noise = random3(v.vertex.xyz + float3(time, 0.0, 0.0) ).xy * _NoiseScale;
			// Displaces the vertex
			v.vertex.xy += noise;
			// ---

            #if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = v.color * _Color * _RendererColor;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = SampleSpriteTexture (IN.uv_MainTex) * IN.color;
            o.Albedo = c.rgb * c.a;
            o.Alpha = c.a;
        }
        ENDCG
    }

Fallback "Transparent/VertexLit"
}
