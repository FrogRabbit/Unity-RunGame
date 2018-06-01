using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameInfo
{
    private static GameInfo m_this = null;
    private int m_iStage = 0;
    private int m_StageSize = 0;
    private bool m_biIfinite = false;
    private float m_fIntroTime = 8.0f;
    private int m_iScore;
    private bool m_bFinish = false;
    private int m_iMaxStage = 25;
    private int[] m_iarrStage;
    private int[] m_iarrScore;
    private int m_iInfiniteScore;


    public static GameInfo GetInstance
    {
        get
        {
            if (m_this == null)
            {
                m_this = new GameInfo();
            }
            return m_this;
        }
    }

    public bool Infinite
    {
        get { return m_biIfinite; }
        set { m_biIfinite = value; }
    }

    public int Stage
    {
        get { return m_iStage;  }
        set { m_iStage = value; }
    }

    public void Init()
    {
        m_iarrStage = new int[m_iMaxStage];
        m_iarrScore = new int[m_iMaxStage];
        StreamReader stage_sr = File.OpenText("Assets/Stage/Stage_Save.txt");
        int count = 0;
        while (true)
        {
            string data = stage_sr.ReadLine();
            if (data == null) break;
            m_iarrStage[count] = int.Parse(data);
            count++;
        }
        stage_sr.Close();

        StreamReader score_sr = File.OpenText("Assets/Stage/stage_score.txt");
        count = 0;
        while (true)
        {
            string data = score_sr.ReadLine();
            if (data == null) break;
            m_iarrScore[count] = int.Parse(data);
            count++;
        }
        score_sr.Close();

        StreamReader infinite_sr = File.OpenText("Assets/Stage/infinite_score.txt");
        m_iInfiniteScore = int.Parse(infinite_sr.ReadLine());
        infinite_sr.Close();
    }
    public void CheckInfiniteScore()
    {
        if(m_iInfiniteScore <= m_iScore)
        {
            m_iInfiniteScore = m_iScore;
            SaveInfiniteScore();
        }
    }

    public int InfiniteScore()
    {
        return m_iInfiniteScore;
    }
    private void SaveInfiniteScore()
    {
        StreamWriter sw = new StreamWriter("Assets/stage/infinite_score.txt");
        sw.WriteLine(string.Format("{0}", m_iInfiniteScore));
        sw.Close();
    }

    public int GetStage(int index)
    {
        return m_iarrStage[index];
    }
    public void UnlockStage(int index)
    {
        m_iarrStage[index] = int.Parse("1");
        SaveStage();
    }

    public int GetScore(int index)
    {
        return m_iarrScore[index];
    }

    public int GetScore()
    {
        return m_iarrScore[m_iStage];
    }

    public void CheckScore(int score)
    {
        if (m_iarrScore[m_iStage] <= score)
        {
            m_iarrScore[m_iStage] = score;
            SaveScore();
        }
    }
    public void CheckScore(int index, int score)
    {
        if(m_iarrScore[index] <= score)
        {
            m_iarrScore[index] = score;
            SaveScore();
        }
    }
    public void CheckScore()
    {
        if (m_iarrScore[m_iStage] <= m_iScore)
        {
            m_iarrScore[m_iStage] = m_iScore;
            SaveScore();
        }
    }
    public void UnlockStage()
    {
        if (m_iStage + 1 < m_iMaxStage)
        {
            m_iarrStage[m_iStage + 1] = int.Parse("1");
        }
        SaveStage();
    }

    private void SaveStage()
    {
        StreamWriter sw = new StreamWriter("Assets/stage/stage_save.txt");
        for (int i = 0; i < m_iarrStage.Length; i++)
        {
            sw.WriteLine(string.Format("{0}", m_iarrStage[i]));
        }
        sw.Close();
    }

    private void SaveScore()
    {
        StreamWriter sw = new StreamWriter("Assets/stage/stage_score.txt");
        for (int i = 0; i < m_iarrScore.Length; i++)
        {
            sw.WriteLine(string.Format("{0}", m_iarrScore[i]));
        }
        sw.Close();
    }

    public float IntroTime
    {
        get { return m_fIntroTime; }
    }
    

    public int Score
    {
        get { return m_iScore; }
        set { m_iScore = value; }
    }

    public bool Finish
    {
        get { return m_bFinish; }
        set { m_bFinish = value; }
    }

    public int MaxStage
    {
        get { return m_iMaxStage; }
        set { m_iMaxStage = value; }
    }
}
