using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicEffectManager : MonoBehaviour
{
    public List<AudioClip> audioClip;

    [SerializeField]private AudioSource audioSource;

	private void OnEnable()
	{
		PlayerEventHandler.ChangedGamIndex += PlayerEventHandler_ChangedGamIndex;
	}

	private void PlayerEventHandler_ChangedGamIndex(int obj)
	{
		PlayerAudio("ChangeGem");
	}

	private void OnDisable()
	{
		PlayerEventHandler.ChangedGamIndex -= PlayerEventHandler_ChangedGamIndex;
	}


	public void PlayerAudio(string name)
	{
		audioSource.clip = audioClip.Find((a) => { return a.name.Equals(name); });
		audioSource.Play();
	}

}
