using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Monster
{
	public GameObject bodyControl;
	public void StartMove()
	{
		if (this.stateNum == (int)MonsterStateTpye.Patrol)
		{
			navMeshAgent.speed = speed;
		}
		else if (this.stateNum == (int)MonsterStateTpye.Pursue)
		{
			navMeshAgent.speed = speed * 1.2f;
		}
	}
	public void StopMove()
	{
		navMeshAgent.speed = 0;
	}
	public override GameObject SetChilrenGameObect(GameObject gameObject,Vector3 position)
	{
		GameObject result =  Instantiate(gameObject, position, Quaternion.identity, bodyControl.transform);
		result.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
		return result;
	}
}
