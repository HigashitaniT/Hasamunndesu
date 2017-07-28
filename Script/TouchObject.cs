using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour {
	//タッチできるオブジェクトにつける

	public Rigidbody _rigidBody;

	// Use this for initialization
	void Start () {
		this.gameObject.tag = "TouchObject";
		//_rigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MyTouch () 
	{
		_rigidBody.constraints = RigidbodyConstraints.FreezeAll;
	}
	public void MyTouchEnd ()
	{
		_rigidBody.constraints = RigidbodyConstraints.None;
		_rigidBody.constraints = RigidbodyConstraints.FreezePositionZ |
		RigidbodyConstraints.FreezeRotationX |
		RigidbodyConstraints.FreezeRotationY;
	}
}
