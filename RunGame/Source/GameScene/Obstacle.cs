using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public GameObject m_object;
    private EffectManager m_effectManager;
    private Transform m_playerTransform;
    private Animation m_animation;
    // Use this for initialization
    void Start() {
        m_playerTransform = GameObject.FindWithTag("Player").transform;
        m_animation = GetComponent<Animation>();
        m_effectManager = GameObject.FindWithTag("EffectManager").GetComponent<EffectManager>();
    }

    // Update is called once per frame
    void Update() {
        if (m_object.transform.position.z - m_playerTransform.position.z <= -10)
        {
            m_object.SetActive(false);
        }
    }

    public void SetActiveFalse()
    {
        m_object.transform.rotation = new Quaternion(0, 0, 0, 0);
        m_object.SetActive(false);
    }

    public void Crash()
    {
        m_effectManager.MakeCrashEffect(m_object.transform.position);
        m_animation.Play();
    }
}
