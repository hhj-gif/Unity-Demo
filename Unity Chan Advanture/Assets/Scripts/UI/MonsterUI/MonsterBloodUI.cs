using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterBloodUI : MonoBehaviour
{
	private Image currentBloodBar;
	private Image boundUI;
	private bool isSet;
	private int maxBlood;
	private Monster monster;
	private RectTransform rectTransform;
	private void Awake()
	{
		boundUI = GetComponentsInChildren<Image>()[0];
		currentBloodBar = GetComponentsInChildren<Image>()[1];
		rectTransform = GetComponent<RectTransform>();
		isSet = false;
	}

	private void Update()
	{
		if (isSet)
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(monster.transform.position);
			//Debug.Log(screenPoint.z);
			if (screenPoint.z > 15|| screenPoint.z<5)
			{
				boundUI.gameObject.SetActive(false);
				currentBloodBar.gameObject.SetActive(false);
			}
			else
			{
				boundUI.gameObject.SetActive(true);
				currentBloodBar.gameObject.SetActive(true);
				rectTransform.position = screenPoint;
			}
			//Debug.Log(screenPoint);
		}
	}
	public void SetMonster(Monster monster)
	{
		isSet = true;
		maxBlood = monster.maxHP;
		this.monster = monster;
		this.monster.SetBloodUI(this);
	}

	public void UpdateBlood(float blood)
	{
		currentBloodBar.fillAmount = blood / maxBlood;
	}
}
