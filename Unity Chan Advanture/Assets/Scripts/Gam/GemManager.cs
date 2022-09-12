using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GemManager : Singleton<GemManager>
{
	private Dictionary<GemType,List<MagicCircle>> GemsSetInWorld;

	protected override void Awake()
	{
		base.Awake();
		GemsSetInWorld = new Dictionary<GemType, List<MagicCircle>>();
	}

	private void OnEnable()
	{
		PlayerEventHandler.PlayerCreate += Clear;
	}

	private void OnDisable()
	{
		PlayerEventHandler.PlayerCreate -= Clear;

	}

	public HashSet<Gem> TestGemIsClose(Gem gem,ref HashSet<Gem> result)
	{
		if (GemsSetInWorld.ContainsKey(gem.gemType))
		{
			List<MagicCircle> typeGems = GemsSetInWorld[gem.gemType];
			//判断是否有相同类型的Gem与之相近
			for (int i = 0; i < typeGems.Count; i++)
			{
				foreach (var item in typeGems[i].gems)
				{
					if (Vector3.Distance(item.transform.position, gem.transform.position) <= gem.radius)
					{
						
						result.Add(item);
					}
					else
					{
						if (result.Contains(item))
						{
							result.Remove(item);
						}
					}
				}
			}
		}
		return result;
	}
	public void AddGemsInWold(Gem gem)
	{
		if (GemsSetInWorld.ContainsKey(gem.gemType))
		{
			List<MagicCircle> typeGems = GemsSetInWorld[gem.gemType];
			//判断新加入的gem是否可以和现有的组成阵型
			List<int> result = new List<int>();
			for (int i = 0; i < typeGems.Count; i++)
			{
				foreach (var item in typeGems[i].gems)
				{
					if (Vector3.Distance(item.transform.position, gem.transform.position) <= gem.radius)
					{
						result.Add(i);
						break;
					}
				}
			}
			//和一个现有的阵型相交
			if (result.Count == 1)
			{
				typeGems[result[0]].AddGem(gem);
			}
			else if(result.Count>1)
			{
				//和多个阵型相交
				UnionGems(result, gem.gemType);
				typeGems[result[0]].AddGem(gem);
				for (int i = result.Count-1;  i>0; i--)
				{
					MagicCircle removeMagicCircle = typeGems[result[i]];
					typeGems.Remove(removeMagicCircle);
					Destroy(removeMagicCircle.gameObject);
				}
			}
			else
			{
				//没有相交
				GameObject gameObject = new GameObject("magic");
				MagicCircle newMagicLink = gameObject.AddComponent<MagicCircle>();
				gameObject.transform.SetParent(transform);
				typeGems.Add(newMagicLink);
				newMagicLink.AddGem(gem);
			}
		}	
		else
		{
			//不存在魔法阵
			GameObject gameObject = new GameObject("magic");
			MagicCircle newMagicCircle = gameObject.AddComponent<MagicCircle>();
			gameObject.transform.SetParent(transform);
			newMagicCircle.AddGem(gem);
			GemsSetInWorld.Add(gem.gemType, new List<MagicCircle>() { newMagicCircle });
		}
	}
	
	/// <summary>
	/// 合并多个gem,并保存在第一个
	/// </summary>
	/// <param name="results"></param>
	/// <param name="gemType"></param>
	/// <returns></returns>
	private MagicCircle UnionGems(List<int> results,GemType gemType)
	{
		List<MagicCircle> typeGems = GemsSetInWorld[gemType];
		MagicCircle gems = typeGems[results[0]];
		for (int i = 1; i < results.Count; i++)
		{
			gems.AddRangeGem(typeGems[results[i]].gems);
		}
		return gems;
	}
	
	public int ActivateMagic()
	{
		Dictionary<Monster, int> hittedMonsters = new Dictionary<Monster, int>();
		//判断击中魔法的Monster
		List<MagicCircle> activateCircle = new List<MagicCircle>();
		foreach (var item in GemsSetInWorld.Keys)
		{
			foreach (var magicCircle in GemsSetInWorld[item])
			{
				if (magicCircle.hitMonsters.Count != 0)
				{
					activateCircle.Add(magicCircle);
					for (int i = 0; i < magicCircle.hitMonsters.Count; i++)
					{
						if (!hittedMonsters.ContainsKey(magicCircle.hitMonsters[i]))
						{
							hittedMonsters.Add(magicCircle.hitMonsters[i], (int)item);
						}
						else
						{
							hittedMonsters[magicCircle.hitMonsters[i]] += (int)item;
						}
					}
				}
			}
		}
		//判断Monster被击中的魔法类型
		foreach (var item in hittedMonsters)
		{
			MagicType magicType = (MagicType)item.Value;
			//Debug.Log(item.Key.name+ magicType.ToString());
			Type magic = MagicManager.GetMagic(magicType);
			item.Key.gameObject.AddComponent(magic);
		}

		//消除已经发动的魔法阵
		foreach (var item in activateCircle)
		{
			GemsSetInWorld[item.magicType].Remove(item);
			item.DestoryMagicCircle();
		}
		return hittedMonsters.Count;
	}

	public int ActivateMagic(GemType []gemTypes,int []index)
	{
		return 0;
	}

	public float GetShortestDistance(Vector3 point,out Gem target)
	{
		float shortestDistance = float.MaxValue;
		target = null;
		foreach (var item in GemsSetInWorld.Keys)
		{
			foreach (var magicCircle in GemsSetInWorld[item])
			{
				for (int i = 0; i < magicCircle.gems.Count; i++)
				{
					float distance = Vector3.Distance(point, magicCircle.gems[i].transform.position);
					if (distance < shortestDistance)
					{
						shortestDistance = distance;
						target = magicCircle.gems[i];
					}
				}
			}
		}
		return shortestDistance;
	}

	private void Clear(Player player)
	{
		foreach (var item in GemsSetInWorld)
		{
			foreach (var magicCircle in item.Value)
			{
				Destroy(magicCircle.gameObject);
			}
		}
		GemsSetInWorld.Clear();
	}
}
