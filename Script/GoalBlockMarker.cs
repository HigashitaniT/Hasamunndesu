using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBlockMarker : MonoBehaviour {
	//これを置くとゴールブロックをインスタンス
	//ここで設定するのは、動くか動かないか、マーク
	//動くか動かないか、マークのスプライト、マークのコライダーをゴールブロックに渡す

	public GameObject goalBlock;	//スポーンするgoalBlock

	public bool isStatic = false;	//false=動く　true=動かない

	public enum Mark : int			//マークを選択できる 偶数は動くブロック　奇数は動かないブロック
	{
		aoi			=0,
		batu		=2,
		heart		=4,
		maru		=6,
		sakura		=8,
		sankaku		=10,
		star		=12,
		triforce	=14,
		yotuba		=16,
	}
	public Mark mark;

	void Awake () {
		GameObject instanceGoalBlock;
		if (isStatic) {
			instanceGoalBlock = Instantiate (goalBlock, transform.position, Quaternion.identity);
		} else {
			instanceGoalBlock = Instantiate (goalBlock, transform.position, new Quaternion (0, -180, 0, 0));
		}
		instanceGoalBlock.GetComponent<GoalBlockScript> ().MarkSetter (isStatic, (int)mark);
		this.gameObject.SetActive (false);
	}


	void Update () {
		
	}

	void OnDrawGizmos () //ステージを作りやすいようにギズモ表示
	{
		Gizmos.color = new Color (0, 1, 0, 0.5f);
		Gizmos.DrawSphere (transform.position, 1f);

		int sum = (int)mark;

		if (isStatic) sum++;

		Gizmos.DrawIcon (
			transform.position, 
			MarksSet.Instance.MarkGetter () [sum].markSprite.name,
			true
		);
	}
}
