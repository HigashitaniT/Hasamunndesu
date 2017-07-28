using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvasController : MonoBehaviour {

    public GameObject popUpWin;
    public GameController _gameCtrl;
	public SoundManager _soundManager;
	public GameObject HintUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitButton()
    {
        Debug.Log("Exit");
        popUpWin.SetActive(true);
		_soundManager.SEType (3);
    }

    public void ResetButton()
    {
		_soundManager.SEType (2);
        Debug.Log("Reset");
		if(GameController._gameState == GameController.GameState.Main)
        {
            GameController.isReload = true;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    public void PopUpYes()
    {
		_soundManager.SEType (2);
   
        GameController.nowStageNum = 0;
		GameController.isReload = false;
		if (GameController._gameState == GameController.GameState.StageSelect) {
			GameController._gameState = GameController.GameState.Title;
		} else{
			GameController._gameState = GameController.GameState.StageSelect;
		}
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void PopUpNo()
    {
		_soundManager.SEType (3);
        Debug.Log("No");
        popUpWin.SetActive(false);
    }

	public void NextStageButton(){
		_soundManager.SEType (2);
		GameController._gameState = GameController.GameState.Main;
		GameController.nowStageNum++;
	}
	public void ReturnButton(){
		_soundManager.SEType (2);
		GameController._gameState = GameController.GameState.StageSelect;
	}
	public void HintButton (){
		_soundManager.SEType (2);
		if (HintUI.activeSelf) {
			HintUI.SetActive (false);
		} else {
			HintUI.SetActive (true);
		}
	}
}
