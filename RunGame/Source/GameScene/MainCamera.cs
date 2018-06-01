using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public GameObject m_mainCamera;
    public GameObject m_player;
    public GameManager m_gameManager;
    public Transform m_playerTransform;
    public Vector3 m_LookAt;
    public float m_Distance;
    public float m_angle = 0.0f;
   
	void Start () {
        m_mainCamera.transform.LookAt(m_playerTransform.forward + new Vector3(0, 0, 2));
        m_mainCamera.transform.position = m_player.transform.position + new Vector3(-m_player.transform.position.x, 2, -m_Distance);
        //m_mainCamera.transform.position = m_player.transform.position + new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(m_gameManager.State)
        {
            case (int)GameState.Intro:
                {
                    float agree = (360 * (m_gameManager.ElapsedTime / 8.0f)) / 180.0f * 3.14f;
                    m_mainCamera.transform.position = new Vector3(m_playerTransform.position.x + Mathf.Sin(agree) * m_Distance, 2, m_playerTransform.position.z + Mathf.Cos(agree) * -m_Distance);
                    Vector3 lerp = Vector3.Lerp(m_playerTransform.position, m_playerTransform.position + m_playerTransform.forward, m_gameManager.ElapsedTime / 8.0f);
                    m_mainCamera.transform.LookAt(lerp);
                    break;
                }
            case (int)GameState.Play:
                {
                    m_mainCamera.transform.position = m_player.transform.position + new Vector3(-m_player.transform.position.x, -m_player.transform.position.y + 2, -m_Distance);
                    m_mainCamera.transform.LookAt(m_player.transform.position + new Vector3(-m_player.transform.position.x, -m_player.transform.position.y, 1));
                    break;
                }
            case (int)GameState.Finish:
                {
                    float agree = 540 * (m_gameManager.ElapsedTime / 8.0f) / 180 * 3.14f;
                    m_mainCamera.transform.position = new Vector3(m_playerTransform.position.x + Mathf.Sin(agree) * m_Distance, 2, m_playerTransform.position.z + Mathf.Cos(agree) * -m_Distance);
                    Vector3 lerp = Vector3.Lerp(m_playerTransform.position + m_playerTransform.forward, m_playerTransform.position + new Vector3(0, 1, 0), m_gameManager.ElapsedTime / 8.0f);
                    m_mainCamera.transform.LookAt(lerp);
                }
                break;
            case (int)GameState.GameOver:
                {
                    float agree = 540 * (m_gameManager.ElapsedTime / 8.0f) / 180 * 3.14f;
                    m_mainCamera.transform.position = new Vector3(m_playerTransform.position.x + Mathf.Sin(agree) * m_Distance, 2, m_playerTransform.position.z + Mathf.Cos(agree) * -m_Distance);
                    Vector3 lerp = Vector3.Lerp(m_playerTransform.position + m_playerTransform.forward, m_playerTransform.position + new Vector3(0, 1, 0), m_gameManager.ElapsedTime / 8.0f);
                    m_mainCamera.transform.LookAt(lerp);
                }
                break;
        }
        //m_mainCamera.transform.position = m_player.transform.position + new Vector3(-m_player.transform.position.x, 2, -2);
        //m_mainCamera.transform.RotateAround(m_playerTransform.position, m_playerTransform.up , 2.0f);
        //m_mainCamera.transform.LookAt(m_playerTransform.position);
        /*m_angle += 2.0f * Time.deltaTime;
        m_mainCamera.transform.position = new Vector3(5*Mathf.Sin(m_angle), 1, 5 * Mathf.Cos(m_angle));
        m_mainCamera.transform.LookAt(m_playerTransform.forward);*/
        

    }
}
