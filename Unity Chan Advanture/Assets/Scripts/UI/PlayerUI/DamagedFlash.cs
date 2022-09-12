using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamagedFlash : MonoBehaviour
{
	public GameObject radFlash;
	private void Awake()
	{
		radFlash.SetActive(false);
	}
	private void OnEnable()
	{
		PlayerEventHandler.PlayerDamaged += StartFlash;
	}
	private void OnDisable()
	{
		PlayerEventHandler.PlayerDamaged -= StartFlash;
	}

	private void StartFlash()
	{
		StartCoroutine(FlashImage(PlayerSetting.RadFlashTime));
	}

	private IEnumerator FlashImage(int times)
	{
		int i = 0;
		while (true)
		{
			radFlash.SetActive(true);
			yield return new WaitForSeconds(PlayerSetting.RadFlashEachTime);
			radFlash.SetActive(false);
			yield return new WaitForSeconds(PlayerSetting.RadFlashEachTime);
			i++;
			if (i >= times)
			{
				break;
			}
		}
	}
}
