using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextStage()
    {
        GameInfo.GetInstance.Stage++;
        GameInfo.GetInstance.Infinite = false;
        LoadingScene.LoadScene("GameScene");
    }

    public void Retry()
    {
        LoadingScene.LoadScene("GameScene");
    }

    public void ToTitle()
    {
        GameInfo.GetInstance.Infinite = false;
        LoadingScene.LoadScene("TitleScene");
    }
}
