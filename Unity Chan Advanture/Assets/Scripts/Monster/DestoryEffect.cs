using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour
{
	public bool isLoop;
	public float liveTime = -1;

	private void Start()
	{
		if (!isLoop && liveTime > 0)
		{
			StartCoroutine(DestoryDelay(liveTime));
		}
	}
	IEnumerator DestoryDelay(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
