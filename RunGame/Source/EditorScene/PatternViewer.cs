using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatternViewer : ObjectPool {

    public int m_iPattern = 0;
    public int m_iStage = 0;

    public UILabel m_patternLabel;
    public string m_patternPath;
    public int PatternSize;
    private Dictionary<int, List<GameObject>> m_MapList = new Dictionary<int, List<GameObject>>();
    private Dictionary<int, int> m_MapPattern = new Dictionary<int, int>();
    public List<GameObject> m_StationList = null;
    public Transform m_cameraTransform;
    public EditorSceneCamera m_camara;
    public int m_iStation = 0;
    void Start()
    {
        m_iStation = 0;
        Reserve("SideBlock", 100);
        Reserve("Block", 200);
        Reserve("Coin", 200);
        Reserve("Hurdle0", 50);
        Reserve("Hurdle1", 50);
        MakePattern(0);
    }


    void Update()
    {
        m_patternLabel.text = string.Format("Pattern {0}", m_iPattern);
    }

    public void MakePattern()
    {
        m_StationList = new List<GameObject>();
       

        string line;
        StreamReader pattern_sr = File.OpenText(m_patternPath + "Pattern_" + m_iPattern + ".txt");
        for (int i = 0; i < 6; i++)
        {
            line = pattern_sr.ReadLine();
            MakeLine(line, (m_iStation * 7) + i);
        }
        AddLine();
        pattern_sr.Close();
        m_MapList.Add(m_iStation, m_StationList);

    }

    public void MakePattern(int pattern)
    {
        m_StationList = new List<GameObject>();
        string line;
        StreamReader pattern_sr = File.OpenText(m_patternPath + "Pattern_" + pattern + ".txt");
        for (int i = 0; i < 6; i++)
        {
            line = pattern_sr.ReadLine();
            MakeLine(line, (m_iStation * 7) + i);
        }
        AddLine();
        pattern_sr.Close();
        m_MapList.Add(m_iStation, m_StationList);
        m_MapPattern.Add(m_iStation, pattern);

    }

    public void SwitchPattern()
    {
        
        if (m_MapList.ContainsKey(m_iStation))
        {
            m_StationList = m_MapList[m_iStation];
        }
        ReleaseStation(m_StationList);
        string line;
        StreamReader pattern_sr = File.OpenText(m_patternPath + "Pattern_" + m_iPattern + ".txt");
        m_MapPattern[m_iStation] = m_iPattern;
        for (int i = 0; i < 6; i++)
        {
            line = pattern_sr.ReadLine();
            MakeLine(line, (m_iStation * 7) + i);
        }
        AddLine();
        pattern_sr.Close();
    }
    public void AddLine()
    {
        MakeSideBlock((m_iStation * 7) + 6);
        for(int i = 0; i < 5; i++)
        {
            MakeBlock(i - 2, (m_iStation * 7) + 6);
        }
    }

    public void ReleaseMap()
    {
        m_iStation = 0;
        for(int i = 0; i < m_MapList.Count; i++)
        {
            ReleaseStation(m_MapList[i]);
        }
        m_MapList.Clear();

        m_MapPattern.Clear();
    }

    public void ReleaseStation(List<GameObject> list)
    {
        foreach (GameObject var in list)
        {
            var.SetActive(false);
        }
        list.Clear();
    }

    public void ReleaseStation()
    {
        foreach (GameObject var in m_StationList)
        {
            var.SetActive(false);
        }
        m_StationList.Clear();
    }

    private void MakeLine(string line, int length)
    {
        string[] data = line.Split(',');
        for (int j = 0; j < data.Length; j++)
        {
            switch (data[j])
            {
                case "1":
                    MakeBlock(j - 2, length);
                    break;
                case "2":
                    MakeBlock(j - 2, length);
                    MakeCoin(j - 2, length);
                    break;
                case "3":
                    MakeBlock(j - 2, length);
                    MakeHurdle0(j - 2, length);
                    break;
                case "4":
                    MakeBlock(j - 2, length);
                    MakeHurdle1(j - 2, length);
                    break;
                default:
                    break;
            }
        }
        MakeSideBlock(length);
    }

    private void MakeBlock(int x, int z)
    {
        GameObject obj = GetObject("Block");
        obj.transform.localPosition = new Vector3(x, 0, z);
        m_StationList.Add(obj);
    }

    private void MakeSideBlock(int z)
    {
        GameObject obj = GetObject("SideBlock");
        obj.transform.localPosition = new Vector3(-3, 0, z);
        m_StationList.Add(obj);
        obj = GetObject("SideBlock");
        obj.transform.localPosition = new Vector3(3, 0, z);
        m_StationList.Add(obj);
    }

    private void MakeHurdle0(int x, int z)
    {
        GameObject obj = GetObject("Hurdle0");
        obj.transform.localPosition = new Vector3(x, 0, z);
        m_StationList.Add(obj);
    }

    private void MakeHurdle1(int x, int z)
    {
        GameObject obj = GetObject("Hurdle1");
        obj.transform.localPosition = new Vector3(x, 0, z);
        m_StationList.Add(obj);
    }

    private void MakeCoin(int x, int z)
    {
        GameObject obj = GetObject("Coin");
        obj.transform.localPosition = new Vector3(x, 0.5f, z);
        m_StationList.Add(obj);
    }

    public void LoadStage()
    {
        ReleaseMap();
        StreamReader stage_sr = File.OpenText("Assets/Stage/" + string.Format("stage_{0}.txt", m_iStage));
        string[] arripattern = stage_sr.ReadLine().Split(',');
        stage_sr.Close();

        for (int i = 0; i < arripattern.Length; i++)
        {
           
            MakePattern(int.Parse(arripattern[i]));
            m_iStation++;
        }

        m_iStation = 0;
        m_StationList = m_MapList[m_iStation];
        m_iPattern = m_MapPattern[m_iStation];
    }

    public void SaveStage()
    {
        string saveLine = null;
        for(int i = 0; i < m_MapPattern.Count; i++)
        {
            saveLine += string.Format("{0}", m_MapPattern[i]);
            if (i == m_MapPattern.Count - 1)
            {
                break;
            }
            saveLine += ',';
        }
        int a = 0;
        StreamWriter sw = new StreamWriter("Assets/stage/"+string.Format("stage_{0}.txt", m_iStage));
        sw.WriteLine(saveLine);
        sw.Close();
    }

    public void MoveLeft()
    {
        m_iStation--;
        m_cameraTransform.position = new Vector3(5, 5, m_iStation * 7 + 3.0f);
        if(m_MapPattern.ContainsKey(m_iStation))
        {
            m_iPattern = m_MapPattern[m_iStation];
        }
        if (m_MapList.ContainsKey(m_iStation))
        {
            m_StationList = m_MapList[m_iStation];
        }
    }

    public void MoveRight()
    {
        m_iStation++;
        m_cameraTransform.position = new Vector3(5, 5, m_iStation * 7 + 3.0f);
        if (m_MapPattern.ContainsKey(m_iStation))
        {
            m_iPattern = m_MapPattern[m_iStation];
        }
        else
        {
            MakePattern(0);
        }
        if (m_MapList.ContainsKey(m_iStation))
        {
            m_StationList = m_MapList[m_iStation];
        }
        else
        {
            MakePattern(0);
        }
    }

    public void Remove()
    {
        m_StationList = m_MapList[m_MapList.Count - 1];
        ReleaseStation();
        m_MapList.Remove(m_MapList.Count - 1);
        m_MapPattern.Remove(m_MapPattern.Count - 1);
    }

    public void TestRun()
    {
        m_camara.TestRun(m_MapList.Count);
    }

    public void NewStage()
    {
        ReleaseMap();
        MakePattern(0);
        m_iPattern = 0;
        m_iStation = 0;
        m_camara.MoveTo(0);
    }
}
