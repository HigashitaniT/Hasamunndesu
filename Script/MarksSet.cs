using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MarkList
{
	public Sprite markSprite;
	public Mesh markMesh;
}

public class MarksSet : MonoBehaviour {
	//マークスプライトとマークメッシュを保管して置く場所
	public static MarksSet Instance { get; private set; }

	public List<MarkList> _markList = new List<MarkList>();
	/*
	Sprite[] markSprite;
	Mesh[] markMesh;
	*/
	//public void 


	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this);
		} else {
			enabled = false;
			DestroyImmediate(this.gameObject);
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<MarkList> MarkGetter()
	{
		return _markList;
	}

}
