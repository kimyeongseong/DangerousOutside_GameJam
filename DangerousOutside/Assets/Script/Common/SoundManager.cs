﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンド管理クラス
/// </summary>
public class SoundManager : MonoBehaviour {

	private static SoundManager instance;
	public static SoundManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<SoundManager>();

				if (instance == null)
				{
					instance = new GameObject("SoundManager").AddComponent<SoundManager>();
					GameObject.DontDestroyOnLoad(instance.gameObject);
				}
			}
			return instance;
		}
	}

	private AudioSource bgmAudio;
	private List<AudioSource> seAudioList = new List<AudioSource>();

	public float bgmValue = 0;
	public float seValue = 0;

	void Awake()
	{
		Init();
	}

	public void Start()
	{
		//PlayBGM("Main");
	}

	/// <summary>
	/// 初期化
	/// </summary>
	private void Init()
	{
		bgmValue = PlayerPrefs.GetFloat("bgmValue", 0.5f);
		BgmVolumeReset();

		seValue = PlayerPrefs.GetFloat("seValue", 1);
		SeVolumeReset();
	}

	/// <summary>
	/// BGMの出力値修正
	/// </summary>
	/// <param name="value"></param>
	public void BgmValueReset(float value)
	{
		bgmValue = value;
		PlayerPrefs.SetFloat("bgmValue", bgmValue);
		BgmVolumeReset();
	}

	/// <summary>
	/// bgmAudioのボリューム値の修正
	/// </summary>
	void BgmVolumeReset()
    {
		if (bgmAudio == null)
			bgmAudio = gameObject.AddComponent<AudioSource>();

		bgmAudio.volume = bgmValue;
	}

	/// <summary>
	/// SEの出力値の修正
	/// </summary>
	/// <param name="value"></param>
	public void SeValueReset(float value)
	{
		seValue = value;
		PlayerPrefs.SetFloat("seValue", seValue);
		SeVolumeReset();
	}

	/// <summary>
	/// seAudioのボリューム値修正
	/// </summary>
	void SeVolumeReset()
    {
		foreach (var seAudio in seAudioList)
		{
			seAudio.volume = seValue;
		}
	}

	/// <summary>
	/// BGM実行
	/// </summary>
	/// <param name="path"></param>
	public void PlayBGM(BGMEnum bgmEnum)
	{
		bgmAudio.clip = Resources.Load<AudioClip>("Sounds/Bgm/" + bgmEnum.ToString());
		bgmAudio.loop = true;
		bgmAudio.Play();
	}

	public void StopBGM()
	{
		bgmAudio.Stop();
	}

	/// <summary>
	/// SE実行
	/// </summary>
	/// <param name="path"></param>
	public void PlaySe(SeEnum seEnum)
	{
		if (bgmAudio == null)
			bgmAudio = gameObject.AddComponent<AudioSource>();

		AudioSource audioSource = null;

		foreach (var seAudio in seAudioList)
		{
			if (!seAudio.isPlaying)
			{
				audioSource = seAudio;
				break;
			}
		}

		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			seAudioList.Add(audioSource);
			SeVolumeReset();
		}

		audioSource.clip = Resources.Load<AudioClip>("Sounds/Se/" + seEnum.ToString());
		audioSource.Play();
	}
}