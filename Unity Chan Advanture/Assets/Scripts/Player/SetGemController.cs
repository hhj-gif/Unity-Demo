using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SetGemController : MonoBehaviour
{
	public event Action<GemEventHandlerArges> HoldGemEventHandler;
	public event Action<GemEventHandlerArges> SetGemEventHandler;
	public event Action<GemEventHandlerArges> RecovedGemEventHandler;

	public event Action ActivateEventHandler;

	[SerializeField] private MusicEffectManager musicEffectManager;
	[SerializeField] private int[] gemsCount;
	[SerializeField] private Player player;

	public Gem []gemPrefabs;
	public GameObject gemsParent;

	private bool isHold;
	private int index;
	private Gem holdGem;
	private HoldGemLinker holdGemLinker;

	//宝石使用限制
	private float[] gemCoolingTime;
	private bool[] gemIsUse;
	private int[] usedGemsCount;
	private bool isStop;
	private int gemTypeNumber;
	private void Awake()
	{
		Initialized();
	}
	private void OnEnable()
	{
		PlayerEventHandler.ChangedGamIndex += PlayerEventHandler_ChangedGamIndex;
	}

	private void OnDisable()
	{
		PlayerEventHandler.ChangedGamIndex -= PlayerEventHandler_ChangedGamIndex;
	}

	public void Initialized()
	{
		index = 0;
		isHold = false;
		isStop = false;
		gemTypeNumber = gemPrefabs.Length;
		gemCoolingTime = new float[gemTypeNumber];
		gemIsUse = new bool[gemTypeNumber];
		usedGemsCount = new int[gemsCount.Length];
		for (int i = 0; i < gemTypeNumber; i++)
		{
			gemCoolingTime[i] = gemPrefabs[i].coolingTime;
			gemIsUse[i] = true;
		}
	}

	private void PlayerEventHandler_ChangedGamIndex(int obj)
	{
		musicEffectManager.PlayerAudio("ChangeGem");
	}

	public void Stop()
	{
		isStop = true;
	}

	public void Continue()
	{
		isStop = false;
	}

	private void Update()
	{
		if (!isStop)
		{
			if (isHold)
			{
				if (Input.GetKeyUp(KeyCode.Space))
				{
					Set();
				}
				else if (Input.GetKeyDown(KeyCode.F))
				{

					DestoryGem();
					isHold = false;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Q))
				{
					index++;
					if (index >= gemTypeNumber)
					{
						index = 0;
					}
					PlayerEventHandler.CallChangedGamIndex(index);
				}
				else if (Input.GetKeyDown(KeyCode.E))
				{
					index--;
					if (index < 0)
					{
						index = gemTypeNumber - 1;
					}
					PlayerEventHandler.CallChangedGamIndex(index);
				}
				else if (Input.GetKeyDown(KeyCode.Space))
				{
					Hold();
				}
			}
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				StartCoroutine(nameof(ActivateMagic));
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				Time.timeScale = 0;
			}
		}
	}
	private void ActivateMagic()
	{
		GemManager.Instance.ActivateMagic();
		ActivateEventHandler?.Invoke();
	}
	private void Hold()
	{
		if (gemIsUse[index])
		{
			musicEffectManager.PlayerAudio("HoldGem");
			isHold = true;
			Vector3 position = this.transform.position;
			position.y += 0.5f;
			position += transform.forward * 0.5f;
			holdGem = GameObject.Instantiate<Gem>(gemPrefabs[index], position, Quaternion.Euler(-90, 0, 0), transform);
			
			holdGemLinker = holdGem.gameObject.AddComponent<HoldGemLinker>();
		}
		else
		{
			musicEffectManager.PlayerAudio("HoldGemFailure");
		}
		HoldGemEventHandler?.Invoke(new GemEventHandlerArges(){
			isSuccess = gemIsUse[index],
			coolingTime = gemCoolingTime[index],
			index = index,
			count = gemsCount[index] - usedGemsCount[index],
			gem = holdGem
		});
	}
	private void Set()
	{
		if (isHold&&holdGem != null)
		{
			musicEffectManager.PlayerAudio("SetGem");
			isHold = false;
			GemManager.Instance.AddGemsInWold(holdGem.GetComponent<Gem>());
			usedGemsCount[index]++;
			SetGemEventHandler?.Invoke(new GemEventHandlerArges()
			{
				isSuccess = true,
				coolingTime = gemCoolingTime[index],
				index = index,
				count = gemsCount[index]-usedGemsCount[index],
				gem = holdGem
			});
			holdGem.SetPlayer(player, index);
			StartCoroutine(StartCoolTime(index, gemCoolingTime[index]));
			Destroy(holdGemLinker);
			holdGem = null;
		}
	}

	public void RecoverGemCount(int index,int count)
	{
		usedGemsCount[index] -= count;
		RecovedGemEventHandler?.Invoke(new GemEventHandlerArges() 
		{
			index = index,
			count = gemsCount[index] - usedGemsCount[index],
		});
	}
	private void DestoryGem()
	{
		Destroy(holdGemLinker);
		holdGemLinker = null;
		Destroy(holdGem.gameObject);
		holdGem = null;
	}

	private IEnumerator StartCoolTime(int index,float time)
	{
		gemIsUse[index] = false;
		yield return new WaitForSeconds(time);
		gemIsUse[index] = true;
	}

	public int GetMaxCount(int index)
	{
		return gemsCount[index];
	}

	private void OnDestroy()
	{
		PlayerEventHandler.ChangedGamIndex -= PlayerEventHandler_ChangedGamIndex;
	}
}
