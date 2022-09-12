using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Transform> bornPoints;
    public int maxCreateNumber = 10;
    public int startNumber = 5;
    public int maxLiveNumber = 5;
    public List<Monster> monstersType;
    private int createNumber=>monsters.Count;
    private List<Monster> monsters;
	private void Awake()
	{
        monsters = new List<Monster>();
    }

	private void OnDisable()
	{
		MonsterEventHandler.DeadMonster -= DeadMonster;
	}

	private void OnEnable()
	{
		MonsterEventHandler.DeadMonster += DeadMonster;
    }

    private void DeadMonster(Monster monster)
	{
        monsters.Remove(monster);
		if (monsters.Count < maxLiveNumber)
		{
            CreateMonster(maxLiveNumber - monsters.Count);
        }
    }

	// Start is called before the first frame update
	void Start()
    {
		if (startNumber > maxCreateNumber)
		{
            startNumber = maxCreateNumber;
		}
        CreateMonster(startNumber);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 随机生成number个怪物
    /// </summary>
    /// <param name="number"></param>
    void CreateMonster(int number)
	{
        for(int i = 0; i < number; i++)
		{
			int pointIndex = UnityEngine.Random.Range(0, bornPoints.Count);
			int monsterIndex = UnityEngine.Random.Range(0, monstersType.Count);
            Vector3 point = bornPoints[pointIndex].position;
            point.y = 10;
            monsters.Add(GameObject.Instantiate(monstersType[monsterIndex], point, Quaternion.identity, transform));
		}
    }
}
