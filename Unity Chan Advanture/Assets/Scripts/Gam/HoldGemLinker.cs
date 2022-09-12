using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Gem))]
public class HoldGemLinker : MonoBehaviour
{
	GemLinker linkerPerfab;
    HashSet<Gem> linkGems;
    Gem startGem;
	Dictionary<Gem, GemLinker> lines;
	List<Gem> removeKey;
	private void Awake()
	{
		startGem = GetComponent<Gem>();
		linkGems = new HashSet<Gem>();
		lines = new Dictionary<Gem, GemLinker>();
		removeKey = new List<Gem>();
		linkerPerfab = Resources.Load<GemLinker>(MagicSetting.GemLinkerPath);
		if (linkerPerfab == null)
		{
			Debug.Log("fasfasd");
		}
	}
    void Update()
    {
		GemManager.Instance.TestGemIsClose(startGem, ref linkGems);
		GemLinker lineRenderer;
		removeKey.Clear();
		foreach (var item in lines)
		{
			if (linkGems.Contains(item.Key)){
				lineRenderer = item.Value;
				lineRenderer.SetStartPoint(this.transform.position);
			}
			else
			{
				Destroy(item.Value.gameObject);
				removeKey.Add(item.Key);
			}
		}

		foreach (var item in removeKey)
		{
			lines.Remove(item);
		}

		foreach (var item in linkGems)
		{
			if (!lines.ContainsKey(item))
			{
				lineRenderer = GameObject.Instantiate(linkerPerfab, transform);
				Vector3[] vectors = new Vector3[2];
				vectors[0] = this.transform.position;
				vectors[1] = item.transform.position;
				if (lineRenderer)
				{
					lineRenderer.SetPoints(vectors);
					lines.Add(item, lineRenderer);
				}
			}
		}
	}

	private void OnDestroy()
	{
		foreach (var item in lines)
		{
			Destroy(item.Value.gameObject);
		}

	}
}
