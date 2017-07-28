using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScrollController : MonoBehaviour {

	public SoundManager _soundManager;

	[SerializeField]
	RectTransform prefab = null;

	[SerializeField]
	private int StageRange;

	public ScrollController sclCtrl;

	private List<GameObject> nodes = new List<GameObject>();

	void Start () 
	{
		_soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject contents = GameObject.Find("Contents").gameObject;
		ScrollController sclCtrl = contents.GetComponent<ScrollController>();
		for(int i=1; i<=StageRange; i++)
		{
			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(transform, false);
			item.name = "Node" + i.ToString();
			Button itemButton = item.transform.GetChild(0).GetComponent<Button> ();

//			Debug.Log (item.gameObject);
			nodes.Add(item.gameObject);
			InteractableSet ();

			GameObject n = item.gameObject;
			itemButton.onClick.AddListener(() => sclCtrl.OnStageSelectButton(n));

			var text = item.GetComponentInChildren<Text>();
			if (i <= 2) {
				text.text = "チュートリアル" + i.ToString ();
				//item.name = "tutorial" + i.ToString();
			} else {
				text.text = "ステージ" + (i-2).ToString ();
				//item.name = "Stage" + (i-2).ToString();
			}
		}
	}

	void Update ()
	{
		InteractableSet ();
	}

	public void OnStageSelectButton (GameObject Obj)
	{
		_soundManager.SEType (2);
		string nodeNum = "node";
		if (Obj.name.Length == 5) {
			nodeNum = Obj.name.Substring (4, 1);
		} else if (Obj.name.Length == 6) {
			nodeNum = Obj.name.Substring (4, 2);
		}
		int num = int.Parse (nodeNum);
//		Debug.Log (num);
		GameController.nowStageNum = num;
		GameController._gameState = GameController.GameState.Main;

	}
	void InteractableSet()
	{
		//node数分回す
		for(int i = 0; i < nodes.Count; i++)
		{
			//最初ClearStageNom=0 i=0 
			if (GameController.clearStageNum >= i) {
				nodes [i].transform.GetChild (0).GetComponent<Button> ().interactable = true;
			}
		}
	}

}
