using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLabel : MonoBehaviour {

    public UILabel m_stageLabel;
    public GameObject m_stage;
    public GameObject m_ready;
    public GameObject m_go;
    void Start () {
        //int stage = GameInfo.GetInstance.Stage;
        if(GameInfo.GetInstance.Infinite)
        {
            m_stageLabel.text = "Infinite Stage";
        }
        else
        {
            m_stageLabel.text = string.Format("{0} Stage", GameInfo.GetInstance.Stage + 1);
        }
        
        m_ready.SetActive(false);
        m_go.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Ready()
    {
        m_stage.SetActive(false);
        m_ready.SetActive(true);
    }

    public void Go()
    {
        m_ready.SetActive(false);
        m_go.SetActive(true);
    }

    public void SetActiveFalse()
    {
        m_stage.SetActive(false);
        m_ready.SetActive(false);
        m_go.SetActive(false);
    }
    
}
