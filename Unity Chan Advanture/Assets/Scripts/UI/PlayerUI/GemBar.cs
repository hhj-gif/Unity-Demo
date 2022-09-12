using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GemBar : MonoBehaviour
{
	public GameObject gamCarema;
	public GameObject caremaParent;
	public GemImage backgroudImage;

	private List<GemImage> backgroudImages;
	private List<Vector3> backgroundImagePosition;
	private List<Vector3> backgroundImageScale;

	private List<GemCamera> gemCemares;
	private int currentIndex;
	SetGemController setGemController;

	private Gem[] gams;

	private void OnEnable()
	{
		PlayerEventHandler.ChangedGamIndex += UpdateIndex;
		PlayerEventHandler.PlayerCreate += Initialized;
	}

	private void OnDisable()
	{
		PlayerEventHandler.ChangedGamIndex -= UpdateIndex;
		PlayerEventHandler.PlayerCreate -= Initialized;
	}
	private void Awake()
	{
		currentIndex = 0;
		backgroudImages = new List<GemImage>();
		gemCemares = new List<GemCamera>();
		backgroundImagePosition = new List<Vector3>();
		backgroundImageScale = new List<Vector3>();
	}

	private void Start()
	{
	}
	private void Initialized(Player player)
	{
		Clear();
		//生成Rander Texture
		setGemController = player.GetComponent<SetGemController>();
		gams = setGemController.gemPrefabs;
		setGemController.SetGemEventHandler += SetGem;
		setGemController.RecovedGemEventHandler += UpdeteGem;
		Vector3 position = new Vector3(1,100,100);
		for (int i = 0; i < gams.Length; i++)
		{
			gemCemares.Add(GameObject.Instantiate(gamCarema, position, Quaternion.identity, caremaParent.transform).GetComponent<GemCamera>());
			position.x++;
			gemCemares[i].SetGam(gams[i].gameObject);
		}

		//生成UI;
		int j = 0;
		Vector3 initPosition = new Vector3(0, 0, 0);
		Vector3 initScale = new Vector3(1, 1, 1);
		Vector3 diffPosinton = new Vector3(0, 0, 0);
		//水平偏移每一层偏移多少，每一层减半
		int d = 50;
		//目前偏移量，左右正负转换
		int t = 0;
		foreach (var item in gemCemares)
		{
			backgroudImages.Add(GameObject.Instantiate(backgroudImage, this.transform));
			RawImage image = backgroudImages[j].GetComponentInChildren<RawImage>();
			image.texture = item.GetTexture();
			backgroudImages[j].GetComponent<RectTransform>().localPosition = initPosition + diffPosinton;
			backgroudImages[j].transform.localScale = initScale;
			backgroudImages[j].SetCount(setGemController.GetMaxCount(j));
			backgroudImages[j].transform.SetAsFirstSibling();
			backgroundImagePosition.Add(initPosition + diffPosinton);
			backgroundImageScale.Add(initScale);

			//左右排放
			if (j % 2 == 0)
			{
				//缩小使得显示在后方
				initScale /= 2;
				t = -t;
				t += d;
				d /= 2;
			}
			if (j % 2 == 1)
			{
				//先左后右
				t = -t;
			}
			diffPosinton.x = t;
			j++;
		}
		currentIndex = 0;
	}

	private void Clear()
	{
		for (int i = 0; i < gemCemares.Count; i++)
		{
			Destroy(gemCemares[i].gameObject);
		}
		gemCemares.Clear();
		for (int i = 0; i < backgroudImages.Count; i++)
		{
			Destroy(backgroudImages[i].gameObject);
		}
		backgroudImages.Clear();
		backgroundImagePosition.Clear();
		backgroundImageScale.Clear();
	}
	private void UpdateIndex(int index)
	{
		int r = currentIndex - index;
		if (r > 1)
		{
			r = -1;
		}
		else if (r < -1)
		{
			r = 1;
		}
		for (int i = 0; i < backgroudImages.Count; i++)
		{
			int t = i + r;
			if(t>= backgroudImages.Count)
			{
				t = 0;
			}
			else if (t < 0)
			{
				t = backgroudImages.Count - 1;
			}
			t -= currentIndex;
			if (t < 0)
			{
				t =  backgroudImages.Count + t;
			}
			backgroudImages[i].transform.DOLocalMove(backgroundImagePosition[t], 0.5f);
			backgroudImages[i].transform.DOScale(backgroundImageScale[t], 0.5f);
			if(i == index)
			{
				backgroudImages[i].transform.SetAsLastSibling();
			}
		}
		currentIndex = index;
	}
	private void SetGem(GemEventHandlerArges arges)
	{
		backgroudImages[arges.index].StartCoolingTime(arges.coolingTime);
		backgroudImages[arges.index].SetCount(arges.count);
	}

	private void UpdeteGem(GemEventHandlerArges arges)
	{
		backgroudImages[arges.index].SetCount(arges.count);
	}
}   
