﻿Shader "AxeyWorks/Low Poly/Water v2 (No Geo)"
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
    }
	CGINCLUDE
        uniform half3 _SineWave0[8];
        uniform half3 _SineWave1[8];
        uniform int _Waves;
        uniform float _TimeScale;

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
        LOD 200
        Pass
        {
        	Tags{ "RenderMode" = "Opaque" "Queue" = "Geometry" "LightMode" = "ForwardBase" }
        	Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap

			#include "AutoLight.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

            struct VS_Input
            {
                half4 vertex : POSITION;
                half3 normal : NORMAL;
                half2 uv : TEXCOORD0;
            };
            struct VS_Output
            {
                half4 pos : SV_POSITION;
                half3 worldPos : WORLDPOSITION;
                half2 uv : TEXCOORD0;
				SHADOW_COORDS(1)
                half4 screenPos : REFLECTION;
                half3 waveNormal : WAVENORMAL;
				half3 ambient : AMBIENT;
				half3 diffuse : DIFFUSE;
				half3 specular : SPECULAR;
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
				half3 normalDirection = o.waveNormal;
				half3 lightDirection;

				if (0.0 == _WorldSpaceLightPos0.w)
				{
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}
				else
				{
					lightDirection = normalize(_WorldSpaceLightPos0.xyz - o.worldPos);
				}

				float nDotL = dot(normalDirection, lightDirection);
				half3 ambient = ShadeSH9(half4(normalDirection, 1.0f)) * _AlbedoColor.rgb;
				half3 diffuse = _LightColor0.rgb * _AlbedoColor.rgb * max(0.0f, nDotL);
				half3 specular = half3(0.0f, 0.0f, 0.0f);

				if (nDotL > 0.0f)
				{
					half3 viewDir = normalize(_WorldSpaceCameraPos - o.worldPos);
					half3 H = normalize(lightDirection + viewDir);
					float specIntensity = pow(saturate(dot(normalDirection, H)), _Shininess);
					float fresnel = pow(1.0f - max(0.0f, dot(viewDir, H)), _FresnelPower);
					half3 refl2Refr = _SpecularColor.rgb + (1.0f - _SpecularColor.rgb) * fresnel;
					specular = _LightColor0.rgb * refl2Refr * max(0.0f, specIntensity);
				}

				o.ambient = ambient;
				o.specular = specular;
				o.diffuse = diffuse;
				TRANSFER_SHADOW(o);

                return o;
            }

            half4 frag(VS_Output i) : SV_Target
            {
            	half4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.screenPos));
                half4 col = tex2D(_AlbedoTex, i.uv);
                UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);
                col.rgb *= i.ambient + (i.diffuse + i.specular) * attenuation;
                col.rgb = lerp(col.rgb, refl.rgb, _Reflectivity);
                return col;
            }
            ENDCG
        }
        Pass
        {
            Tags { "LightMode"="ShadowCaster" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
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
}