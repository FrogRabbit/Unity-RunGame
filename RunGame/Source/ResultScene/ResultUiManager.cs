using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUiManager : MonoBehaviour {
    public GameObject m_infiniteObject;
    public GameObject m_clearObject;
    public GameObject m_gameoverObject;
    public UILabel m_curScoreLabel;
    public UILabel m_maxScoreLabel;
    public UILabel m_topLabel;
	void Start () {
        m_infiniteObject.SetActive(false);
        m_clearObject.SetActive(false);
        m_gameoverObject.SetActive(false);
        m_curScoreLabel.text = string.Format("{0}", GameInfo.GetInstance.Score);
       
        if (GameInfo.GetInstance.Infinite)
        {
            m_infiniteObject.SetActive(true);
            m_maxScoreLabel.text = string.Format("{0}", GameInfo.GetInstance.InfiniteScore());
            m_topLabel.text = "Result";
        }
        else
        {
            m_maxScoreLabel.text = string.Format("{0}", GameInfo.GetInstance.GetScore());
            if (GameInfo.GetInstance.Finish)
            {
                
                m_clearObject.SetActive(true);
                m_topLabel.text = string.Format("{0}Stage Clear!", GameInfo.GetInstance.Stage + 1);
            }
            else
            {
                m_gameoverObject.SetActive(true);
                m_topLabel.text = "GameOver";
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
