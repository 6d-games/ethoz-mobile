Shader "AxeyWorks/Low Poly/Batch Vertex Colour Shader" {

	Properties {
		_Shininess ("Shininess", Range(0.03,1)) = 0.078125
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM

		#pragma surface surf BlinnPhong vertex:vert

		half _Shininess;

		struct Input {
			half2 uv_MainTex;
			fixed3 vertColors;
		};

		void vert(inout appdata_full v, out Input o) {
			o.vertColors = v.color.rgb;
			o.uv_MainTex = v.texcoord;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = IN.vertColors.rgb;
			o.Specular = 1;
			o.Gloss = _Shininess;
		}

		ENDCG
	}
	FallBack "Specular"
}
