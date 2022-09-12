using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : Singleton<LevelManager>
{
	private List<Task> tasks;
	private int tasksCount;
	private int completeTaskCount;
	private int levelIndex;
	private int nextLevelIndex;
	private int deathCount;
	public string levelName => string.Format("Level{0:D2}", levelIndex);

	public event Action BeginLoadingLevel;
	public event Action LoadingSceneComplete;

	protected override void Awake()
	{
		base.Awake();
		levelIndex = -1;
		tasks = new List<Task>();
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += LoadedSceneCmplete;
		PlayerEventHandler.PlayerDeath += PlayerEventHandler_PlayerDeath;
		LoadingSceneComplete += UpdateLevel;
		LoadingSceneComplete += GameEventHandler.CallGameContinue;
		BeginLoadingLevel += GameEventHandler.CallGameStop;
	}


	private void OnDisable()
	{
		//先执行场景下的awake,onEnable 再执行SceneManager.sceneLoaded事件
		SceneManager.sceneLoaded -= LoadedSceneCmplete;
		PlayerEventHandler.PlayerDeath -= PlayerEventHandler_PlayerDeath;
		LoadingSceneComplete -= UpdateLevel;
		LoadingSceneComplete -= GameEventHandler.CallGameContinue;
		BeginLoadingLevel -= GameEventHandler.CallGameStop;
	}

	private void PlayerEventHandler_PlayerDeath()
	{
		deathCount++;
		if (deathCount > 0)
		{
			GameEventHandler.CallGameFailure(new ResultEventArge());
		}
	}

	private void LoadedSceneCmplete(Scene arg0, LoadSceneMode arg1)
	{
		if (arg0.name.Contains("Level"))
		{
			LoadingSceneComplete?.Invoke();
			Debug.Log("loaded scene");
		}
	}
	public void RestartLevel()
	{
		LoadLevel(levelIndex);
	}

	public void LoadLevel(int index)
	{
		nextLevelIndex = index;
		StartCoroutine(LoadScene(index));
	}

	public IEnumerator LoadScene(int levelIndex)
	{
		if(this.levelIndex!=-1)
			yield return SceneManager.UnloadSceneAsync(string.Format("Level{0:D2}", this.levelIndex));

		BeginLoadingLevel?.Invoke();
		yield return SceneManager.LoadSceneAsync(string.Format("Level{0:D2}", levelIndex), LoadSceneMode.Additive);
		this.levelIndex = levelIndex;
	}

	private void UpdateLevel()
	{
		tasks.Clear();
		completeTaskCount = 0;
		GameObject levelGameObject = GameObject.FindGameObjectWithTag("Level");
		tasks.AddRange(levelGameObject.GetComponents<Task>());
		for (int i = 0; i < tasks.Count; i++)
		{
			tasks[i].TaskSuccessEvent += LevelManager_TaskSuccessEvent;
			tasks[i].TaskFailureEvent += LevelManager_TaskFailureEvent;
			tasks[i].TaskStart();
		}
		deathCount = 0;
		tasksCount = tasks.Count;
	}
	public void ExitLevel()
	{
		this.levelIndex = -1;
	}
	private void LevelManager_TaskFailureEvent()
	{
		GameEventHandler.CallGameFailure(new ResultEventArge());
	}

	private void LevelManager_TaskSuccessEvent()
	{
		completeTaskCount++;
		if (completeTaskCount == tasksCount)
		{
			GameEventHandler.CallGameSuccess(new ResultEventArge() { starNumber = 3 });
		}
	}

	public void StartNextLevel()
	{
		GameReluManager.Instance.ReturnMainMenu();
	}
}
