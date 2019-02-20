Shader "Custom/NewSurfaceShader" {
	Properties 
	{
		_Color("base",Color) = (1,1,1,1)
		_Diffuse("diffuse",Color) = (1,0,0,1)
		_Ambient("ambient",Color) = (1,1,1,1)
		_Specular("specular",Color) = (1,1,1,1)
		_Shininess("shininess",range(0,4))  = 1
	}
	SubShader 
	{
		pass
		{
		Color[_Color]
		material
		{
			Diffuse[_Diffuse]
			Ambient[_Ambient]
			Specular[_Specular]
			Shininess[_Shininess]
		}
			lighting on
			separatespecular on
		}
	}
	FallBack "Diffuse"
}
