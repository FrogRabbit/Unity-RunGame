using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScene : MonoBehaviour {

    public UISprite m_progressBar;
    public static string nextScene;
	// Use this for initialization
	void Start () {
        m_progressBar.fillAmount = 0.0f;
        StartCoroutine(LoadScene());

	}

    string nextSceneName;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                m_progressBar.fillAmount = Mathf.Lerp(m_progressBar.fillAmount, 1f, timer);

                if (m_progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    Debug.Log("Load Complete");
                }
            }
            else
            {
                m_progressBar.fillAmount = Mathf.Lerp(m_progressBar.fillAmount, op.progress, timer);
                if (m_progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}
