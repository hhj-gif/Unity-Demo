using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeContain : MonoBehaviour
{
	private Image lifeImage;

	public void Awake()
	{
		lifeImage = GetComponentsInChildren<Image>()[1];
	}
	public void SetLife(float life)
	{
		lifeImage.fillAmount = life;
	}
}
