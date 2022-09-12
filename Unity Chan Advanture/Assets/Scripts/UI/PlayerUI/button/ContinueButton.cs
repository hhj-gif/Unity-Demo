using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
	public GameObject topUI;
	public void Continue()
	{
		topUI.SetActive(false);
	}
}
