using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeskBarManager : MonoBehaviour
{
	[SerializeField] private TaskUI taskUIProfab;

	private List<TaskUI> taskUIs;
	private void Awake()
	{
		taskUIs = new List<TaskUI>();
	}
	private void Start()
	{
	}
	private void OnEnable()
	{
		PlayerEventHandler.LevelTaskStart += AddTask;
		if (LevelManager.Instance != null)
			LevelManager.Instance.BeginLoadingLevel += Clear;
	}
	private void OnDisable()
	{
		PlayerEventHandler.LevelTaskStart -= AddTask;
		if(LevelManager.Instance!=null)
			LevelManager.Instance.BeginLoadingLevel -= Clear;
	}
	public void AddTask(Task task)
	{
		TaskUI taskUI = GameObject.Instantiate(taskUIProfab, transform);
		taskUI.SetTask(task);
		taskUIs.Add(taskUI);
	}
	private void Clear()
	{
		foreach (var item in taskUIs)
		{
			GameObject.Destroy(item.gameObject);
		}
		taskUIs.Clear();
	}
}
