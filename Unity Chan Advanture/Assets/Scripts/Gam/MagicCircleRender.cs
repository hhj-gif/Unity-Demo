using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleRender : MeshRenderer
{
	public void LoadMaterial(string path)
	{
		this.material = Resources.Load<Material>(path);
	}

	public void ChangeColor(Color color)
	{
		//this.material.SetColor();
	}
}
