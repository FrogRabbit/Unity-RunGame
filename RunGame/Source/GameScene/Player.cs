using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject m_runEffect;
    public GameObject m_player;
    public Animator m_animator;
    public float m_runSpeed;
    public float m_fMoveTime;
    public float m_fJumpTime;
    public float m_fSlideTime;
    public GameManager m_gameManager;
    public UiRoot m_uiManager;
    private bool m_bMove = false;
    private float m_fMoveElapsedTime = 0;
    private bool m_bJump = false;
    private bool m_bSlide = false;
    private int m_iPosX = 0;
    private int m_fDesPosX;
    private float m_fJumpElapsedTime = 0;
    private float m_fSlideElapsedTime = 0;
    public BoxCollider m_hitbox;
    public EffectManager m_effectManager;
    public BlockManager m_blockManager;
    private float m_curPosZ = 0;
    private float m_oldPosZ = 0;
    private float m_distance = 0;

    void Start()
    {
        m_animator.SetBool("isRun", true);
        m_animator.SetTrigger("triIntro");
        m_runEffect.SetActive(false);
    }

    public void MoveLeft()
    {

        if (m_iPosX > -2)
        {
            if (!m_bJump && !m_bSlide && !m_bMove && m_gameManager.State == (int)GameState.Play)
            {
                m_bMove = true;
                //m_animator.SetBool("isRunLeft", true);
                m_animator.SetTrigger("triRunLeft");
                m_fDesPosX = m_iPosX - 1;
            }
        }

    }

    public void MoveRight()
    {

        if (m_iPosX < 2)
        {
            if (!m_bJump && !m_bSlide && !m_bMove && m_gameManager.State == (int)GameState.Play)
            {
                m_bMove = true;
                //m_animator.SetBool("isRunRight", true);
                m_animator.SetTrigger("triRunRight");
                m_fDesPosX = m_iPosX + 1;
            }
        }


    }

    public void Jump()
    {
        if (!m_bMove && !m_bJump && !m_bSlide && m_gameManager.State == (int)GameState.Play)
        {
            m_bJump = true;
            //m_animator.SetBool("isJump", true);
            m_animator.SetTrigger("triJump");
            GetComponent<Rigidbody>().useGravity = false;
            m_hitbox.center = m_hitbox.center + new Vector3(0, 0.7f, 0);

        }

    }

    public void Slide()
    {

        if (!m_bMove && !m_bSlide && !m_bJump && m_gameManager.State == (int)GameState.Play)
        {
            m_bSlide = true;
            //m_animator.SetBool("isSlide", true);
            m_animator.SetTrigger("triSlide");
            m_hitbox.center = m_hitbox.center + new Vector3(0, -0.35f, 0);
            m_hitbox.size = m_hitbox.size + new Vector3(0, -0.7f, 0);

        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gameManager.State)
        {
            case (int)GameState.Intro:
                {
                    break;
                }
            case (int)GameState.Play:
                {
                    m_oldPosZ = m_curPosZ;
                    m_player.transform.Translate(m_player.transform.forward * m_runSpeed * Time.deltaTime);
                    m_curPosZ = m_player.transform.position.z;
                    m_distance += m_curPosZ - m_oldPosZ;
                    if(m_distance >= 0.1f)
                    {
                        m_distance -= 0.1f;
                        m_uiManager.IncScore(10);
                    }
                    if (m_player.transform.position.y <= -0.8f)
                    {
                        Drop();
                    }
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            MoveLeft();
                        }

                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            MoveRight();
                        }
                        if(Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            Slide();
                        }

                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            Jump();
                        }
                    }
                    if (m_bMove)
                    {
                        m_fMoveElapsedTime += Time.deltaTime;
                        if (m_fMoveElapsedTime >= m_fMoveTime)
                        {
                            m_fMoveElapsedTime = 0;
                            m_iPosX = m_fDesPosX;
                            Vector3 position = m_player.transform.position;
                            position.x = m_iPosX;
                            m_player.transform.position = position;
                            m_bMove = false;
                            return;
                        }
                        float lerp = m_iPosX + ((float)(m_fDesPosX - m_iPosX) * (m_fMoveElapsedTime / m_fMoveTime));
                        Vector3 vector = m_player.transform.position;
                        vector.x = lerp;
                        m_player.transform.position = vector;
                    }
                    if (m_bJump)
                    {
                        m_fJumpElapsedTime += Time.deltaTime;

                        if (m_fJumpElapsedTime >= m_fJumpTime)
                        {
                            m_fJumpElapsedTime = 0;
                            GetComponent<Rigidbody>().useGravity = true;
                            m_hitbox.center = m_hitbox.center + new Vector3(0, -0.7f, 0);
                            m_bJump = false;
                        }
                    }
                    if (m_bSlide)
                    {
                        m_fSlideElapsedTime += Time.deltaTime;
                        if (m_fSlideElapsedTime >= m_fSlideTime)
                        {
                            m_fSlideElapsedTime = 0;
                            m_hitbox.center = m_hitbox.center + new Vector3(0, 0.35f, 0);
                            m_hitbox.size = m_hitbox.size + new Vector3(0, 0.7f, 0);
                            m_bSlide = false;

                        }
                    }
                    break;
                }
            case (int)GameState.Crash:
                {
                    break;
                }
            case (int)GameState.Drop:
                {
                    break;
                }
            case (int)GameState.Finish:
                {

                    break;
                }
            case (int)GameState.GameOver:
                {
                    m_animator.SetBool("isRun", false);
                    break;
                }
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coin":
                m_effectManager.MakeCoinDestroyEffect(collision.gameObject.transform.position);
                collision.gameObject.GetComponent<Coin>().Hide();
                m_uiManager.IncScore(100);
                break;
            case "Hurdle0":
                if (!m_bJump)
                {
                    Crash();
                    //collision.gameObject.SetActive(false);
                    Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
                    //m_effectManager.MakeCrashEffect(collision.gameObject.transform.position);
                    obstacle.Crash();
                }
                //m_uiManager.IncScore(500);
                break;
            case "Hurdle1":
                if (!m_bSlide)
                {
                    Crash();
                    //collision.gameObject.SetActive(false);
                    Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
                    //m_effectManager.MakeCrashEffect(collision.gameObject.transform.position);
                    obstacle.Crash();
                }
                //m_uiManager.IncScore(500);
                break;
            case "Strut":
                {
                    Crash();
                    //collision.gameObject.SetActive(false);
                    Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
                    obstacle.Crash();
                    break;
                }
        }
    }

    public void SetJump()
    {
        m_bJump = false;
    }

    private void Crash()
    {
        // m_player.transform.position = m_player.transform.position + new Vector3(0, -m_player.transform.position.y, -5);
        m_runEffect.SetActive(false);
        m_gameManager.State = (int)GameState.Crash;
        m_animator.SetTrigger("triCrash");
        m_uiManager.DecLife();

    }

    private void Drop()
    {
        m_runEffect.SetActive(false);
        m_gameManager.State = (int)GameState.Drop;
        m_player.transform.position =  new Vector3(Mathf.Round(m_player.transform.position.x), 1.5f, (int)m_player.transform.position.z);
        m_animator.SetTrigger("triCrash");
        m_uiManager.DecLife();
        m_blockManager.AddDropBlock((int)m_player.transform.position.x, (int)m_player.transform.position.z);
    }
    public void Finish()
    {
        m_runEffect.SetActive(false);
        m_animator.SetTrigger("triFinish");
        m_animator.SetTrigger("triFinish1");
    }
    public void GameOver()
    {
        m_animator.SetTrigger("triGameOver");
        m_gameManager.State = (int)GameState.GameOver;
        if(GameInfo.GetInstance.Infinite)
        {
            GameInfo.GetInstance.Score = m_uiManager.m_iScore;
            GameInfo.GetInstance.CheckInfiniteScore();
        }
    }
}
