fixed4 _Color;
fixed4 _ReflectColor;
half _Shininess;
fixed _DrawLimit;
struct Input {
	float2 uv_MainTex;
	float3 worldRefl;
	float3 worldPos;
};
void surf(Input IN, inout SurfaceOutput o) {
	clip(IN.worldPos.z - _DrawLimit);

	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	o.Albedo = c.rgb;
	o.Gloss = tex.a;

	o.Specular = _Shininess;

	fixed4 reflcol = texCUBE(_Cube, IN.worldRefl);
	reflcol *= tex.a;
	o.Emission = reflcol.rgb * _ReflectColor.rgb;
	o.Alpha = reflcol.a * _ReflectColor.a;
}