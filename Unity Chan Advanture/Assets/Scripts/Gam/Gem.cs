using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
	[SerializeField] private GemType _gemType;
	[SerializeField] private float _radius;
	[SerializeField] private float _coolingTime;
	[SerializeField] private GemParitcle particle;

	private Player player;
	private int index;
	public void SetPlayer(Player player,int index)
	{
		this.player = player;
		this.index = index;
	}
	public float coolingTime { get => _coolingTime; }
	public GemType gemType { get => _gemType; }
	public float radius { get => _radius; }

	public void OnDestroy()
	{
		if (player)
		{
			GemParitcle gemParitcle = GameObject.Instantiate<GemParitcle>(particle, transform.position, Quaternion.identity);
			gemParitcle.SetPlayer(player, index);
		}
	}

	public void ShowRadius()
	{

	}
}
