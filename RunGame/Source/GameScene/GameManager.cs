using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState m_state = GameState.Intro;
    private float m_fTime = 0;
    public Transform m_playerTransform;
    public EffectManager m_effectManager;
    public BlockManager m_blockManager;
    public GameObject m_runEffect;
    private float m_fEffectTime = 0;
	void Start ()
    {
        GameInfo.GetInstance.Init();
    }


    void Update()
    {
        switch (m_state)
        {
            case GameState.Intro:
                m_fTime += Time.deltaTime;
                if(m_fTime >= GameInfo.GetInstance.IntroTime)
                {
                    m_fTime = 0;
                    m_state = GameState.Play;
                    m_runEffect.SetActive(true);
                }
                break;
            case GameState.Play:
               
                break;
            case GameState.Drop:
                m_fTime += Time.deltaTime;
                if (m_fTime >= 3.4f)
                {
                    m_fTime = 0;
                    m_state = GameState.Play;
                    m_runEffect.SetActive(true);
                }
                break;
            case GameState.Crash:
                m_fTime += Time.deltaTime;
                if(m_fTime >= 3.4f)
                {
                    m_fTime = 0;
                    m_state = GameState.Play;
                    m_runEffect.SetActive(true);
                }
                break;
            case GameState.Finish:
                m_fTime += Time.deltaTime;
                m_fEffectTime += Time.deltaTime;
                if(m_fEffectTime >= 0.5f)
                {
                    m_fEffectTime -= 0.5f;
                    float radian = Random.Range(0, 360) * 180 / 3.14f;
                    m_effectManager.MakeFireWork(m_playerTransform.position + new Vector3(Mathf.Sin(radian) * 2, 0, Mathf.Cos(radian) * 2));
                }
                if (m_fTime >= 7.5f)
                {
                    m_fTime = 0;
                    GameInfo.GetInstance.Finish = true;
                    GameInfo.GetInstance.UnlockStage();
                    //GameInfo.GetInstance.CheckScore();
                  
                    LoadingScene.LoadScene("ResultScene");
                }
                break;
            case GameState.GameOver:
                m_fTime += Time.deltaTime;
                m_fEffectTime += Time.deltaTime;
                if (m_fEffectTime >= 0.5f)
                {
                    m_fEffectTime -= 0.5f;
                    float radian = Random.Range(0, 360) * 180 / 3.14f;
                    m_effectManager.MakeCrashEffect(m_playerTransform.position + new Vector3(Mathf.Sin(radian) * 2, 0, Mathf.Cos(radian) * 2));
                }
                if (m_fTime >= 8.0f)
                {
                    m_fTime = 0;
                    LoadingScene.LoadScene("ResultScene");
                }
                break;
            default:
                break;
        }
    }

    public int State
    {
        set { m_state = (GameState)value; }
        get { return (int)m_state; }
    }

    public float ElapsedTime
    {
        get { return m_fTime; }
    }

}
