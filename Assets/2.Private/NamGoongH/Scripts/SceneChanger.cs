using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public static string nextScene; // 다음 씬 이름

    [SerializeField]
    TextMeshProUGUI sceneNameText; // 다음 씬 이름

    [SerializeField]
    TextMeshProUGUI progressText; // 로딩 진행도

    private void Start()
    {
        // 다음 씬 이름 표시
        sceneNameText.text = nextScene;

        // 씬 로딩 시작
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 다음 씬 이름을 입력받고, 로딩 씬으로 이동
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    /// <summary>
    /// 씬 로딩
    /// </summary>
    IEnumerator LoadScene()
    {
        yield return null;

        // 비동기 씬 로딩
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            // 로딩 진행도 표시
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                float progress = Mathf.Lerp(0, op.progress, timer) * 100;

                // 로딩 진행도 표시
                progressText.text = $"Loading {progress:F0}%";

                if (progress >= op.progress * 100)
                {
                    timer = 0f;
                }
            }
            else
            {
                // 100% 로딩까지 기다림
                float progress = Mathf.Lerp(0, 1f, timer) * 100;

                // 로딩 진행도 표시
                progressText.text = $"Loading {progress:F0}%";

                if (progress >= 100f)
                {
                    // 씬 활성화
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}