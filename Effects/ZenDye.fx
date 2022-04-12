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

float4 ArmorMyShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	return color * sampleColor * float4(0.815, 0.329, 0.360, 1);
}
float4 ArmorMyShader2(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	return color * sampleColor * float4(0.258, 0.192, 0.286, 1);
}
float4 MotionMango(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{ 
	// Time varying pixel color
	float3 col = float3(0.5, 0.5, 0.5) + float3(0.5, 0.5, 0.5) * cos(float3(uTime, uTime, uTime) + coords.xyx + float3(0, 2, 4));
	float4 color = tex2D(uImage0, coords);
	float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
	color.rgb *= frameY;
	return color * sampleColor * float4(coords.x, frameY, col, 1); // 'combine' the color with multiplication
}

technique Technique1
{
	pass Zen
	{
		PixelShader = compile ps_2_0 ArmorMyShader();
	}
	pass Zen2
	{
		PixelShader = compile ps_2_0 ArmorMyShader2();
	}
}