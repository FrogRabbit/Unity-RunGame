using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSceneCamera : MonoBehaviour {

    public Transform m_camera;
    public float m_distance;
    public float m_height;
    private float m_angle = 0;
    private float m_time = 0;
    private int m_length;
    private bool m_bRun = false;
    private Vector3 m_startPos;
    private Vector3 m_desPos;
	void Start () {
        m_camera.position = new Vector3(5, 5, 3.0f);
        m_camera.LookAt(new Vector3(0, 0, 3.0f));
        m_bRun = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(m_bRun)
        {
            m_time += Time.deltaTime;
           
            Vector3 pos = Vector3.Lerp(m_startPos, m_desPos, (float)(m_time / (m_length * 1.0f)));
            m_camera.position = pos;
            m_camera.LookAt(pos + new Vector3(0, 0, 3));

            if (m_time >= m_length * 1.0f)
            {
                m_bRun = false;
                m_time = 0;
                m_camera.position = new Vector3(5, 5, 2.5f);
                m_camera.LookAt(new Vector3(0, 0, 2.5f));
            }
        }
    }

    public void TestRun(int length)
    {
        m_length = length;
        m_startPos = new Vector3(0, 2, -3);
        m_desPos = new Vector3(0, 2, (m_length * 7) - 6);
        m_bRun = true;
    }

    public void MoveTo(int station)
    {
        m_camera.position = new Vector3(5, 5, (station * 7) + 3.0f);
        m_camera.LookAt(new Vector3(0, 0, (station * 7) + 3.0f));
    }
}
