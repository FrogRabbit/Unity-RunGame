using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public List<GameObject> m_GameObjectPrefab = new List<GameObject>();
    private Dictionary<string, List<GameObject>> m_objpool = new Dictionary<string, List<GameObject>>();

    private GameObject GetPrefabs(string prefabsName)
    {
        foreach(var v in m_GameObjectPrefab)
        {
            if(v.name == prefabsName)
            {
                return v;
            }
        }
        Debug.LogError(string.Format("{0} asdfasdf", prefabsName));
        return null;
    }
    protected void Reserve(string prefabsName, int iReserve)
    {
        GameObject obj_prefabs = GetPrefabs(prefabsName);
        for(int i = 0; i < iReserve; i++)
        {
            GameObject obj = GameObject.Instantiate<GameObject>(obj_prefabs);
            obj.SetActive(false);
            obj.transform.parent = transform;
            if(!m_objpool.ContainsKey(prefabsName))
            {
                List<GameObject> list = new List<GameObject>();
                list.Add(obj);
                m_objpool.Add(prefabsName, list);
            }
            else
            {
                List<GameObject> list = m_objpool[prefabsName];
                list.Add(obj);
            }
        }
    }

    protected GameObject GetObject(string prefabsName)
    {
        foreach(var v in m_objpool[prefabsName])
        {
            if(v.activeSelf == false)
            {
                v.SetActive(true);
                return v;
            }
        }
        Reserve(prefabsName, m_objpool.Count);
        return GetObject(prefabsName);
    }
}
