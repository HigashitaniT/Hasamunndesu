using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public SoundManager _soundManager;

	public static int clearStageNum = 20;
    public static int nowStageNum = 0;

    public static bool isReload;
    private bool isStart = false;
	private static float clearTime;
    private int gBlockCount = 0;
	[SerializeField]
    private int clearCount = 0;
	[HideInInspector]
	public GameObject nowStage;
    private Text clearText;

	public GameObject hintButton;
    public GameObject titleUI;
	public GameObject stageSelectUI;
    public GameObject clearUI;
	public GameObject endUI;
	public GameObject timeTextUI;

    public int _ClearCount
    {
        get { return clearCount; }
        set { clearCount = value; }
    }

    [SerializeField]
    private GameObject[] stages;

	private static bool[] isStageClear;

    public enum GameState
    {
        Title,
		StageSelect,
        Main,
        Clear,
        End
    }
	public static GameState _gameState;

	static bool isfirstflag;

	// Use this for initialization
	void Start () {
		//_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager> ();

		if (!isfirstflag) {
			titleUI.SetActive (true);
			isStageClear = new bool[stages.Length];
			for (int i = 0; isStageClear.Length > i; i++) {
				isStageClear [i] = true;
			}
			isfirstflag = true;
		}
		//clearText = clearUI.transform.GetChild(1).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        
		switch(_gameState)
        {
            case GameState.Title:
                stateTitle();
                break;
			case GameState.StageSelect:
				stateStageSelect ();
				break;
            case GameState.Main:
                stateMain();
                break;
            case GameState.Clear:
                stateClear();
                break;
            case GameState.End:
                stateEnd();
                break;
        }
	}

    void stateTitle()
    {
		if (!titleUI.activeSelf) titleUI.SetActive (true);
		if (timeTextUI.activeSelf) timeTextUI.SetActive (false);

        if (0 < Input.touchCount)
        {
			// タッチされている指の数だけ処理
			for (int i = 0; i < Input.touchCount; i++) {
				// タッチ情報をコピー
				Touch t = Input.GetTouch (i);
				// タッチしたときかどうか
				if (t.phase == TouchPhase.Began) {
					nowStageNum = 0;
					isStart = false;
					_gameState = GameState.StageSelect;
				}
			}
        }
		if (Input.GetMouseButtonDown (0)) 
		{
			nowStageNum = 0;
			isStart = false;
			_gameState = GameState.StageSelect;
		}
    }
	void stateStageSelect()
	{
		nowStageNum = 0;
		if (clearUI.activeSelf) clearUI.SetActive (false);
		if (timeTextUI.activeSelf) timeTextUI.SetActive (false);
		titleUI.SetActive(false);
		stageSelectUI.SetActive (true);
		clearTime = 0;
	}
    void stateMain()
    {
		if (clearUI.activeSelf) clearUI.SetActive (false);
		if (titleUI.activeSelf) titleUI.SetActive (false);
		if (timeTextUI.activeSelf) timeTextUI.SetActive (false);
		if (hintButton.activeSelf) hintButton.SetActive (false);
		if (nowStageNum == 2) {
			hintButton.SetActive (true);
		}
		//リロード時以外でタイムをリセット
		if (!isReload && !isStart) {
			_soundManager.ChangeBGM ();
		} 
		if (isReload) {
			isReload = false;
		} 

        if (!isStart)
        {
			//_soundManager.ChangeBGM ();
			stageSelectUI.SetActive (false);
   			isStart = true;
			clearCount = 0;
            nowStage = Instantiate(stages[nowStageNum-1]);
            gBlockCount = GameObject.FindGameObjectsWithTag("GoalBlock").Length;
        }
//		Debug.Log (gBlockCount / 2);
//		Debug.Log (clearCount);

        if((gBlockCount/2) == clearCount)
        {
                _gameState = GameState.Clear;
        }
		clearTime += Time.deltaTime;
    }
    void stateClear()
    {
		if(isStart)
        {
			//そのステージをクリアしたかどうか
			//今のステージをクリアしていなければ
			if (!isStageClear [nowStageNum-1]) {
				isStageClear [nowStageNum-1] = true;
			}
			//isStageClear配列の中のtrueの数をカウントして、clearStageNumに入れる
//			int sum = 0;
//			for (int i = 0; isStageClear.Length > i; i++) {
//				if (isStageClear [i] == true) {
//					sum++;
//				}
//			}
			//clearStageNum = sum;

			timeTextUI.SetActive (true);
			timeTextUI.GetComponent<Text> ().text = "タイム : " + clearTime.ToString ("F1");
			clearTime = 0;
            isStart = false;
            gBlockCount = 0;
            clearCount = 0;
			Destroy(nowStage,0.5f);
			_soundManager.SEType (1);

//			Debug.Log (nowStageNum);
//			Debug.Log (clearStageNum);

			if (stages.Length == clearStageNum && nowStageNum == clearStageNum) {
				_gameState = GameState.End;
				//clearStageNum--;
			} else {
				clearUI.SetActive(true);
				//clearText.text = "STAGE " + nowStageNum;
			}
        }
    }

    void stateEnd()
    {
		if (clearUI != null) {
			clearUI.SetActive (false);
			endUI.SetActive (true);
		}
		if (0 < Input.touchCount) {
			// タッチされている指の数だけ処理
			for (int i = 0; i < Input.touchCount; i++) {
				// タッチ情報をコピー
				Touch t = Input.GetTouch (i);
				// タッチしたときかどうか
				if (t.phase == TouchPhase.Began) {

					endUI.SetActive (false);
					titleUI.SetActive (true);
					_gameState = GameState.Title;
				}
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			endUI.SetActive(false);
			titleUI.SetActive (true);
			_gameState = GameState.Title;
		}
        Debug.Log("End");
    }

    public void StageInstance()
    {
        _gameState = GameState.Main;
    }
    //public void InstanceStage()
    //{
    //    if(nowStage != null)
    //    {
    //        Destroy(nowStage);
    //        nowStage = Instantiate(stages[nowStageNum]);
    //    }
    //}
	/*
	void DebagTest ()
	{
		for(int i = 0;


	}*/
}
