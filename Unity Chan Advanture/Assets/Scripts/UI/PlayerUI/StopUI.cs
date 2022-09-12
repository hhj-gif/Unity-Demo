using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopUI : MonoBehaviour
{
	private void OnEnable()
	{
		GameEventHandler.CallGameStop();
	}

	private void OnDisable()
	{
		if (gameObject.activeSelf == false)
		{
			GameEventHandler.CallGameContinue();
		}
	}
}
