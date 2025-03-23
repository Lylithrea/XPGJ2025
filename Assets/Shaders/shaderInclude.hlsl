#ifndef SHADERINCLUDE_INCLUDED
#define SHADERINCLUDE_INCLUDED


//#include <HLSLSupport.cginc>
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

// sampler2D _MainTex;
//float2 _MainTex_TexelSize;

struct region
{
    float3 mean;
    float variance;
};

region calcRegion(int2 lower, int2 upper, int samples, float2 uv, float2 _MainTex_TexelSize, UnityTexture2D _MainTex)
{
    region r;
    float3 sum = 0.0;
    float3 squareSum = 0.0;

    for (int x = lower.x; x <= upper.x; ++x)
    {
        for (int y = lower.y; y <= upper.y; ++y)
        {
            const float2 offset = float2(_MainTex_TexelSize.x * x, _MainTex_TexelSize.y * y);
            const float3 tex = tex2D(_MainTex, uv + offset).rgb;
            sum += tex;
            squareSum += tex * tex;
        }
    }

    r.mean = sum / samples;
    const float3 variance = abs((squareSum / samples) - (r.mean * r.mean));
    r.variance = length(variance);

    return r;
}

void Kuwahara_float(UnityTexture2D blitTexture, float2 texelSize, float2 uv, uint kernelSize, out float3 _out)
{
    int upper = (kernelSize - 1) / 2;
    int lower = -upper;

    int samples = (upper + 1) * (upper + 1);
            	
    const region regionA = calcRegion(int2(lower, lower), int2(0, 0), samples, uv, texelSize, blitTexture);
    const region regionB = calcRegion(int2(0, lower), int2(upper, 0), samples, uv, texelSize, blitTexture);
    const region regionC = calcRegion(int2(lower, 0), int2(0, upper), samples, uv, texelSize, blitTexture);
    const region regionD = calcRegion(int2(0, 0), int2(upper, upper), samples, uv, texelSize, blitTexture);

    float3 col = regionA.mean;
    float minVar = regionA.variance;
    
    float testVal = step(regionB.variance, minVar);
    col = lerp(col, regionB.mean, testVal);
    minVar = lerp(minVar, regionB.variance, testVal);
    
    testVal = step(regionC.variance, minVar);
    col = lerp(col, regionC.mean, testVal);
    minVar = lerp(minVar, regionC.variance, testVal);
    
    testVal = step(regionD.variance, minVar);
    col = lerp(col, regionD.mean, testVal);

    _out = col;
}

#endif //SHADERINCLUDE_INCLUDED