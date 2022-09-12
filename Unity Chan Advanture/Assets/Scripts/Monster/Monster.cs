using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Monster : StateMechine
{
    public int maxHP;
	[SerializeField] int attackDamage;
	[SerializeField] int score;

	private Animator animator;
    private int currentHP;
	private bool isAttack;
	protected NavMeshAgent navMeshAgent;
	private MonsterBloodUI bloodUI;
	private bool isDie;

	private void Awake()
	{
		currentHP = maxHP;
		animator = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		isDie = false;
		states = new Dictionary<int, BaseState>();
		states.Add((int)MonsterStateTpye.Idle, new IdleState(animator));
		states.Add((int)MonsterStateTpye.Patrol, new PatrolState(animator));
		states.Add((int)MonsterStateTpye.Pursue, new PursueState(animator));
		states.Add((int)MonsterStateTpye.GetHit, new HittedState(animator));
		states.Add((int)MonsterStateTpye.Die, new DieState(animator));
		states.Add((int)MonsterStateTpye.Attack, new AttackState(animator));
		SwitchState((int)MonsterStateTpye.Idle, states[(int)MonsterStateTpye.Idle]);

		isAttack = false;
		Init();
		GameEventHandler.GameStop += Stop;
		GameEventHandler.GameContinue += Continue;
	}

	private void Init()
	{
        currentHP = maxHP;
		MonsterEventHandler.CallCreateMonster(this);
	}
	// Start is called before the first frame update
	protected void Start()
    {

    }

	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.L))
		{
			Damaged(50);
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}
	public void SetBloodUI(MonsterBloodUI bloodUI)
	{
		this.bloodUI = bloodUI;
	}
	public static bool FindPlayer(Vector3 point, float viewDistance, float viewAngle,float ListenCoffient,out Vector3 playerPoint)
	{
		float distance = PlayerManager.Instance.GetDistance(point, out playerPoint);
		if (distance <= viewDistance)
		{
			float angle = Vector3.Angle(point, playerPoint);
			if (angle <= viewAngle)
			{
				return true;
			}
			else if (distance <= viewDistance / ListenCoffient)
			{
				return true;
			}
		}
		return false;
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (isAttack)
			{
				other.GetComponent<Player>().Damaged(attackDamage);
			}
		}
	}
	//¶¯»­ÊÂ¼þ
	public void StopDamaged()
	{
		this.SwitchState((int)MonsterStateTpye.Patrol, states[(int)MonsterStateTpye.Patrol]);
	}
	public void StartAttact()
	{
		isAttack = true;
	}
	public void StopAttact()
	{
		isAttack = false;
	}

	public override void Damaged(int damage, bool isHit = true)
	{
		currentHP -= damage;
		if (currentHP > 0)
		{
			bloodUI.UpdateBlood(currentHP);
			if (isHit)
			{
				SwitchState((int)MonsterStateTpye.GetHit, states[(int)MonsterStateTpye.GetHit]);
			}
		}
		else
		{
			if (currentState as DieState == null)
			{
				bloodUI.UpdateBlood(0);
				isDie = true;
				MonsterEventHandler.CallDeadMonster(this);
				SwitchState((int)MonsterStateTpye.Die, states[(int)MonsterStateTpye.Die]);
			}
		}
	}
	public override void ChangeSpeed(int changeNumber)
	{
		speed += changeNumber;
		if (speed < 0.5)
		{
			speed = 0.5f;
		}
		SwitchState(stateNum,currentState);
	}

	public override void Stop()
	{
		base.Stop();
		animator.speed = 0;
		navMeshAgent.enabled = false;
		//SwitchState((int)MonsterStateTpye.Idle, states[(int)MonsterStateTpye.Idle]);
	}
	public override void Continue()
	{
		base.Continue();
		navMeshAgent.enabled = true;
		SwitchState(stateNum, currentState);
		animator.speed = 1;
	}

	public int GetScore()
	{
		return score;
	}
	protected virtual void OnDestroy()
	{
		GameEventHandler.GameStop -= Stop;
		GameEventHandler.GameContinue -= Continue;
	}
}
