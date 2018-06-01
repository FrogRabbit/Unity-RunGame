using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour {
    public BlockManager m_blockManager;
    public ItemManager m_itemManager;
    public ObstacleManager m_obstacleManager;
    public string m_stagePath;
    public string m_patternPath;
    private int x = 10;
    private int m_iStageLength = 0;
    private int m_iStage;
    public Transform m_playerTransform;
    private float m_desPlayerPosZ;
    public int m_iPatternSize;
    private bool m_infinite = false;
    private int m_delCount = 0;

    void Start() {
        m_iStageLength = 0;

        m_iStage = GameInfo.GetInstance.Stage;
        m_blockManager.AddStartBlock();
        m_infinite = GameInfo.GetInstance.Infinite;

        if (!m_infinite)
        {
            StreamReader stage_sr = File.OpenText(m_stagePath + string.Format("stage_{0}.txt", m_iStage));
            string[] arripattern = stage_sr.ReadLine().Split(',');
            stage_sr.Close();

            for (int i = 0; i < arripattern.Length; i++)
            {
                StreamReader pattern_sr = File.OpenText(m_patternPath + "Pattern_" + arripattern[i] + ".txt");
                while (true)
                {
                    string line = pattern_sr.ReadLine();
                    if (line == null)
                    {
                        pattern_sr.Close();
                        break;
                    }
                    MakeStation(line);
                }
                m_blockManager.AddLineBlock();
            }
        }
        else
        {
            m_desPlayerPosZ = m_playerTransform.position.z + 7.0f;
            for (int i = 0; i < 3; i++)
            {
                MakeRandomStation();
                m_blockManager.AddLineBlock();
            }
        }
        m_blockManager.AddFinishBlock();
        //m_blockManager.AddSideBlock();

    }

    // Update is called once per frame
    void Update() {
        if (m_infinite)
        {
            if (m_desPlayerPosZ <= m_playerTransform.position.z)
            {
                m_desPlayerPosZ += 7.0f;
                MakeRandomStation();
                m_blockManager.AddLineBlock();
            }
        }
    }

    private void MakeRandomStation()
    {
        int pattern = Random.Range(0, m_iPatternSize + 1);
        StreamReader pattern_sr = File.OpenText(m_patternPath + string.Format("Pattern_{0}.txt", pattern));
        while (true)
        {
            string line = pattern_sr.ReadLine();
            if (line == null)
            {
                pattern_sr.Close();
                break;
            }
            MakeStation(line);
        }
    }

    private void MakeStation(string str)
    {
        string[] data = str.Split(',');
        for (int j = 0; j < data.Length; j++)
        {
            switch (data[j])
            {
                case "0":
                    break;
                case "1":
                    m_blockManager.AddBlock(j - 2, m_iStageLength);
                    break;
                case "2":
                    m_blockManager.AddBlock(j - 2, m_iStageLength);
                    m_itemManager.AddCoin(j - 2, m_iStageLength);
                    break;
                case "3":
                    m_blockManager.AddBlock(j - 2, m_iStageLength);
                    m_obstacleManager.AddObstacle(0, j - 2, m_iStageLength);
                    break;
                case "4":
                    m_blockManager.AddBlock(j - 2, m_iStageLength);
                    m_obstacleManager.AddObstacle(1, j - 2, m_iStageLength);
                    break;

            }
        }
        m_blockManager.AddSideBlock();
        m_iStageLength++;
    }

    public int Length
    {
        get { return m_iStageLength; }
        set { m_iStageLength = value; }
    }
}
