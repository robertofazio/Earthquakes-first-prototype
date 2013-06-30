Shader "CookbookShaders/BasicDiffuse" 
{
	//We define Properties in the properties block
	Properties 
	{
		_EmissiveColor ("Emissive Color", Color) = (1,1,1,1)
		_AmbientColor  ("Ambient Color", Color) = (1,1,1,1)
		_MySliderValue ("This is a Slider", Range(0,10)) = 2.5
		_Fresnel ("Fresnel (A) ", 2D) = "" {}
	}
	
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert


//We need to declare the properties variable type inside of the CGPROGRAM so we can access its value from the properties block.
		float4 _EmissiveColor;
		float4 _AmbientColor;
		float _MySliderValue;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
//We can then use the properties values in our shader 
			float4 c;
c =  pow((_EmissiveColor + _AmbientColor), _MySliderValue);
			
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		
		ENDCG
	} 
	
	FallBack "Diffuse"
}