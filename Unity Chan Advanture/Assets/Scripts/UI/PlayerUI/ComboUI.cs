using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ComboUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI comboNumberText;
	[SerializeField] private Image timeImage;
	private float continueTime;
	private float time;
	private bool isStop;
	private int comboNumber;

	private void Awake()
	{
		PlayerEventHandler.ComboChange += StartTime;
		Initialized();
	}
	private void Initialized()
	{
		timeImage.fillAmount = 0;
		time = 0;
		continueTime = 0;
		comboNumber = 0;
		comboNumberText.text = comboNumber.ToString();
		isStop = false;
		gameObject.SetActive(false);
	}
	private void OnEnable()
	{
		GameEventHandler.GameStop += Stop;
		GameEventHandler.GameContinue += Continue;
		if (LevelManager.Instance!=null)
		{
			LevelManager.Instance.BeginLoadingLevel += Initialized;
		}
	}
	private void OnDisable()
	{
		GameEventHandler.GameStop -= Stop;
		GameEventHandler.GameContinue -= Continue;
		if (LevelManager.Instance != null)
		{
			LevelManager.Instance.BeginLoadingLevel -= Initialized;
		}
	}

	private void OnDestroy()
	{
		PlayerEventHandler.ComboChange -= StartTime;
	}
	private void Stop()
	{
		isStop = false;
	}
	private void Continue()
	{
		isStop = true;
	}

	private void StartTime(float continueTime)
	{
		gameObject.SetActive(true);
		comboNumber++;
		timeImage.fillAmount = 0;
		this.time = 0;
		this.continueTime = continueTime;
		comboNumberText.text = comboNumber.ToString();
	}

	private void Update()
	{
		if (time < continueTime)
		{
			timeImage.fillAmount = time/continueTime;
			time += Time.deltaTime;
		}
		else
		{
			comboNumber = 0;
			timeImage.fillAmount = 1;
			gameObject.SetActive(false);
		}
	}
}
