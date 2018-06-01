using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectPanel : ObjectPool {
    // Use this for initialization
    void Start()
    {
        Reserve("StageButton", 30);

        for (int i = 0; i < GameInfo.GetInstance.MaxStage; i++)
        {
            float width = ((i % 5) - 2) * 200;
            float height = ((i / 5) - 2) * -110;
            GameObject obj = GetObject("StageButton");
            obj.transform.localPosition = new Vector3(width, height, 0);
            obj.transform.localScale = Vector3.one;
            StageButton button = obj.GetComponent<StageButton>();
            if (GameInfo.GetInstance.GetStage(i) == 1)
            {
                button.Init(i, true);
            }
            else
            {
                button.Init(i, false);
            }
        }
        
       
       

    }
	// Update is called once per frame
	void Update () {
		
	}
}
