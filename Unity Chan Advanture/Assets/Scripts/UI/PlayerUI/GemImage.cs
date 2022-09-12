using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GemImage : MonoBehaviour
{
    [SerializeField] private Image coolingImage;
	[SerializeField] private TextMeshProUGUI countText;

	public void SetCount(int count)
	{
		countText.text = count.ToString();
	}
    public void StartCoolingTime(float time)
	{
		coolingImage.fillAmount = 1;
		coolingImage.DOFillAmount(0, time);
	}
}
