using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBar : MonoBehaviour
{
	[SerializeField] private GameObject []starUI;
	
	public void SetStarNumber(int number)
	{
		if (number > starUI.Length)
		{
			number = starUI.Length;
		}
		for (int i = 0; i < starUI.Length; i++)
		{
			if (i < number)
			{
				starUI[i].SetActive(true);
			}
			else
			{
				starUI[i].SetActive(false);
			}
		}
	}
}
