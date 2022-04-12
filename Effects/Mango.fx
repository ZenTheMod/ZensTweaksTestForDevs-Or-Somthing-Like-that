sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 mainImage(float2 coords : TEXCOORD0) : Color0
{
	float4 color = tex2D(uImage0, coords);
	float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
	color.rgb *= frameY;
	return color * float4(coords.x, frameY, 0.2, 6);
	/*float4 color = tex2D(uImage0, coords);
	float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
	color.rgb *= frameY;
	return color * float4(coords.x, coords.y % (uSourceRect.y / uSourceRect.w), 0.2, 6);*/
}

technique Technique1
{
	pass Light
	{
		PixelShader = compile ps_2_0 mainImage();
	}
}