using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Player : Role
{
    [Header("玩法属性")]
    public float invincibleTime;
    public int maxHeart;

    private int maxLife;
    private int life;
    private bool isInvincible;
    public bool isHidden;

    private Animator animator;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private SetGemController setGemController;
    [SerializeField] private MusicEffectManager musicEffect;


    public event Action GemRecoverd;

    private bool isDie;
	private void OnEnable()
	{
        if(LevelManager.Instance!=null)
		    LevelManager.Instance.LoadingSceneComplete += Initialized;
        GameEventHandler.GameStop += Stop;
        GameEventHandler.GameContinue += Continue;
    }

	private void OnDisable()
	{
        GameEventHandler.GameStop -= Stop;
        GameEventHandler.GameContinue -= Continue;
        if(LevelManager.Instance!=null)
            LevelManager.Instance.LoadingSceneComplete -= Initialized;
    }
    public void Stop()
	{
        playerController.PlayerStop();
        setGemController.Stop();
    }

    public void Continue()
	{
        playerController.PlayerContinue();
        setGemController.Continue();
    }
    private void Initialized()
	{
        animator.SetBool("isDie", false);
        isInvincible = false;
        isHidden = false;
        maxLife = maxHeart * PlayerSetting.HeartLife;
        life = maxLife;
        isDie = false;
		if (MapManger.Instance != null)
		{
            transform.position = MapManger.Instance.initPoint;
		}
        playerController.Initialized();
        setGemController.Initialized();
        PlayerEventHandler.CallPlayerCreate(this);
        Continue();
    }


	private void Awake()
	{
        animator = GetComponentInChildren<Animator>();
        Stop();
    }
	// Start is called before the first frame update
	void Start()
    {
        //setGemController.HoldGemEventHandler += HoldGemEventHandler;
        //setGemController.SetGemEventHandler += SetGemEventHandler;
    }
	// Update is called once per frame
	void Update()
    {
    }
	
    private void FixedUpdate()
	{
        
    }
    private void LifeChange(int lifeChange)
	{
        life += lifeChange;
        life = Mathf.Clamp(life, 0, maxLife);
        PlayerEventHandler.CallPlayerLifeChanged(life);
    }
    IEnumerator DamagedDelay(float second)
	{
        yield return new WaitForSeconds(second);
        playerController.canMoving = true;
    }
    IEnumerator Invincible(float second)
	{
        yield return new WaitForSeconds(second);
        isInvincible = false;
    }
    //抽象类方法
    public override void Damaged(int damageCount, bool isHit = true)
    {
        if (!isInvincible&&!isDie)
        {
            Debug.Log(life);
            animator.SetTrigger("Damaged");
			musicEffect.PlayerAudio("PlayerDamaged");
			isInvincible = true;
            playerController.canMoving = false;
            PlayerEventHandler.CallPlayerDamaged();
            LifeChange(-damageCount);
			if (life <= 0)
			{
                Dead();
            }
            StartCoroutine(Invincible(invincibleTime));
            StartCoroutine(DamagedDelay(1f));
        }
    }
    private void Dead()
	{
        isDie = true;
        animator.SetBool("isDie", true);
        Stop();
        PlayerEventHandler.CallPlayerDeath();
    }

    public override void ChangeSpeed(int changeNumber)
	{
        playerController.moveSpeed += changeNumber;
	}

	public override GameObject SetChilrenGameObect(GameObject gameObject,Vector3 position)
	{
        gameObject.transform.SetParent(transform,true);
        return gameObject;
	}

    public void RecoverdGem(int index, int num)
	{
        setGemController.RecoverGemCount(index, num);
    }
}
