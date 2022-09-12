using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
	public LifeContain lifeContainPrefab;
	private List<LifeContain> lifeContains;

	private void Awake()
	{
		lifeContains = new List<LifeContain>();
	}
	private void OnEnable()
	{
		PlayerEventHandler.PlayerLifeChanged += UpdateLifeBar;
		PlayerEventHandler.PlayerCreate += Initialized;
	}

	private void OnDisable()
	{
		PlayerEventHandler.PlayerLifeChanged -= UpdateLifeBar;
		PlayerEventHandler.PlayerCreate -= Initialized;
	}

	private void Initialized(Player player)
	{
		Clear();
		int heartCount = player.maxHeart;
		for (int i = 0; i < heartCount; i++)
		{
			LifeContain lifeContain = GameObject.Instantiate<LifeContain>(lifeContainPrefab, transform);
			lifeContain.SetLife(1);
			lifeContains.Add(lifeContain);
		}
	}

	private void Clear()
	{
		foreach (var item in lifeContains)
		{
			Destroy(item.gameObject);
		}
		lifeContains.Clear();
	}

	private void UpdateLifeBar(int life)
	{
		Debug.Log(life);
		int index = life / PlayerSetting.HeartLife;
		int margin = life % PlayerSetting.HeartLife;
		for(int i = 0; i < lifeContains.Count; i++)
		{
			if (i < index)
			{
				lifeContains[i].SetLife(1);
			}
			else if(i>index) 
			{
				lifeContains[i].SetLife(0);
			}
			else
			{
				lifeContains[i].SetLife((margin/(float)PlayerSetting.HeartLife));
			}
		}
	}
}
