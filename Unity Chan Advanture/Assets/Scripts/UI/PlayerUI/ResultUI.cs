using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
	[SerializeField] GameObject successTitle;
	[SerializeField] GameObject failureTitle;
	[SerializeField] StarBar starBar;
	[SerializeField] NextLevelButton levelButton;
	private int state = 0;
	private int starNumber = 0;
	private void Awake()
	{
		GameEventHandler.GameSuccess += PlayerEventHandler_GameSuccess;
		GameEventHandler.GameFailure += PlayerEventHandler_GameFailure;
		Initialized();
	}
	private void OnEnable()
	{
		if(LevelManager.Instance!=null)
			LevelManager.Instance.BeginLoadingLevel += Initialized;
	}

	private void OnDisable()
	{
		if (LevelManager.Instance != null)
			LevelManager.Instance.BeginLoadingLevel -= Initialized;
	}

	private void Initialized()
	{
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		GameEventHandler.GameSuccess -= PlayerEventHandler_GameSuccess;
		GameEventHandler.GameFailure -= PlayerEventHandler_GameFailure;
	}
	private void PlayerEventHandler_GameFailure(ResultEventArge resultEventArge)
	{
		state = -1;
		starNumber = resultEventArge.starNumber;
		gameObject.SetActive(true);
		levelButton.gameObject.SetActive(false);
	}
	private void PlayerEventHandler_GameSuccess(ResultEventArge resultEventArge)
	{
		state = 1;
		starNumber = resultEventArge.starNumber;
		gameObject.SetActive(true);
		levelButton.gameObject.SetActive(true);
	}

	public void ShowResult()
	{
		if (state == 1)
		{
			successTitle.SetActive(true);
		}
		else
		{
			failureTitle.SetActive(true);
		}
		starBar.gameObject.SetActive(true);
		starBar.SetStarNumber(starNumber);
	}
}
