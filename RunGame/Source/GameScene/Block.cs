using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    
    public GameObject m_cube;
    private Transform m_playerTransform;
	void Start () {
        m_playerTransform = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if(m_cube.transform.position.z - m_playerTransform.position.z <= - 10)
        {
            m_cube.SetActive(false);
        }
	}
}
