using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TaskUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskTextMesh;
    [SerializeField] Image teskIcon;
	private Task task;
	public void SetTask(Task task)
	{
		this.task = task;
		this.task.TaskProgressChange += ChangeTaskNumber;
		SetTaskInfo(task.GetTaskProgress(), task.GetTaskIcon());
	}
    private void SetTaskInfo(string taskNumber,Sprite icon)
	{
		teskIcon.sprite = icon;
		taskTextMesh.text = taskNumber;
	}
	private void ChangeTaskNumber(string taskNumber)
	{
		taskTextMesh.text = taskNumber;
	}
}
