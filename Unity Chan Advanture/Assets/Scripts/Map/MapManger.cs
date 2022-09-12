using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : Singleton<MapManger>
{
	public Vector3 initPoint;
	private Dictionary<Vector2Int, Cell> map;

	protected override void Awake()
	{
		base.Awake();
		map = new Dictionary<Vector2Int, Cell>();
		UpdateMap();
	}

	private void OnEnable()
	{
		Debug.Log("===");
	}
	//¶ÁÈ¡×©¿éµØÍ¼
	private void UpdateMap()
	{
		map.Clear();
		Cell[] cells = GetComponentsInChildren<Cell>();
		foreach (var item in cells)
		{
			Vector3 position = item.transform.position;
			int x = Convert.ToInt32(position.x / MapSetting.CellSize);
			int y = Convert.ToInt32(position.z / MapSetting.CellSize);
			Vector2Int vector = new Vector2Int(x,y);
			item.SetIndexes(x, y);
			map.Add(vector, item);
		}

	}
	private Vector3 GetRandomPosition()
	{
		int k = map.Keys.Count;
		int index = UnityEngine.Random.Range(0, k);
		int i = 0;
		foreach (var item in map.Keys)
		{
			if (i == index)
			{
				return map[item].transform.position;
			}
		}
		return map[new Vector2Int(0,0)].transform.position;
	}
	public Vector3 GetNextCell(Vector3 position, Vector3 forward)
	{
		int x = Convert.ToInt32(position.x / MapSetting.CellSize);
		int y = Convert.ToInt32(position.z / MapSetting.CellSize);
		Vector2Int p = new Vector2Int(x, y);
		Cell cell=null;
		if (map.TryGetValue(p,out cell))
		{
			return map[cell.GetNextCell(forward)].transform.position;
		}
		return GetRandomPosition();
	}
}
