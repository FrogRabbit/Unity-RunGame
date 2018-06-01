using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour {
    public GameObject m_lockSpr;
    public UILabel m_label;
    private bool m_bLock;
    private int m_iIndex;
	public void Init(int stage, bool bLock)
    {
        m_iIndex = stage;
        m_bLock = !bLock;
        m_lockSpr.SetActive(m_bLock);
        
        if (!m_bLock)
        {
            m_label.text = string.Format("{0}", stage + 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select()
    {
        if (!m_bLock)
        {
            GameInfo.GetInstance.Infinite = false;
            Debug.Log(string.Format("select : {0}Stage", m_iIndex + 1));
            GameInfo.GetInstance.Stage = m_iIndex;
            LoadingScene.LoadScene("GameScene");
        }
        else
        {
            Debug.Log(string.Format("Lock : {0}Stage", m_iIndex + 1));
        }
    }
}
