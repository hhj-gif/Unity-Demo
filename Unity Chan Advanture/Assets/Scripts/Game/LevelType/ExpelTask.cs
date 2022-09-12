using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ExpelTask : Task
{
	public int targetNumber;
	private int number;
	private void OnEnable()
	{
		MonsterEventHandler.DeadMonster += MonsterEventHandler_DeadMonster;
	}
	private void OnDisable()
	{
		MonsterEventHandler.DeadMonster -= MonsterEventHandler_DeadMonster;
	}
	private void MonsterEventHandler_DeadMonster(Monster obj)
	{
		AddNumber(1);
	}
	private void AddNumber(int number)
	{
		this.number += number;
		if (this.number >= targetNumber)
		{
			this.number = targetNumber;
			TaskSuccess();
		}
		CallTaskProgressChange(string.Format("{0}/{1}", this.number, targetNumber));
	}
	public override string GetTaskProgress()
	{
		return string.Format("{0}/{1}", number, targetNumber);
	}
}
