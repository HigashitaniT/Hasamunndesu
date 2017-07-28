using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {

	public GameObject[] markPrefabs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Transform MarkInstance (int markNum,Transform trans)
	{
		Vector3 pos;
		if (markNum % 2 == 0) {
			pos = new Vector3 (0.3f, 0, 0);
		} else {
			pos = new Vector3 (-0.3f, 0, 0);
		}
		Transform instanceObjTransform = Instantiate (markPrefabs [markNum], trans.position - pos, markPrefabs[markNum].transform.localRotation).transform;
		return instanceObjTransform;
	}
}
