using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReluManager : Singleton<GameReluManager>
{
	private int startIndex;
	private void Start()
	{
		GameObject.DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		Debug.Log(arg0.name);
		if (arg0.name.Equals("UI"))
		{
			LevelManager.Instance.LoadLevel(startIndex);

		}
	}
	public void GameStart(int index)
	{
		startIndex = index;
		SceneManager.LoadScene("Play 1", LoadSceneMode.Single);
		SceneManager.LoadScene("UI", LoadSceneMode.Additive);
	}

	public void CloseGame()
	{
		#if UNITY_EDITOR    //在编辑器模式下
			EditorApplication.isPlaying = false;
		#else
			Application.Quit();	
		#endif
	}
	public void GameOver()
	{

	}

	public void ReturnMainMenu()
	{
		LevelManager.Instance.ExitLevel();
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
