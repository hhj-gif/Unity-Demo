using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueState : BaseState
{
	private NavMeshAgent navMeshAgent;

	private Vector3 playerPoint;

	Monster role;
	//追击状态
	public PursueState(Animator animator) : base(animator)
	{
		transform = animator.transform;
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
		//navMeshAgent.enabled = true;

		animator.SetBool("isMove", true);
		playerPoint = PlayerManager.Instance.GetPlayerPosition();
		navMeshAgent.speed = role.speed * 1.2f;
		navMeshAgent.stoppingDistance = 0;
		navMeshAgent.SetDestination(playerPoint);
	}
	public override void ExitState()
	{
		//navMeshAgent.enabled = false;
		navMeshAgent.speed = 0;
		navMeshAgent.stoppingDistance = 0;
		animator.SetBool("isMove", false);
	}
	public override int ChangeState()
	{

		if (!Monster.FindPlayer(transform.position, MonsterSetting.PursueViewDistance, MonsterSetting.PursueViewAngle, MonsterSetting.PursueListenConffient,out playerPoint))
		{
			return (int)MonsterStateTpye.Idle;
		}
		else if (Vector3.Distance(transform.position, playerPoint) < MonsterSetting.AtteckRange)
		{
			return (int)MonsterStateTpye.Attack;
		}
		else
		{
			navMeshAgent.SetDestination(playerPoint);
			return (int)MonsterStateTpye.Pursue;
		}
	}
}
