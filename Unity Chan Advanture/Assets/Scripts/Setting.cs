using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting
{
	//��Ļ��˸����
	public const int RadFlashTime = 1;
	public const float RadFlashEachTime = 0.3f;

	//Ѫ��UI����
	public const int HeartLife = 2;
	
	//ħ����Material ��Դ·��
	public const string MagicCirleMaterialPath = "Materials/MagicCircle";
	//ħ����y���С
	public const float floorY = 10.2f;

	public const float ActivateStrandedTime = 1f;

	// ����ʱ��
	public const float comboContinue = 5f;
}

public class MagicSetting
{
	public const string GemLinkerPath = "Gem/GemLinker";

	//buffЧ������Ч
	public const string BurnBuffParticlePath = "BuffEffect/Burn";
	public const string ColdBuffParticlePath = "BuffEffect/Cold";



	//ħ����Ч��Ч��
	public const int FireDamage = 15;
	public const int FireBurnTime = 5;
	public const int FireBurnDamage = 1;
	public const string FireParticlePath = "MagicEffect/Fire";


	public const int ColdDamage = 10;
	public const int ColdTime = 5;
	public const string ColdParticlePath = "MagicEffect/Cold";

}

public class MonsterSetting
{
	public const string DestoryMaterialPath = "Materials/DissolveEmissionMaterial";
	public const float BackRate = 0.8f;
	//��������˶��ӽǲ���
	public const float IdleViewDistance = 10f;
	public const float IdleViewAngle = 60f;
	public const float IdleListenConffient = 3f;

	//����Ѳ���˶��ӽǲ���
	public const float PatrolViewDistance = 10f;
	public const float PatrolViewAngle = 30f;
	public const float PatrolListenConffient = 4f;
	public const float PatrolMoveSpeed = 2f;

	//����׷���˶��ӽǲ���
	public const float PursueViewDistance = 20f;
	public const float PursueViewAngle = 15f;
	public const float PursueListenConffient = 10f;
	public const float AtteckRange = 1.2f;
	public const float PursueMoveSpeed = 4f;
}
public class RabbitSetting
{
	public const float BackRate = 0.8f;
	//��������˶��ӽǲ���
	public const float IdleViewDistance = 10f;
	public const float IdleViewAngle = 60f;
	public const float IdleListenConffient = 3f;

	//����Ѳ���˶��ӽǲ���
	public const float PatrolViewDistance = 10f;
	public const float PatrolViewAngle = 30f;
	public const float PatrolListenConffient = 4f;
	public const float PatrolMoveSpeed = 2f;

	//����׷���˶��ӽǲ���
	public const float PursueViewDistance = 20f;
	public const float PursueViewAngle = 15f;
	public const float PursueListenConffient = 10f;
	public const float AtteckRange = 2f;
	public const float PursueMoveSpeed = 4f;
}
public class MapSetting
{
	public const float CellSize = 3.8f;
}