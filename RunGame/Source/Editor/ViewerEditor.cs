using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatternViewer))]
public class MapMakerEditor : Editor {
    private PatternViewer m_this;

    void OnEnable()
    {
        m_this = target as PatternViewer;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        GUILayout.Label("Stage");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
        {
            if (m_this.m_iStage > 0)
            {
                m_this.m_iStage--;
            }
        }
        m_this.m_iStage = int.Parse(EditorGUILayout.TextArea(string.Format("{0}", m_this.m_iStage), GUILayout.MinWidth(50.0f)));
        if (GUILayout.Button(">>"))
        {
            m_this.m_iStage++;
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
        {
            m_this.NewStage();
        }
        if (GUILayout.Button("Load"))
        {
            m_this.LoadStage();
        }
        if (GUILayout.Button("Save"))
        {
            m_this.SaveStage();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        GUILayout.Label("Pattern");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
        {
            if (m_this.m_iPattern > 0)
            {
                //m_this.ReleaseStation();
                m_this.m_iPattern--;
                m_this.SwitchPattern();
            }
        }
        m_this.m_iPattern = int.Parse(EditorGUILayout.TextArea(string.Format("{0}", m_this.m_iPattern), GUILayout.MinWidth(50.0f)));
        if (GUILayout.Button(">>"))
        {
            if (m_this.m_iPattern < m_this.PatternSize)
            {
                //m_this.ReleaseStation();
                m_this.m_iPattern++;
                m_this.SwitchPattern();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        GUILayout.Label("Station");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
        {
            if (m_this.m_iStation > 0)
            {
                m_this.MoveLeft();
            }
        }
        m_this.m_iStation = int.Parse(EditorGUILayout.TextArea(string.Format("{0}", m_this.m_iStation), GUILayout.MinWidth(50.0f)));
        if (GUILayout.Button(">>"))
        {
            m_this.MoveRight();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove"))
        {
            m_this.Remove();
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Test Run"))
        {
            m_this.TestRun();
        }
    }

}
