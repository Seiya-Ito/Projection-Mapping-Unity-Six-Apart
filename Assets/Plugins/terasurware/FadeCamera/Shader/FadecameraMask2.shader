/*
 The MIT License (MIT)

Copyright (c) 2013 yamamura tatsuhiko

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
Shader "Fadecamera/mask2" {
        Properties {
                _MainTex ("Base", 2D) = "black" {}
                _MaskTex ("Mask", 2D) = "white" {}
                _Cutoff ("Alpha cutoff", Range(-1,1)) = 0.0
        }
        SubShader {
                Pass {
                        Blend SrcAlpha OneMinusSrcAlpha
                        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

sampler2D _MainTex;
sampler2D _MaskTex;
float _Cutoff;

struct v2f {
    float4 pos : SV_POSITION;
    float2 uv1 : TEXCOORD0;
    float2 uv2 : TEXCOORD1;
};

float4 _MainTex_ST;
float4 _MaskTex_ST;

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
    o.uv1 = TRANSFORM_TEX (v.texcoord, _MainTex);
    o.uv2 = TRANSFORM_TEX (v.texcoord, _MaskTex);
    return o;
}

half4 frag (v2f i) : COLOR
{
        half4 base = tex2D (_MainTex, i.uv1);
        
        half4 mask = tex2D (_MaskTex, i.uv2);
        
        mask.r = mask.r - (_Cutoff);
        mask.b = mask.b - (_Cutoff);
        mask.g = mask.g - (_Cutoff);

       base.w = mask.x * mask.x * mask.x;
    return base;
}
                        ENDCG
                }
        }
        FallBack Off
}

