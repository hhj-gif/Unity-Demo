using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventHandler
{
	public static event Action GameStop;
	public static void CallGameStop()
	{
		GameStop?.Invoke();
	}

	public static event Action GameContinue;
	public static void CallGameContinue()
	{
		GameContinue?.Invoke();
	}

	public static event Action<ResultEventArge> GameSuccess;

	public static void CallGameSuccess(ResultEventArge resultEventArge)
	{
		GameEventHandler.CallGameStop();
		GameSuccess?.Invoke(resultEventArge);
	}

	public static event Action<ResultEventArge> GameFailure;
	public static void CallGameFailure(ResultEventArge resultEventArge)
	{
		GameEventHandler.CallGameStop();
		GameFailure?.Invoke(resultEventArge);
	}
}

public static class PlayerEventHandler
{
	public static event Action PlayerDamaged;
	public static void CallPlayerDamaged()
	{
		PlayerDamaged?.Invoke();
	}

	public static event Action<int> PlayerLifeChanged;
	public static void CallPlayerLifeChanged(int life)
	{
		PlayerLifeChanged?.Invoke(life);
	}

	public static event Action<Player> PlayerCreate;
	public static void CallPlayerCreate(Player player)
	{
		PlayerCreate?.Invoke(player);
	}

	public static event Action<int> ChangedGamIndex;
	//ÇÐ»»Ñ¡ÔñµÄ±¦Ê¯
	public static void CallChangedGamIndex(int index)
	{
		ChangedGamIndex?.Invoke(index);
	}

	public static event Action<Task> LevelTaskStart;
	public static void CallLevelTaskStart(Task task)
	{
		LevelTaskStart?.Invoke(task);
	}

	public static event Action PlayerDeath;
	public static void CallPlayerDeath()
	{
		PlayerDeath?.Invoke();
	}

	public static event Action<int> ScoreChange;

	public static void CallScoreChange(int score)
	{
		ScoreChange?.Invoke(score);
	}

	public static event Action<float> ComboChange;

	public static void CallComboChanged(float continueTime)
	{
		ComboChange?.Invoke(continueTime);
	}
}

public static class MonsterEventHandler
{
	public static event Action<Monster> CreateMonster;
	public static void CallCreateMonster(Monster monster)
	{
		CreateMonster?.Invoke(monster);
	}

	public static event Action<Monster> DeadMonster;

	public static void CallDeadMonster(Monster monster)
	{
		DeadMonster?.Invoke(monster);
	}
}
