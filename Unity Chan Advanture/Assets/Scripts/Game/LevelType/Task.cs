using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TaskState
{
	Doing,Success,Failure
}
public abstract class Task : MonoBehaviour
{
	public Sprite sprite;
	public event Action TaskSuccessEvent;
	public event Action TaskFailureEvent;
	public event Action<string> TaskProgressChange;
	public TaskState taskState = TaskState.Doing;
	protected virtual void CallTaskProgressChange(string taskInfo)
	{
		TaskProgressChange?.Invoke(taskInfo);
	}
	public virtual void TaskStart()
	{
		PlayerEventHandler.CallLevelTaskStart(this);
	}
	public virtual string GetTaskInfo()
	{
		return "";
	}
	public virtual string GetTaskProgress()
	{
		return "";
	}
	public virtual Sprite GetTaskIcon()
	{
		return sprite;
	}
	protected virtual void TaskSuccess()
	{
		taskState = TaskState.Success;
		TaskSuccessEvent?.Invoke();
	}
	protected virtual void TaskFailure()
	{
		taskState = TaskState.Failure;
		TaskFailureEvent?.Invoke();
	}
	
}
