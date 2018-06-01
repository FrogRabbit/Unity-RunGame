using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockManager : ObjectPool
{
    private List<GameObject> m_BlockList = new List<GameObject>();
    private int m_StageLength = 3;
    public ItemManager m_itemManager;
    public MapManager m_mapManager;
    public GameObject m_finishLine;
    void Start()
    {
        Reserve("Block", 100);
        Reserve("SideBlock", 100);

        /*AddStartBlock();
        //FileStream file = new FileStream(m_filePath + string.Format("stage_{0}.txt", m_iStage), FileMode.Open, FileAccess.Read);
        while (true)
        {
            string line = sr.ReadLine();
            if (line == null)
            {
                sr.Close();
                break;
            }
            string[] data = line.Split(',');
            for (int i = 0; i < 5; i++)
            {
                if (data[i] != "0")
                {
                    GameObject obj = GetObject("Block");
                    obj.transform.position = new Vector3(i - 2, 0, m_StageLength);
                    if (Random.Range(1, 100) >= 60)
                    {
                        m_itemManager.MakeCoin(new Vector3(i - 2, 1, m_StageLength));
                    }
                }
            }
            m_StageLength++;
        }
        AddFinishBlock();
        AddSideBlock();*/

    }

    void Update()
    {
      
    }
    public void AddBlock(int x, int y)
    {
        GameObject obj = GetObject("Block");
        obj.transform.position = new Vector3(x, 0, y);
        m_BlockList.Add(obj);
    }

    public void AddStartBlock()
    {
        for (int i = -2; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject obj = GetObject("Block");
                obj.transform.position = new Vector3(j - 2, 0, i);
                m_BlockList.Add(obj);
            }
            AddSideBlock(i);
            //m_mapManager.Length++;
        }
        m_mapManager.Length = 5;
        //출발선 추가
    }

    public void AddFinishBlock()
    {
        if (!GameInfo.GetInstance.Infinite)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GameObject obj = GetObject("Block");
                    obj.transform.position = new Vector3(j - 2, 0, m_mapManager.Length + i);
                    m_BlockList.Add(obj);
                }
                AddSideBlock(m_mapManager.Length + i);

            }
            //결승선 추가
            m_mapManager.Length += 5;
            m_finishLine.SetActive(true);
            m_finishLine.transform.position = new Vector3(0, 0, m_mapManager.Length - 1.0f);
        }
    }

    public void AddSideBlock()
    {
        
        GameObject obj = GetObject("SideBlock");
        obj.transform.position = new Vector3(-3, 0, m_mapManager.Length);
        m_BlockList.Add(obj);
        obj = GetObject("SideBlock");
        obj.transform.position = new Vector3(3, 0, m_mapManager.Length);
        m_BlockList.Add(obj);
    }

    public void AddSideBlock(int z)
    {

        GameObject obj = GetObject("SideBlock");
        obj.transform.position = new Vector3(-3, 0, z);
        m_BlockList.Add(obj);
        obj = GetObject("SideBlock");
        obj.transform.position = new Vector3(3, 0, z);
        m_BlockList.Add(obj);
    }

    public void AddLineBlock()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = GetObject("Block");
            obj.transform.position = new Vector3(i - 2, 0, m_mapManager.Length);
            m_BlockList.Add(obj);
        }
        AddSideBlock();
        m_mapManager.Length++;
    }

    public void AddDropBlock(int x, int z)
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject obj = GetObject("Block");
            obj.transform.position = new Vector3(x, 0, z + i);
            m_BlockList.Add(obj);
        }
    }
}
