using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBlockScript : MonoBehaviour {
	//ゴールブロックのタグのオブジェクトにくっつける。
	//boolで止まる、動く。
	//ゴールブロック同士が当たるとクリア判定。
	//動かない方は、クリア判定時のみ当たっている方も止める。

	private bool isThisStatic = false;	//isStaticを格納

	Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (isThisStatic) {
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MarkSetter (bool isStatic,int mark) {
		isThisStatic = isStatic;
		int markNum = mark;				//ブロックが動くブロックなら偶数に
		if (isStatic) markNum++;		//動かないブロックなら奇数に調整
		List<MarkList> markList = MarksSet.Instance.MarkGetter();
		Mesh mesh = markList [markNum].markMesh;
		Sprite spr = markList [markNum].markSprite;
		SetCompornent (mesh,spr);
	}

	void SetCompornent (Mesh mesh,Sprite spr) {
		transform.GetComponentInChildren<SpriteRenderer>().sprite = spr;
		if (isThisStatic) {
			Vector3 sprVec = transform.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.localPosition;
			sprVec = new Vector3 (sprVec.x, sprVec.y, 0.33f);
			transform.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.localPosition = sprVec;
		} else {
			Quaternion sprVec = transform.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.localRotation;
			sprVec = new Quaternion (sprVec.x, -180, sprVec.z, 0);
			transform.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.localRotation = sprVec;
		}
		GetComponent<MeshFilter> ().mesh = mesh;
		GetComponent<MeshCollider> ().sharedMesh = mesh;
	}
}
