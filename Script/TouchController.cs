using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	Ray ray;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OnTouchDown())
		{
//			Debug.Log ("touch");
		}
	}

	bool OnTouchDown()
	{
		// タッチされているとき
		if (0 < Input.touchCount)
		{
			// タッチされている指の数だけ処理
			for (int i = 0; i < 1; i++)
			{
				// タッチ情報をコピー
				Touch t = Input.GetTouch(i);
				// タッチしたときかどうか
				if (t.phase == TouchPhase.Began)
				{
					ray = Camera.main.ScreenPointToRay(t.position);
					return RayChack();
				}
				if (t.phase == TouchPhase.Ended) {
					ray = Camera.main.ScreenPointToRay (t.position);
					return EndRay ();
				}
			}
		}
		if (Input.GetMouseButtonDown(0))//クリックしたとき
		{
//			Debug.Log ("kurikku");
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			return RayChack();
		}
		if (Input.GetMouseButtonUp (0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			return EndRay ();
		}
		return false; //タッチされていないときfalse
	}
	bool RayChack()//レイを飛ばす
	{
		//タッチした位置からRayを飛ばす

		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit))
		{
			//Rayを飛ばしてあたったオブジェクトのタグがGoalBlockだったら
			if (hit.collider.gameObject == (hit.collider.gameObject.tag == "GoalBlock"))
			{
				hit.collider.gameObject.GetComponent<GoalBlockManager> ().MyTouch ();
				return true;
			}
			if (hit.collider.gameObject.tag == "TouchObject") 
			{
				hit.collider.gameObject.GetComponent<TouchObject> ().MyTouch ();
				return true;
			}
		}
		return false;//タッチされてなかったらfalse
	}
	bool EndRay()
	{
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit))
		{
			//Rayを飛ばしてあたったオブジェクトのタグがGoalBlockだったら
			if (hit.collider.gameObject == (hit.collider.gameObject.tag == "GoalBlock"))
			{
				hit.collider.gameObject.GetComponent<GoalBlockManager> ().MyTouchEnd ();
				return true;
			}
			if (hit.collider.gameObject.tag == "TouchObject") 
			{
				hit.collider.gameObject.GetComponent<TouchObject> ().MyTouchEnd ();
				return true;
			}
		}
		return false;//タッチされてなかったらfalse
	}


}
