Shader "Toon/Lit Tri Planar Normal" {
	Properties{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex("Top Texture", 2D) = "white" {}
		_NormalT("Top Normal", 2D) = "bump" {}
		_MainTexSide("Side/Bottom Texture", 2D) = "white" {}
		_Normal("Side/Bottom Normal", 2D) = "bump" {}
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_Noise("Noise", 2D) = "white" {}
		_Scale("Top Scale", Range(-2,2)) = 1
		_SideScale("Side Scale", Range(-2,2)) = 1
		_NoiseScale("Noise Scale", Range(-2,2)) = 1
		_TopSpread("TopSpread", Range(-2,2)) = 1
		_EdgeWidth("EdgeWidth", Range(0,0.5)) = 1
		_EdgeColor("Edge Color", Color) = (0.5,0.5,0.5,1)
		_RimPower("Rim Power", Range(-2,20)) = 1
		_RimColor("Rim Color Top", Color) = (0.5,0.5,0.5,1)
		_RimColor2("Rim Color Side/Bottom", Color) = (0.5,0.5,0.5,1)
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf ToonRamp

		sampler2D _Ramp;

	// custom lighting function that uses a texture ramp based
	// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
	inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
	{
#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = normalize(lightDir);
#endif

		half d = dot(s.Normal, lightDir)*0.5 + 0.5;
		half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;

		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
		c.a = 0;
		return c;
	}


	sampler2D _MainTex, _MainTexSide, _Normal, _Noise, _NormalT;
	float4 _Color, _RimColor, _RimColor2, _EdgeColor;
	float _RimPower;
	float  _TopSpread, _EdgeWidth;
	float _Scale, _SideScale, _NoiseScale;

	struct Input {
		float2 uv_MainTex : TEXCOORD0;
		float3 worldPos; // world position built-in value
		float3 worldNormal; INTERNAL_DATA // world normal built-in value
			float3 viewDir;// view direction built-in value we're using for rimlight
	};

	void surf(Input IN, inout SurfaceOutput o) {

		// clamp (saturate) and increase(pow) the worldnormal value to use as a blend between the projected textures
		float3 worldNormalE = WorldNormalVector(IN, o.Normal);
		float3 blendNormal = saturate(pow(worldNormalE * 1.4,4));


		// normal noise triplanar for x, y, z sides
		float3 xn = tex2D(_Noise, IN.worldPos.zy * _NoiseScale);
		float3 yn = tex2D(_Noise, IN.worldPos.zx * _NoiseScale);
		float3 zn = tex2D(_Noise, IN.worldPos.xy * _NoiseScale);

		// lerped together all sides for noise texture
		float3 noisetexture = zn;
		noisetexture = lerp(noisetexture, xn, blendNormal.x);
		noisetexture = lerp(noisetexture, yn, blendNormal.y);

		// triplanar for top texture for x, y, z sides
		float3 xm = tex2D(_MainTex, IN.worldPos.zy * _Scale);
		float3 zm = tex2D(_MainTex, IN.worldPos.xy * _Scale);
		float3 ym = tex2D(_MainTex, IN.worldPos.zx * _Scale);

		// lerped together all sides for top texture
		float3 toptexture = zm;
		toptexture = lerp(toptexture, xm, blendNormal.x);
		toptexture = lerp(toptexture, ym, blendNormal.y);

		// triplanar for top normal for x, y, z sides

		float3 xnnt = UnpackNormal(tex2D(_NormalT, IN.worldPos.zy * _Scale));
		float3 znnt = UnpackNormal(tex2D(_NormalT, IN.worldPos.xy * _Scale));
		float3 ynnt = UnpackNormal(tex2D(_NormalT, IN.worldPos.zx * _Scale));

		// lerped together all sides for top normal
		float3 toptextureNormal = znnt;
		toptextureNormal = lerp(toptextureNormal, xnnt, blendNormal.x);
		toptextureNormal = lerp(toptextureNormal, ynnt, blendNormal.y);

		// triplanar for side normal for x, y, z sides
		float3 xnn = UnpackNormal(tex2D(_Normal, IN.worldPos.zy * _SideScale));
		float3 znn = UnpackNormal(tex2D(_Normal, IN.worldPos.xy * _SideScale));
		float3 ynn = UnpackNormal(tex2D(_Normal, IN.worldPos.zx * _SideScale));

		// lerped together all sides for side normal
		float3 sidetextureNormal = znn;
		sidetextureNormal = lerp(sidetextureNormal, xnn, blendNormal.x);
		sidetextureNormal = lerp(sidetextureNormal, ynn, blendNormal.y);


		// triplanar for side and bottom texture, x,y,z sides
		float3 x = tex2D(_MainTexSide, IN.worldPos.zy * _SideScale);
		float3 y = tex2D(_MainTexSide, IN.worldPos.zx * _SideScale);
		float3 z = tex2D(_MainTexSide, IN.worldPos.xy * _SideScale);

		// lerped together all sides for side bottom texture
		float3 sidetexture = z;
		sidetexture = lerp(sidetexture, x, blendNormal.x);
		sidetexture = lerp(sidetexture, y, blendNormal.y);



		// dot product of world normal and surface normal + noise
		float worldNormalDotNoise = dot(o.Normal + (noisetexture.y + (noisetexture * 0.5)), worldNormalE.y);

		// if dot product is higher than the top spread slider, multiplied by triplanar mapped top texture
		// step is replacing an if statement to avoid branching :
		// if (worldNormalDotNoise > _TopSpread{ o.Albedo = toptexture}
		float3 topTextureResult = step(_TopSpread + _EdgeWidth, worldNormalDotNoise) * toptexture;
		float3 topNormalResult = step(_TopSpread, worldNormalDotNoise) * toptextureNormal;

		// if dot product is lower than the top spread slider, multiplied by triplanar mapped side/bottom texture
		float3 sideTextureResult = step(worldNormalDotNoise, _TopSpread) * sidetexture;
		float3 sideNormalResult = step(worldNormalDotNoise, _TopSpread) * sidetextureNormal;

		// if dot product is in between the two, make the texture darker
		float3 topTextureEdgeResult = (step(_TopSpread , worldNormalDotNoise) * step(worldNormalDotNoise, _TopSpread + _EdgeWidth)) * _EdgeColor;

		// final normal
		o.Normal = topNormalResult + sideNormalResult;
		// final albedo color 
		o.Albedo = topTextureResult + sideTextureResult + topTextureEdgeResult;
		o.Albedo *= _Color;

		// adding the fuzzy rimlight(rim) on the top texture, and the harder rimlight (rim2) on the side/bottom texture
		// rim light for fuzzy top texture
		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));

		// rim light for side/bottom texture
		half rim2 = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = step(_TopSpread + _EdgeWidth, worldNormalDotNoise) * _RimColor.rgb * pow(rim, _RimPower) + step(worldNormalDotNoise, _TopSpread) * _RimColor2.rgb * pow(rim2, _RimPower);


	}
	ENDCG

	}

		Fallback "Diffuse"
}