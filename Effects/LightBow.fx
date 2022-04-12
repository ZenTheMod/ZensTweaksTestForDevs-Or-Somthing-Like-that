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
    // Time varying pixel color
	float3 col = float3(0.78, 0.78, 0.78) + float3(0.3, 0.3, 0.3) * cos(float3(uTime, uTime, uTime) + coords.xyx + float3(3, 29999, 4));
    
	return tex2D(uImage0, coords) * // sample from the texture
    float4(col, 1.110); // 'combine' the color with multiplication
}

technique Technique1
{
	pass Light
	{
		PixelShader = compile ps_2_0 mainImage();
	}
}