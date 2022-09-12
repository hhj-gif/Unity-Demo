using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	[SerializeField] GameObject []ui;
	[SerializeField] KeyCode[] uiKey;
	private bool[] uiActivate;
	protected override void Awake()
	{
		base.Awake();
		uiActivate = new bool[ui.Length];
	}
	private void Update()
	{
		for (int i = 0; i < ui.Length; i++)
		{
			if (Input.GetKeyDown(uiKey[i]))
			{
				uiActivate[i] = !ui[i].activeSelf;
				ui[i].SetActive(uiActivate[i]);
			}
		}
	}
}
