using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : ObjectPool {

	// Use this for initialization
	void Start () {
        Reserve("CoinEffect", 100);
        Reserve("CoinDestroyEffect", 10);
        Reserve("CrashEffect", 10);
        Reserve("FireWorkEffect", 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject MakeCoinEffect(Vector3 pos)
    {
        GameObject obj = GetObject("CoinEffect");
        obj.transform.position = pos;
        return obj;
    }

    public void MakeCoinDestroyEffect(Vector3 pos)
    {
        GameObject obj = GetObject("CoinDestroyEffect");
        obj.transform.position = pos;
    }

    public void MakeCrashEffect(Vector3 pos)
    {
        GameObject obj = GetObject("CrashEffect");
        obj.transform.position = pos;
    }

    public void MakeFireWork(Vector3 pos)
    {
        GameObject obj = GetObject("FireWorkEffect");
        obj.transform.position = pos;
    }
}
