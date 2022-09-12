using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	//z轴正方向是前，负方向是后;x轴正方向是右,负方向左
	[SerializeField] private bool canForward;
	[SerializeField] private bool canLeft;
	[SerializeField] private bool canBack;
	[SerializeField] private bool canRight;

	private bool reviseForward;
	private bool reviseLeft;
	private bool reviseBack;
	private bool reviseRight;
	private int x;
	private int y;
	private List<Vector2Int> mayNext;

	private void Awake()
	{
		mayNext = new List<Vector2Int>();
		Revise();
	}
	public Vector2Int GetNextCell(Vector3 forward)
	{
		int forwardX = forward.x > 0 ? 1 : -1;
		int forwardY = forward.z > 0 ? 1 : -1;

		//判断是前后方向还是左右方向前进
		bool isFB = Mathf.Abs(forward.z) > Mathf.Abs(forward.x);
		float back = UnityEngine.Random.Range(0, 1.0f);
		Vector2Int backIndex = new Vector2Int();
		if (isFB)
		{
			backIndex = new Vector2Int(x, y - forwardY);
		}
		else
		{
			backIndex = new Vector2Int(x - forwardX, y);
		}
		//判断是否返回
		//Debug.Log(x+" "+y);
		if (back > MonsterSetting.BackRate) 
		{
			if (mayNext.Contains(backIndex))
			{
				return backIndex;
			}
			else
			{
				return mayNext[0];
			}
		}
		else
		{
			int nextIndex = UnityEngine.Random.Range(0, mayNext.Count-1);
			int j = 0;
			for (int i = 0; i < mayNext.Count; i++)
			{
				if (mayNext[i].Equals(backIndex))
				{
					continue;
				}
				if (nextIndex == j)
				{
					return mayNext[i];
				}
				j++;
			}
		}
		return backIndex;
	}
	private void Revise()
	{
		int angleY = Convert.ToInt32(transform.eulerAngles.y);
		switch (angleY)
		{
			case 0:
				reviseForward = canForward;
				reviseLeft = canLeft;
				reviseBack = canBack;
				reviseRight = canRight;
				break;
			case 90:
				reviseForward = canLeft;
				reviseLeft = canBack;
				reviseBack = canRight;
				reviseRight = canForward;
				break;
			case 180:
				reviseForward = canBack;
				reviseLeft = canRight;
				reviseBack = canForward;
				reviseRight = canLeft;
				break;
			case 270:
				reviseForward = canRight;
				reviseLeft = canForward;
				reviseBack = canLeft;
				reviseRight = canBack;
				break;
			default:
				break;
		}
	}

	public void SetIndexes(int x,int y)
	{
		this.x = x;
		this.y = y;
		if (reviseForward)
		{
			mayNext.Add(new Vector2Int(x,y+1));
		}
		if (reviseBack)
		{
			mayNext.Add(new Vector2Int(x, y-1));
		}
		if (reviseRight)
		{
			mayNext.Add(new Vector2Int(x+1, y));
		}
		if (reviseLeft)
		{
			mayNext.Add(new Vector2Int(x-1, y));
		}
	}
	
}
