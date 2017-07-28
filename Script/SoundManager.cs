using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour{

	AudioSource _SEAudioSource;
	public GameObject _AudioBGM;
	AudioSource _BGMAudioSource;

	static bool isFirstInstance = false;

	public AudioClip[] SE;
	//0 awasaru
	//1 Clear
	//2 kettei
	//3 modoru
	//4 tometeru

	public AudioClip titleBGM;

	public AudioClip[] mainBGM;

	// Use this for initialization
	void Start () {
		_SEAudioSource = this.GetComponent<AudioSource> ();
		if (!isFirstInstance) {
			GameObject BGMObj = Instantiate (_AudioBGM);
			BGMObj.name = "AudioBGM";
			_BGMAudioSource = BGMObj.GetComponent<AudioSource> ();
			isFirstInstance = true;
		} else {
			_BGMAudioSource = GameObject.Find ("AudioBGM").GetComponent<AudioSource> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SEType(int se_num)
	{
		_SEAudioSource.clip = SE [se_num];
		_SEAudioSource.Play();
	}

	public void ChangeBGM(){
		int randimBGM = Random.Range (0, 2);
		_BGMAudioSource.clip = mainBGM [randimBGM];
		_BGMAudioSource.Play ();
	}

}
