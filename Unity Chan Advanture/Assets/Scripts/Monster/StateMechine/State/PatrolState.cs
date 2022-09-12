using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolState : BaseState
{
	NavMeshAgent navMeshAgent;
	Vector3 target;
	Monster role;
	Vector3 playerPoint;

	private const float minDistance = 5f;
	//巡逻状态
	public PatrolState(Animator animator) : base(animator)
	{
		navMeshAgent = transform.GetComponent<NavMeshAgent>();
		role = transform.GetComponent<Monster>();
	}
	public override void LogicUpdate()
	{
	}
	public override void PhysicsUpdate()
	{
	}

	public override void EnterState()
	{
		navMeshAgent.speed = role.speed;
		target = MapManger.Instance.GetNextCell(transform.position, transform.forward);
		navMeshAgent.SetDestination(target);
		Debug.Log(role.speed);
		animator.SetBool("isMove", true);
	}
	public override void ExitState()
	{
		navMeshAgent.speed = 0;
		animator.SetBool("isMove", false);
	}
	public override int ChangeState()
	{
		//判断是否到达目的地

		if (Vector3.Distance(transform.position,target)<0.5f)
		{
			//到达目的地后休息
			return (int)MonsterStateTpye.Idle;
		}
		//判断是否发现Player
		if (Monster.FindPlayer(transform.position,MonsterSetting.PatrolViewDistance,MonsterSetting.PatrolViewAngle,MonsterSetting.PatrolListenConffient,out playerPoint))
		{
			//发现后进行追击
			return (int)MonsterStateTpye.Pursue;
		}
		return (int)MonsterStateTpye.Patrol;
	}

}
