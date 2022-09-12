using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	//分数计算系统，连续击破能够多倍得分
	[SerializeField] private int comboDuration;

	private float comboTime;
	private int comboNumber;
	private int score;

	private void Awake()
	{
		score = 0;
		comboTime = 0;
	}

	private void OnEnable()
	{
		MonsterEventHandler.DeadMonster += KillMonster;
	}
	private void OnDisable()
	{
		MonsterEventHandler.DeadMonster -= KillMonster;
	}

	private void Update()
	{
		if (comboTime > 0)
		{
			comboTime -= Time.deltaTime;
		}
		else
		{
			comboNumber = 0;
		}
	}

	private void KillMonster(Monster monster)
	{
		comboTime = comboDuration;
		comboNumber++;
		int score = monster.GetScore()*comboNumber;
		this.score += score;
		PlayerEventHandler.CallScoreChange(this.score);
		PlayerEventHandler.CallComboChanged(comboDuration);
	}
}
