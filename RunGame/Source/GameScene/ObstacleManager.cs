using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : ObjectPool {

    private List<GameObject> m_ObstacleList = new List<GameObject>();
	void Start () {
        Reserve("Hurdle0", 20);
        Reserve("Hurdle1", 20);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddObstacle(int index, int x, int z)
    {
        if(index == 0)
        {
            GameObject obj = GetObject("Hurdle0");
            obj.transform.position = new Vector3(x, 0, z);
            //m_ObstacleList.Add(obj);
        }
        else
        {
            GameObject obj = GetObject("Hurdle1");
            obj.transform.position = new Vector3(x, 0, z);
            //m_ObstacleList.Add(obj);
        }
       
    }
}
