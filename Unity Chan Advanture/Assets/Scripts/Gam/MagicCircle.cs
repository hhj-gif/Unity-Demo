using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle: MonoBehaviour
{
	private GemType _magicType;
	public GemType magicType => _magicType;

	public List<Gem> gems;

	public List<Monster> hitMonsters;
	public bool isMagicActivate;
	public bool canActivate => hitMonsters.Count > 0;

	//渲染属性
	private LineRenderer lineRenderer;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private MeshCollider meshcollider;

	//几何计算属性
	private Vector3 positionSum;
	private Vector3 center { get => positionSum / gems.Count; }

	private bool isCanActivate;
	public void Awake()
	{
		gems = new List<Gem>();

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshcollider = gameObject.AddComponent<MeshCollider>();
		meshcollider.convex = true;
		meshcollider.isTrigger = true;
		hitMonsters = new List<Monster>();
		lineRenderer.startWidth = 0.05f;
		lineRenderer.endWidth = 0.05f;
		lineRenderer.loop = true;
		lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		positionSum = new Vector3(0,0,0);
		isMagicActivate = true;
		isCanActivate = true;
	}
// Start is called before the first frame update
	public void AddGem(Gem gem)
	{
		if (gems.Count == 0)
		{
			_magicType = gem.gemType;
		}
		gem.transform.SetParent(this.transform);
        gems.Add(gem);
		positionSum += gem.transform.position;
		SortPoints();
		UpdateLine();
		UpdateMesh();
	}
	public void AddRangeGem(List<Gem> gems)
	{
		if (gems.Count == 0)
		{
			_magicType = gems[0].gemType;
		}
		foreach (var item in gems)
		{
			item.transform.SetParent(this.transform);
			positionSum += item.transform.position;
			this.gems.Add(item);
		}
		SortPoints();
		UpdateLine();
		UpdateMesh();
	}
	private void UpdateLine()
	{
		if (gems.Count == 1)
		{
			lineRenderer.material = gems[0].gameObject.GetComponent<MeshRenderer>().material;
		}
		lineRenderer.positionCount = gems.Count;
		for (int i = 0; i < gems.Count; i++)
		{
			lineRenderer.SetPosition(i, gems[i].transform.position);
		}
		//lineRenderer.SetPosition(gems.Count, gems[0].transform.position);
	}
	private void SortPoints()
	{
		gems.Sort((a, b) => { return Algorithm.Comparer(center, a, b); });
	}
	private void UpdateMesh()
	{
		if (gems.Count == 1)
		{
			meshRenderer.material = Resources.Load<Material>(PlayerSetting.MagicCirleMaterialPath);
		}
		if (gems.Count >= 3)
		{
			Mesh mesh = new Mesh();
			List<Vector3> vertices = new List<Vector3>();
			List<int> triangles = new List<int>();
			foreach (var item in gems)
			{
				Vector3 p = item.transform.position;
				p.y = PlayerSetting.floorY;
				vertices.Add(p);
			}
			Vector3 center = this.center;
			center.y = PlayerSetting.floorY;
			vertices.Add(center);
			for (int i = 1; i < gems.Count; i++)
			{
				triangles.Add(i - 1);
				triangles.Add(i);
				triangles.Add(gems.Count);
			}
			triangles.Add(gems.Count-1);
			triangles.Add(0);
			triangles.Add(gems.Count);
			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();
			meshFilter.mesh = mesh;
			meshcollider.sharedMesh = mesh;
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (isMagicActivate&&isCanActivate)
		{
			if (other.CompareTag("Monster"))
			{
				Monster monster = other.GetComponentInParent<Monster>();
				if (!hitMonsters.Contains(monster))
				{
					if (PointInMagicCircle(other.transform.position))
					{
						hitMonsters.Add(monster);
					}
					else
					{
						hitMonsters.Remove(monster);
					}
				}
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (isMagicActivate)
		{
			if (other.CompareTag("Monster"))
			{
				Monster monster = other.GetComponentInParent<Monster>();
				if (hitMonsters.Contains(monster))
				{
					hitMonsters.Remove(monster);
				}
			}
		}
	}
	private bool PointInMagicCircle(Vector3 point)
	{
		Vector3 direction = (gems[0].transform.position + gems[1].transform.position) / 2 - point;
		int count = 0;
		for (int i = 0; i < gems.Count; i++)
		{
			Vector3 a = gems[i].transform.position;
			Vector3 b = gems[(i+1)% gems.Count].transform.position;
			if(Algorithm.IsIntersectionWithRay(a, b, point, direction))
			{
				count++;
			}
		}
		return	count%2==1;
	}
	public void DestoryMagicCircle()
	{
		isCanActivate = false;
		hitMonsters.Clear();
		Destroy(gameObject, PlayerSetting.ActivateStrandedTime);
	}
}
