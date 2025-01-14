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

    [SerializeField] Animator[] movings;    // 로딩 애니메이션
    [SerializeField] GameObject[] loadings; // 로딩 애니메이션
    [SerializeField] Animator[] pings;      // 로딩 애니메이션

    private int SceneNumber = 0;

    //[SerializeField]
    //TextMeshProUGUI progressText; // 로딩 진행도

    private void Start()
    {
        // 다음 씬 이름 표시
        sceneNameText.text = nextScene;

        // 씬 로딩 시작
        StartCoroutine(LoadScene());
    }

    private void OnEnable()
    {
        // 다음 씬 이름에서 숫자만 추출
        SceneNumber = (int)nextScene[nextScene.Length - 1] - 49;

        // 로딩 애니메이션 비활성화
        foreach (var loading in loadings)
        {
            loading.SetActive(false);
        }

        // "Boss"가 포함된 경우
        if (nextScene.Contains("Boss"))
        {
            Debug.Log("Boss scene detected.");
            // Boss 씬에 대한 특별한 로직을 여기에 추가

            // 해당 씬 위치의 로딩 애니메이션을 활성화
            loadings[4].SetActive(true);

            // 로딩 애니메이션 재생
            for (int i = 0; i < movings.Length; i++)
            {
                if (i == 4)
                {
                    movings[i].SetTrigger("isActivate");
                }
                else if (i < 4)
                {
                    movings[i].SetTrigger("isComplete");
                }
            }

            // 로딩 애니메이션 재생
            pings[4].SetTrigger("isActivate");

            return;
        }

        // 해당 씬 위치의 로딩 애니메이션을 활성화
        loadings[SceneNumber].SetActive(true);

        // 로딩 애니메이션 재생
        for (int i = 0; i < movings.Length; i++)
        {
            if (i == SceneNumber)
            {
                movings[i].SetTrigger("isActivate");
            }
            else if (i < SceneNumber)
            {
                movings[i].SetTrigger("isComplete");
            }
        }

        // 로딩 애니메이션 재생
        pings[SceneNumber].SetTrigger("isActivate");

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
                //progressText.text = $"Loading {progress:F0}%";

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
                //progressText.text = $"Loading {progress:F0}%";

                if (progress >= 100f)
                {
                    // 1초 대기
                    yield return new WaitForSeconds(1.0f);
                    
                    // 씬 활성화
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}