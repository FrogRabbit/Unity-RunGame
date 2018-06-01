using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroyEffect : MonoBehaviour {
    public GameObject m_gameObject;
	private float m_time = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_time += Time.deltaTime;
        if(m_time >= 1.0f)
        {
            m_time = 0;
            m_gameObject.SetActive(false);
        }
	}
}
