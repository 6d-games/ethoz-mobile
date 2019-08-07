Shader "AxeyWorks/Low Poly/Water v2 (Transparent)"
{
    Properties
    {
        _AlbedoTex("Albedo", 2D) = "white" {}
        _AlbedoColor("Albedo Color", Color) = (1, 1, 1, 1)
        _SpecularColor("Specular Color", Color) = (0, 0, 0, 0)
       	_Shininess ("Shininess", Float) = 10
       	_FresnelPower ("Fresnel Power", Float) = 5
       	_Reflectivity ("Reflectivity", Range(0.0, 1.0)) = 0.15
       	_Disturbance ("Disturbance", Float) = 10
		[HideInInspector] _ReflectionTex ("", 2D) = "white" {}

		_FoamColor("Foam Alpha", Color) = (1, 1, 1, 1)
		_ShoreTex("Blend Foam Texture", 2D) = "black" {}
		_InvFadeParemeter("Auto Blend (Edge, Shore, Distance Scale)", Vector) = (0.2 ,0.39, 0.5, 1.0)
		_BumpTiling("Blend Foam Tiling", Vector) = (1.0 ,1.0, -2.0, 3.0)
		_BumpDirection("Foam Movement", Vector) = (1.0 ,1.0, -1.0, 1.0)
		_Foam("Foam Intensity & Cut-off)", Vector) = (0.1, 0.375, 0.0, 0.0)
		[MaterialToggle] _isInnerAlphaBlendOrColor("Disable texture foam and use alpha?", Float) = 0
    }
	CGINCLUDE

		#include "AutoLight.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "UnityLightingCommon.cginc"

        struct VS_Input
        {
            half4 vertex : POSITION;
            half3 normal : NORMAL;
            half2 uv : TEXCOORD0;
        };

        struct GS_Output
        {
            half4 pos : SV_POSITION;
            half3 worldPos : WORLDPOSITION;
			float4 normalInterpolator : TEXCOORD9;
			float4 viewInterpolator : TEXCOORD1;
            half2 uv : TEXCOORD0;
            half3 ambient : AMBIENT;
            half3 diffuse : DIFFUSE;
            half3 specular : SPECULAR;
			float4 bumpCoords : TEXCOORD2;
            half4 screenPos : REFLECTION;
			float4 grabPassPos : TEXCOORD4;
			half3 worldRefl : TEXCOORD6;
			float4 posWorld : TEXCOORD7;
			float3 normalDir : TEXCOORD8;

			UNITY_FOG_COORDS(5)
        };

        struct VS_Output
        {
            half4 pos : SV_POSITION;
            half3 worldPos : WORLDPOSITION;
            half2 uv : TEXCOORD0;
            half4 screenPos : REFLECTION;
            half3 waveNormal : WAVENORMAL;
        };

        static const float PI = float(3.14159);

        sampler2D _AlbedoTex;
        sampler2D _ReflectionTex;
        half4 _AlbedoTex_ST;
        half4 _AlbedoColor;
        half4 _SpecularColor;
        float _Shininess;
        float _FresnelPower;
        float _Reflectivity;
        float _Disturbance;


		sampler2D _ShoreTex;
		sampler2D_float _CameraDepthTexture;
		uniform float4 _InvFadeParemeter;
		uniform float4 _BumpTiling;
		uniform float4 _BumpDirection;
		uniform float4 _Foam;
		half4 _FoamColor;
		float _isInnerAlphaBlendOrColor;
		#define VERTEX_WORLD_NORMAL i.normalInterpolator.xyz 

        uniform half3 _SineWave0[8];
        uniform half3 _SineWave1[8];
        uniform int _Waves;
        uniform float _TimeScale;

		inline half4 Foam(sampler2D shoreTex, half4 coords)
		{
			half4 foam = (tex2D(shoreTex, coords.xy) * tex2D(shoreTex, coords.zw)) - 0.125;
			return foam;
		}

		GS_Output vert(appdata_full v)
		{
			GS_Output o;

			half3 worldSpaceVertex = mul(unity_ObjectToWorld, (v.vertex)).xyz;
			half3 vtxForAni = (worldSpaceVertex).xzz;

			half3	offsets = half3(0, 0, 0);
			half3	nrml = half3(0, 1, 0);

			v.vertex.xyz += offsets;

			half2 tileableUv = mul(unity_ObjectToWorld, (v.vertex)).xz;

			o.bumpCoords.xyzw = (tileableUv.xyxy + _Time.xxxx * _BumpDirection.xyzw) * _BumpTiling.xyzw;

			o.viewInterpolator.xyz = worldSpaceVertex - _WorldSpaceCameraPos;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.screenPos = ComputeScreenPos(o.pos);
			o.normalInterpolator.xyz = nrml;
			o.viewInterpolator.w = saturate(offsets.y);
			o.normalInterpolator.w = 1;

			UNITY_TRANSFER_FOG(o, o.pos);
			half3 worldNormal = UnityObjectToWorldNormal(v.normal);
			float4x4 modelMatrix = unity_ObjectToWorld;
			float4x4 modelMatrixInverse = unity_WorldToObject;
			o.posWorld = mul(modelMatrix, v.vertex);
			o.normalDir = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);

			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
			o.worldRefl = reflect(-worldViewDir, worldNormal);

			return o;
		}

        float Wave(int i, float x, float y)
        {
            float A = _SineWave0[i].x;
			float O = _SineWave0[i].y;
            float P = _SineWave0[i].z;
        	half2 D = _SineWave1[i].xy;
            float sine = sin(dot(D, half2(x, y)) * O + _Time.x * _TimeScale * P);
            return 2.0f * A * pow((sine + 1.0f) / 2.0f, _SineWave1[i].z);
        }
        float dxWave(int i, float x, float y)
        {
            float A = _SineWave0[i].x;  
            float O = _SineWave0[i].y;  
            float P = _SineWave0[i].z;  
            half2 D = _SineWave1[i].xy; 
            float term = dot(D, half2(x, y)) * O + _Time.x * _TimeScale * P;
            float power = max(1.0f, _SineWave1[i].z - 1.0f);
            float sinP = pow((sin(term) + 1.0f) / 2.0f, power);
            return _SineWave1[i].z * D.x * O * A * sinP * cos(term);
        }
        float dzWave(int i, float x, float y)
        {
            float A = _SineWave0[i].x; 
            float O = _SineWave0[i].y; 
            float P = _SineWave0[i].z; 
            half2 D = _SineWave1[i].xy;
            float term = dot(D, half2(x, y)) * O + _Time.x * _TimeScale * P;
            float power = max(1.0f, _SineWave1[i].z - 1.0f);
            float sinP = pow((sin(term) + 1.0f) / 2.0f, power);
            return _SineWave1[i].z * D.y * O * A * sinP * cos(term);
        }
        half3 WaveNormal(float x, float y)
        {
        	float dx = 0.0f;
        	float dz = 0.0f;

        	for(int i = 0; i < _Waves; i++)
        	{
        		dx += dxWave(i, x, y);
        		dz += dzWave(i, x, y);
        	}

        	return normalize(half3(-dx, 1.0f, -dz));
        }

        float WaveHeight(float x, float y)
        {
            float height = 0.0;

            for(int i = 0; i < _Waves; i++)
            {
                height += Wave(i, x, y);
            }

            return height;
        }

	ENDCG
    SubShader
    {
    	Tags{ "Queue" = "Transparent" }
        LOD 200
        Pass
        {
            Tags{ "RenderMode" = "Transparent" "LightMode" = "ForwardBase" }
     		Cull Front
         	ZWrite Off
        	Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
			#pragma multi_compile_fog


            VS_Output vert(VS_Input v)
            {
                VS_Output o = (VS_Output)0;

                v.vertex.xyz += v.normal * WaveHeight(v.vertex.x, v.vertex.z);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _AlbedoTex);

                o.waveNormal = UnityObjectToWorldNormal(half4(WaveNormal(v.vertex.x, v.vertex.z), 0.0f));
                half4 dPos = o.pos;
                dPos.x += _Disturbance * o.waveNormal.x;
                dPos.z += _Disturbance * o.waveNormal.z;
                o.screenPos = ComputeScreenPos(dPos);
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle VS_Output input[3], inout TriangleStream<GS_Output> OutputStream)
            {
                GS_Output test = (GS_Output)0;

                half3 waveFx = (input[0].waveNormal + input[1].waveNormal 
                                + input[2].waveNormal) / 3.0f;

                half3 center = (input[0].worldPos + input[1].worldPos 
                                + input[2].worldPos) / 3.0f;

                half3 normalDirection = normalize(waveFx);
                half3 lightDirection;

                if (0.0 == _WorldSpaceLightPos0.w)
                {
                    lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                }
                else
                {
                    lightDirection = normalize(_WorldSpaceLightPos0.xyz - center);
                }

                float nDotL = dot(normalDirection, lightDirection);
                half3 ambient = ShadeSH9(half4(normalDirection, 1.0f)) * _AlbedoColor.rgb;
                half3 diffuse = _LightColor0.rgb * _AlbedoColor.rgb * max(0.0f, nDotL);
                half3 specular = half3(0.0f, 0.0f, 0.0f);

                if(nDotL > 0.0f)
                {
                    half3 viewDir = normalize(_WorldSpaceCameraPos - center);

                    half3 H = normalize(lightDirection + viewDir);

                    float specIntensity = pow(saturate(dot(normalDirection, H)), _Shininess);

                    float fresnel = pow(1.0f - max(0.0f, dot(viewDir, H)), _FresnelPower);
                    half3 refl2Refr = _SpecularColor.rgb + (1.0f - _SpecularColor.rgb) * fresnel;
                    specular = _LightColor0.rgb * refl2Refr * max(0.0f, specIntensity);  
                }

                for (int i = 0; i < 3; i++)
                {

                    test.pos = input[i].pos;
                    test.worldPos = center;
                    test.uv = input[i].uv;
                    test.ambient = ambient;
                    test.specular = specular;
                    test.diffuse = diffuse;
                    test.screenPos = input[i].screenPos;
                    OutputStream.Append(test);
                }
            }

            half4 frag(GS_Output i) : SV_Target
            {
				half4 edgeBlendFactors = half4(1.0, 0.0, 0.0, 0.0);

				half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
				depth = LinearEyeDepth(depth);
				edgeBlendFactors = saturate(_InvFadeParemeter * (depth - i.screenPos.w));
				edgeBlendFactors.y = 1.0 - edgeBlendFactors.y;

				half4 foam = Foam(_ShoreTex, i.bumpCoords * 2.0);

                half4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.screenPos));
                half4 col = tex2D(_AlbedoTex, i.uv);

				col.rgb += foam.rgb * _Foam.x * (edgeBlendFactors.y + saturate(i.viewInterpolator.w - _Foam.y));

				if (_isInnerAlphaBlendOrColor == 0)
					_FoamColor.rgb += 1.0 - edgeBlendFactors.x;

				if (_isInnerAlphaBlendOrColor == 1.0)
					col.a = edgeBlendFactors.x;

				UNITY_APPLY_FOG(i.fogCoord, col);

                col.rgb *= i.ambient + (i.diffuse + i.specular);
                col.rgb = lerp(col.rgb, refl.rgb, _Reflectivity) * _FoamColor;
                col.a *= _AlbedoColor.a;
                return col;
            }
            ENDCG
        }
		Pass
        {
        	Tags{ "RenderMode" = "Transparent" "LightMode" = "ForwardBase" }
     		Cull Back
         	ZWrite Off
        	Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			#pragma multi_compile_fog


            VS_Output vert(VS_Input v)
            {
                VS_Output o = (VS_Output)0;

                v.vertex.xyz += v.normal * WaveHeight(v.vertex.x, v.vertex.z);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _AlbedoTex);

                TRANSFER_SHADOW(o)

                o.waveNormal = UnityObjectToWorldNormal(half4(WaveNormal(v.vertex.x, v.vertex.z), 0.0f));
                half4 dPos = o.pos;
                dPos.x += _Disturbance * o.waveNormal.x;
                dPos.z += _Disturbance * o.waveNormal.z;
                o.screenPos = ComputeScreenPos(dPos);
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle VS_Output input[3], inout TriangleStream<GS_Output> OutputStream)
            {
                GS_Output test = (GS_Output)0;

                half3 waveFx = (input[0].waveNormal + input[1].waveNormal 
                                + input[2].waveNormal) / 3.0f;

                half3 center = (input[0].worldPos + input[1].worldPos 
                                + input[2].worldPos) / 3.0f;

                half3 normalDirection = normalize(waveFx);
                half3 lightDirection;

                if (0.0 == _WorldSpaceLightPos0.w)
                {
                    lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                }
                else
                {
                    lightDirection = normalize(_WorldSpaceLightPos0.xyz - center);
                }

                float nDotL = dot(normalDirection, lightDirection);
                half3 ambient = ShadeSH9(half4(normalDirection, 1.0f)) * _AlbedoColor.rgb;
                half3 diffuse = _LightColor0.rgb * _AlbedoColor.rgb * max(0.0f, nDotL);
                half3 specular = half3(0.0f, 0.0f, 0.0f);

                if(nDotL > 0.0f)
                {
                    half3 viewDir = normalize(_WorldSpaceCameraPos - center);

                    half3 H = normalize(lightDirection + viewDir);

                    float specIntensity = pow(saturate(dot(normalDirection, H)), _Shininess);

                    float fresnel = pow(1.0f - max(0.0f, dot(viewDir, H)), _FresnelPower);
                    half3 refl2Refr = _SpecularColor.rgb + (1.0f - _SpecularColor.rgb) * fresnel;
                    specular = _LightColor0.rgb * refl2Refr * max(0.0f, specIntensity);  
                }

                for (int i = 0; i < 3; i++)
                {
                    test.pos = input[i].pos;
                    test.worldPos = center;
                    test.uv = input[i].uv;
                    test.ambient = ambient;
                    test.specular = specular;
                    test.diffuse = diffuse;
                    test.screenPos = input[i].screenPos;
                    OutputStream.Append(test);
                }
            }

            half4 frag(GS_Output i) : SV_Target
            {
				half4 edgeBlendFactors = half4(1.0, 0.0, 0.0, 0.0);

				half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
				depth = LinearEyeDepth(depth);
				edgeBlendFactors = saturate(_InvFadeParemeter * (depth - i.screenPos.w));
				edgeBlendFactors.y = 1.0 - edgeBlendFactors.y;

				half4 foam = Foam(_ShoreTex, i.bumpCoords * 2.0);

                half4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.screenPos));
                half4 col = tex2D(_AlbedoTex, i.uv);

				col.rgb += foam.rgb * _Foam.x * (edgeBlendFactors.y + saturate(i.viewInterpolator.w - _Foam.y));

				if (_isInnerAlphaBlendOrColor == 0)
					_FoamColor.rgb += 1.0 - edgeBlendFactors.x;

				if (_isInnerAlphaBlendOrColor == 1.0)
					col.a = edgeBlendFactors.x;

				UNITY_APPLY_FOG(i.fogCoord, col);
                UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);

                col.rgb *= i.ambient + (i.diffuse + i.specular) * attenuation;
                col.rgb = lerp(col.rgb, refl.rgb, _Reflectivity) * _FoamColor;
                col.a *= _AlbedoColor.a;
                return col;
            }
            ENDCG
        }
        Pass
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

			Blend SrcAlpha OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster 
			#pragma multi_compile_fog
            #include "UnityCG.cginc"			

			struct v2f {
				V2F_SHADOW_CASTER;
			};

            v2f vert(appdata_base v)
            {
                v2f o;

                v.vertex.xyz += v.normal * WaveHeight(v.vertex.x, v.vertex.z);
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
	Fallback "Transparent/Diffuse"
}