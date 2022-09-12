using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
	//待机状态
	//原地待机1~3秒
	float timeAmount;
	float idleTime;
	Vector3 playerPoint;
	public IdleState(Animator animator): base(animator)
	{

	}
	public override void LogicUpdate()
	{
		timeAmount += Time.deltaTime;
	}
	public override void PhysicsUpdate()
	{
	}
	public override void EnterState()
	{
		timeAmount = 0;
		idleTime = Random.Range(1f, 3f);
		animator.SetBool("isMove", false);
	}
	public override void ExitState()
	{

	}
	public override int ChangeState()
	{
		if (timeAmount >= idleTime)
		{
			return (int)MonsterStateTpye.Patrol;
		}
		else
		{
			if (Monster.FindPlayer(transform.position,MonsterSetting.IdleViewDistance,MonsterSetting.IdleViewAngle,MonsterSetting.IdleListenConffient,out playerPoint))
			{
				//松懈的观察周围、环境
				return (int)MonsterStateTpye.Pursue;
			}
			else
			{
				return (int)MonsterStateTpye.Idle;
			}
		}
	}
}
