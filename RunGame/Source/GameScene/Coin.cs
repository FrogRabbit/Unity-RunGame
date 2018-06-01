using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public GameObject m_coin;
    private GameObject m_coinEffect;
    private Transform m_playerTransform;

    void Start() {
        m_playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public GameObject CoinEffect
    {
        set { m_coinEffect = value; }
    }
	// Update is called once per frame
	void Update () {
        if (m_coin.transform.position.z - m_playerTransform.position.z <= -10)
        {
            m_coin.SetActive(false);
            m_coinEffect.SetActive(false);
        }
    }

    public void Hide()
    {
        m_coin.SetActive(false);
        m_coinEffect.SetActive(false);
    }
}
