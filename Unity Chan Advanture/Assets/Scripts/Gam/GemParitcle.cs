using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GemParitcle : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private bool isCreated;
    public float flyTime = 1f;
    private Vector3 placement;
    private Vector3 initPosition;
    private Vector3 midPosition;
    private float time;
    private Player player;
    private Transform targetTransform;
    private int gemIndex;
    private bool isStop;
    private void Awake()
	{
        isCreated = false;
        placement = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        initPosition = transform.position;
        midPosition = (placement + initPosition) / 2;
        midPosition.y += 5;
        isStop = false;
    }

	private void OnEnable()
	{
		GameEventHandler.GameStop += Stop;
        GameEventHandler.GameContinue += Continue;

        if (LevelManager.Instance != null)
		{
            LevelManager.Instance.BeginLoadingLevel += ParitcleDestory;
        }
	}

	private void Stop()
	{
        isStop = false;
	}
    private void Continue()
	{
        isStop = true;
    }


    private void OnDisable()
	{
        GameEventHandler.GameStop -= Stop;
        GameEventHandler.GameContinue -= Continue;
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.BeginLoadingLevel -= ParitcleDestory;
        }
    }

	private void Update()
	{
        if (!isStop)
        {
            if (!isCreated)
            {
                time += Time.deltaTime / flyTime;
                transform.position = BezierCurve(initPosition, placement, midPosition, time);
                if (time >= 1)
                {
                    time = 0;
                    isCreated = true;
                }
            }
            else if (player != null)
            {
                Vector3 target = targetTransform.position;
                target.y += this.player.halfHight;
                time += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(placement, target, time);
            }
        }
	}
    public void SetPlayer(Player player,int gemIndex)
	{
        targetTransform = player.transform;
        this.player = player;
        this.gemIndex = gemIndex;
    }


    private void ParitcleDestory()
	{
        Destroy(gameObject);
    }


    private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
            if (player != null)
                player.RecoverdGem(gemIndex, 1);
            Destroy(gameObject);
		}
	}
	private Vector3 BezierCurve(Vector3 left,Vector3 right,Vector3 mid,float t)
	{
        t = t > 1 ? 1 : t;
        Vector3 AC = (left + (mid - left) * t);
        Vector3 CB = (mid + (right - mid) * t);
        return AC + (CB - AC) * t;
    }
}
