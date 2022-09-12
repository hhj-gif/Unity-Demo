using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	private float increasingTime;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private float increaseTime;
	private int targetScore;
	private int currentScore;
	private void Awake()
	{
		Initialized();
	}

	private void OnEnable()
	{
		PlayerEventHandler.ScoreChange += AddScore;
		if (LevelManager.Instance != null)
		{
			LevelManager.Instance.BeginLoadingLevel += Initialized;
		}
	}
	private void OnDisable()
	{
		if (LevelManager.Instance != null)
		{
			LevelManager.Instance.BeginLoadingLevel -= Initialized;
		}
	}
	private void Initialized()
	{
		targetScore = 0;
		currentScore = 0;
		increasingTime = 0;
		scoreText.text = currentScore.ToString();
	}

	public void AddScore(int score)
	{
		currentScore = (int)(Mathf.Lerp(currentScore, targetScore, increasingTime / increaseTime));
		targetScore = score;
		increasingTime = 0;
	}

	private void Update()
	{
		if (increasingTime < increaseTime)
		{
			increasingTime += Time.deltaTime;
			scoreText.text = ((int)(Mathf.Lerp(currentScore, targetScore, increasingTime/increaseTime))).ToString();
		}
		else
		{
			currentScore = targetScore;
		}
		//if (Input.GetKeyDown(KeyCode.P))
		//{
		//	AddScore(10000);
		//}
	}
}
