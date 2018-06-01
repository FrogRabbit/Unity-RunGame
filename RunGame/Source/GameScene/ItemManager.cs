using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : ObjectPool {

    public EffectManager m_effectManager;
    public float m_height;
	void Start () {
        Reserve("Coin", 100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCoin(Vector3 pos)
    {
        GameObject obj = GetObject("Coin");
        obj.transform.position = pos;
        obj.GetComponent<Coin>().CoinEffect = m_effectManager.MakeCoinEffect(pos);
    }

    public void AddCoin(int x, int z)
    {
        GameObject obj = GetObject("Coin");
        obj.transform.position = new Vector3(x, m_height, z);
        obj.GetComponent<Coin>().CoinEffect = m_effectManager.MakeCoinEffect(new Vector3(x, m_height, z));
    }
}
