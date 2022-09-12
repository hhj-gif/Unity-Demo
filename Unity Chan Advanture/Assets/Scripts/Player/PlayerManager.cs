using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager:Singleton<PlayerManager>
{
	public Vector3 initPoint;
	[SerializeField] private Player player;
	public float GetDistance(Vector3 point,out Vector3 target, bool isForce=false)
	{
		target = player.transform.position;
		float distance = Vector3.Distance(point, target);
		//可以加一个射线检测，检测角色和目标之间是否存在障碍物
		if (player.isHidden&& !isForce)
		{
			return float.MaxValue;
		}
		else 
		{
			return distance;
		}
	}
	public Vector3 GetPlayerPosition()
	{
		Vector3 position = player.transform.position;
		position.y = 10;
		return position;
	}
}
