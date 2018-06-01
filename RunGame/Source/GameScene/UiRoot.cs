using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRoot : MonoBehaviour
{

    public Player m_player;
    public GameObject m_navigation;
    public UISprite m_navigationSpr;
    public GameObject m_cursor;
    public GameManager m_gameManager;
    public UILabel m_overLabel;
    public UILabel m_finishLabel;
    private Transform m_playerTransform;
    private int m_iStage;
    public MapManager m_mapManager;
    private float m_naviWidth;
    public IntroLabel m_introLabel;
    private int m_iLife = 3;
    public GameObject[] m_life;
    public UILabel m_score;
    private bool m_bInfinite;
    public int m_iScore = 0;
	void Start () {
        m_overLabel.alpha = 0;
        m_finishLabel.alpha = 0;
        m_iScore = 0;
        m_playerTransform = m_player.GetComponent<Transform>();
        m_bInfinite = GameInfo.GetInstance.Infinite;
        if(m_bInfinite)
        {
            m_navigation.SetActive(false);
            m_cursor.SetActive(false);
        }
        m_naviWidth = m_navigationSpr.width - 10;
        m_cursor.transform.localPosition = m_cursor.transform.localPosition + new Vector3(0 - (m_naviWidth / 2), 0, 0);
        m_iStage = GameInfo.GetInstance.Stage;
    }
	
	// Update is called once per frame
	void Update () {
        
        switch (m_gameManager.State)
        {
            case (int)GameState.Intro:
                {
                    float time = m_gameManager.ElapsedTime;
                    if (time >= 4.0f && time < 6.0f)
                    {
                        m_introLabel.Ready();
                    }
                    else if (time >= 6.0f && time < 7.5f)
                    {
                        m_introLabel.Go();
                    }
                    else if (time >= 7.5f)
                    {
                        m_introLabel.SetActiveFalse();
                    }
                }
                break;
            case (int)GameState.Play:
                if (!m_bInfinite)
                {
                    float ratio = m_playerTransform.position.z / (m_mapManager.Length);
                    if (ratio <= 0.98f)
                    {
                        m_cursor.transform.localPosition = new Vector3((0 - m_naviWidth / 2) + (ratio * m_naviWidth), m_cursor.transform.localPosition.y, 0);
                    }
                    if(ratio >= 0.98f)
                    {
                        m_gameManager.State = (int)GameState.Finish;
                        m_player.Finish();
                        GameInfo.GetInstance.Score = m_iScore;
                        GameInfo.GetInstance.CheckScore();
                    }
                }
                break;
            case (int)GameState.GameOver:
                {
                    // 8.0
                    float time = m_gameManager.ElapsedTime;
                    Vector2 start = Vector2.zero;
                    Vector2 des = new Vector2(255, 0);
                    m_overLabel.alpha = Vector2.Lerp(start, des, time / 8.0f).x;
                }
                break;
            case (int)GameState.Finish:
                {
                    m_finishLabel.alpha = 255;
                }
                break;
        }
        
       

    }

    public void LeftButton()
    {
        m_player.MoveLeft();
        Debug.Log("Left");
    }

    public void RightButton()
    {
        m_player.MoveRight();
        Debug.Log("Right");
    }

    public void DecLife()
    {
        m_iLife--;
        if (m_iLife < 0)
        {
            m_gameManager.State = (int)GameState.GameOver;
            m_player.GameOver();
            GameInfo.GetInstance.Finish = false;
        }
        for (int i = 0; i < 3; i++)
        {
            m_life[i].SetActive(false);
        }
        for(int i = 0; i < m_iLife; i++)
        {
            m_life[i].SetActive(true);
        }
        
    }

    public void IncScore(int score)
    {
        m_iScore += score;
        m_score.text = string.Format("{0}", m_iScore);
    }
}
