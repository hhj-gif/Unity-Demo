using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBloodBar : MonoBehaviour
{
	public MonsterBloodUI bloodUIPrefab;
	private Dictionary<Monster,MonsterBloodUI> MonsterBloodBarDict;

	private void Awake()
	{
		MonsterBloodBarDict = new Dictionary<Monster, MonsterBloodUI>();
	}
	private void OnDisable()
	{
		MonsterEventHandler.CreateMonster -= CreateMonsterBloodUI;
		MonsterEventHandler.DeadMonster -= DeadMonster;
		if (LevelManager.Instance != null)
			LevelManager.Instance.BeginLoadingLevel -= ClearBloodUI;
	}

	private void OnEnable()
	{
		MonsterEventHandler.CreateMonster += CreateMonsterBloodUI;
		MonsterEventHandler.DeadMonster += DeadMonster;
		if(LevelManager.Instance!=null)
			LevelManager.Instance.BeginLoadingLevel += ClearBloodUI;
	}

	private void ClearBloodUI()
	{
		foreach (var item in MonsterBloodBarDict)
		{
			DeadMonster(item.Key);
		}
		MonsterBloodBarDict.Clear();
	}

	private void CreateMonsterBloodUI(Monster monster)
	{
		MonsterBloodUI bloodUI = GameObject.Instantiate(bloodUIPrefab, transform);
		bloodUI.SetMonster(monster);
		MonsterBloodBarDict.Add(monster, bloodUI);
	}

	private void DeadMonster(Monster monster)
	{
		MonsterBloodUI bloodUI;
		MonsterBloodBarDict.TryGetValue(monster, out bloodUI);
		if (bloodUI != null)
		{
			Destroy(bloodUI.gameObject);
		}
	}
}
