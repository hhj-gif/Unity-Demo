using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : BaseState
{
	bool isDie = false;
	bool isCanAttack;
	float attackDelayTime = 2f;
	float time = 0;
	Vector3 playerPosition;
	//待机状态
	public AttackState(Animator animator) : base(animator)
	{

	}
	public override void LogicUpdate()
	{
		playerPosition = PlayerManager.Instance.GetPlayerPosition();
		if (!isCanAttack)
		{
			time += Time.deltaTime;
			if (time > attackDelayTime)
			{
				isCanAttack = true;
			}
		}
		else if (isCanAttack)
		{
			transform.LookAt(playerPosition);
			animator.SetTrigger("attack");
			time = 0;
			isCanAttack = false;
		}
	}
	public override void PhysicsUpdate()
	{
	}
	public override void EnterState()
	{
		isCanAttack = true;
		time = attackDelayTime;
	}
	public override void ExitState()
	{

	}
	public override int ChangeState()
	{
		if (Vector3.Distance(transform.position, playerPosition) > MonsterSetting.AtteckRange)
		{
			Debug.Log("超出攻击范围");
			return (int)MonsterStateTpye.Pursue;
		}
		return (int)MonsterStateTpye.Attack;
	}
}
