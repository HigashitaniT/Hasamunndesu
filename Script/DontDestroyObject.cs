using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour {

	static bool isDontDestroyEnabled = false;

	// Use this for initialization
	void Awake () {
		this.name = "AudioBGM";
		if (!isDontDestroyEnabled) {
			DontDestroyOnLoad (this);
			isDontDestroyEnabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
