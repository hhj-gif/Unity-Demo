using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class EasyButton : Button
{
	TextMeshProUGUI[] textMeshProUGUIs;
	AudioSource audioSource;
	protected override  void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
		textMeshProUGUIs = GetComponentsInChildren<TextMeshProUGUI>();
	}
	public override void OnPointerDown(PointerEventData eventData)
	{
		audioSource.Play();
		textMeshProUGUIs[1].gameObject.SetActive(false);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		textMeshProUGUIs[1].gameObject.SetActive(true);
	}

}
