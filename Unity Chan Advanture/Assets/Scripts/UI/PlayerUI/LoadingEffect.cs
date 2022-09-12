using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class LoadingEffect : MonoBehaviour
{
	[SerializeField]private Image loadingImage;
	[SerializeField]private CanvasGroup fadeCanvasGroup;

	private void Awake()
	{
		if (LevelManager.Instance != null)
		{
			LevelManager.Instance.BeginLoadingLevel += ShowLoadingImage;
			LevelManager.Instance.LoadingSceneComplete += DisapendLoadingImage;
		}
	}

	private void ShowLoadingImage()
	{
		fadeCanvasGroup.alpha = 1;
	}

	private void DisapendLoadingImage()
	{
		StartCoroutine(Fade(0));
	}

	private IEnumerator Fade(float targetAlpha)
	{
		fadeCanvasGroup.blocksRaycasts = true;

		float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / 0.5f;

		while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
		{
			fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
			yield return null;
		}
		fadeCanvasGroup.blocksRaycasts = false;
	}


	private void OnDestroy()
	{
		if (LevelManager.Instance != null)
		{
			LevelManager.Instance.BeginLoadingLevel -= ShowLoadingImage;
			LevelManager.Instance.LoadingSceneComplete -= DisapendLoadingImage;
		}
	}
}
