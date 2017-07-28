using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class GoalBlockManager : MonoBehaviour {

    GameObject result;

    Ray ray;

    Rigidbody rb;

    GoalBlockManager _GoalScript;
    GameController _gameCtrl;
	SoundManager _soundManager;
	Prefabs _prefabs;

	Rigidbody _onRigid;
    
	[HideInInspector]
    public int isInCount = 0;
	[HideInInspector]
	public float timer;

    public Sprite[] markSprite;

	[HideInInspector]
	public bool isGoalEnter; //とりあえずGoalBlockに当たったフラグ
	[HideInInspector]
	public bool enterTouch; //isStaticBlockがisStaticBlock以外と当たった時にタッチを受け付けるフラグ
	[HideInInspector]
	public bool isMyTouch;

	GameObject markSpriteObj;

    public enum BlockMark 
    {
        Star = 0,
        Heart = 2,
        Batu = 4,
		Aoi = 6,
		Circle = 8,
		Sakura = 10,
		Triangle = 12,
		Force = 14,
		Clover = 16
    }
    public BlockMark _blockMark;

    public bool isStaticBlock = false;//静的なブロックにする

	// Use this for initialization
	void Start () {
        _gameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
		_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager> ();
		_prefabs = GameObject.Find ("Prefabs").GetComponent<Prefabs> ();

		int spriteNum = (int)_blockMark;
		if (isStaticBlock) spriteNum++;//staticBlockは1,3,4　動く方は0,2,4

		Transform markObj = _prefabs.MarkInstance (spriteNum, this.transform);
		this.transform.parent = markObj;
		markObj.parent = _gameCtrl.nowStage.transform;


		rb = transform.parent.GetComponent<Rigidbody>();
//		Debug.Log (rb);

		markSpriteObj = markObj.FindChild ("MarkSprite").gameObject;
		/*
        SpriteRenderer _childSp = transform.FindChild("MarkSprite").
                                gameObject.GetComponent<SpriteRenderer>();

        int spriteNum = (int)_blockMark;
        if (isStaticBlock) spriteNum++;//staticBlockは1,3,4　動く方は0,2,4

        _childSp.sprite = markSprite[spriteNum];
        */
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isStaticBlock)
        {
            if (rb != null) rb.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
		/*
        if (OnTouchDown())
        {
            if (rb.constraints != RigidbodyConstraints.FreezeAll)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
		times += Time.deltaTime;
		if (times >= 0.5f && isMyTouch == true) {
			isMyTouch = false;
			times = 0;
		}*/

		if (isMyTouch == false)
        {
            if (rb.constraints == RigidbodyConstraints.FreezeAll)
            {
                //rb.constraints = RigidbodyConstraints.FreezePositionZ |
                //                 RigidbodyConstraints.FreezeRotationX |
                //                 RigidbodyConstraints.FreezeRotationY;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY;

            }
        }
		if (!isGoalEnter) {
			iTween.MoveUpdate (markSpriteObj, iTween.Hash ("position", this.transform.position));
			iTween.RotateUpdate (markSpriteObj, iTween.Hash ("rotation", this.transform.eulerAngles));
		}
	}

    private void OnTriggerEnter(Collider col)
    {
		//関係ないブロックは排除
		if (col.gameObject.tag != "GoalBlock") return;

		//自分が動くブロック　&&　当たったブロックが停止したブロックなら
		if (!isStaticBlock && col.GetComponent<GoalBlockManager>().isStaticBlock) {
			//取得
			_GoalScript = col.gameObject.GetComponent<GoalBlockManager>();
			//取得したGoalBlockManagerを参照して、当たった相手が止まっているブロックなら
			if (_GoalScript.isStaticBlock) {
				isGoalEnter = true;
			}
			return;
		}
		if (!isStaticBlock) return;
		//自分が動かないブロックなら以下を実行

		//マークが同じか確認し同じなら処理へ
		if (col.GetComponent<GoalBlockManager> ()._blockMark == this._blockMark && !col.GetComponent<GoalBlockManager>().isStaticBlock)
		{
			_GoalScript = col.gameObject.GetComponent<GoalBlockManager> ();

			isInCount++;
			Debug.Log (isInCount);

			//当たってるブロックが1つの時
			if (isInCount == 1) {
				_onRigid = col.transform.parent.GetComponent<Rigidbody> ();

				_soundManager.SEType (0);

//			Debug.Log (col.transform.parent);

				GameObject markChild = col.transform.parent.FindChild ("MarkSprite").gameObject;
				iTween.MoveTo (markChild.gameObject, iTween.Hash ("position", this.transform.position));
				iTween.RotateTo (markChild.gameObject, iTween.Hash ("rotation", this.transform.eulerAngles));

				this.transform.FindChild ("ClearSphere").gameObject.SetActive (true);
				_gameCtrl._ClearCount++;
			}
		}
    }
	private void OnTriggerExit(Collider col)
    {
		//関係のないもの排除
		if (col.gameObject.tag != "GoalBlock")return;

		//自分が動くブロックで、かつ、動かないブロックと離れたら
		if (!isStaticBlock) {
//			_GoalScript = col.gameObject.GetComponent<GoalBlockManager>();
			if (col.GetComponent<GoalBlockManager>().isStaticBlock) {
				isGoalEnter = false;
			}
		}
		Debug.Log (isInCount);
        if (!isStaticBlock) return;

		if (col.GetComponent<GoalBlockManager> ()._blockMark == this._blockMark)
		{
			if (isInCount == 1) {
				this.transform.FindChild ("ClearSphere").gameObject.SetActive (false);
				_gameCtrl._ClearCount--;
			}
			isInCount--;
		}
    }

	public void MyTouch()
	{
		isMyTouch = true;
		_soundManager.SEType (4);
		if (rb.constraints != RigidbodyConstraints.FreezeAll && !isStaticBlock)//動くブロックが止められていなければ
		{
			rb.constraints = RigidbodyConstraints.FreezeAll;
//			Debug.Log ("ugokuzo");
		}
		else if (isStaticBlock && _GoalScript != null && _onRigid != null) //動かないブロックが
		{
			_onRigid.constraints = RigidbodyConstraints.FreezeAll;
			_GoalScript.isMyTouch = true;
//			Debug.Log ("stop");
		}
	}
	public void MyTouchEnd()
	{
		isMyTouch = false;
		if (isStaticBlock && _onRigid != null) 
		{
			_GoalScript.isMyTouch = false;
			_onRigid.constraints = RigidbodyConstraints.None;
			_onRigid.constraints = RigidbodyConstraints.FreezePositionZ |
				RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationY;
			Debug.Log ("start");
		}
	}
}
