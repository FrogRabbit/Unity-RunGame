using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject m_stageButton;
    public GameObject m_infiniteButton;
    public GameObject m_stageSelect;
	// Use this for initialization
	void Start () {
        /* m_stageButton.SetActive(true);
         m_infiniteButton.SetActive(true);
         m_stageSelect.SetActive(false);*/
        GameInfo.GetInstance.Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StageMode()
    {
        m_stageButton.SetActive(false);
        m_infiniteButton.SetActive(false);
        m_stageSelect.SetActive(true);
        Debug.Log("stage");
        /*GameInfo.GetInstance.Infinite = false;
        Debug.Log("infinite");
        SceneManager.LoadScene(1);*/
    }

    public void ModeSelect()
    {
        GameInfo.GetInstance.Infinite = false;
        m_stageButton.SetActive(true);
        m_infiniteButton.SetActive(true);
        m_stageSelect.SetActive(false);
        Debug.Log("mode");
    }

    public void InfiniteMode()
    {
        GameInfo.GetInstance.Infinite = true;
        Debug.Log("infinite");
        LoadingScene.LoadScene("GameScene");
    }

    public void SelectStage()
    {
        GameInfo.GetInstance.Infinite = false;
    }
}
