using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterSelector : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] chapterTexts; // 챕터 텍스트들

    [SerializeField]
    GameObject[] cursors;

    private int selectedChapterIndex = 0; // 선택된 챕터 인덱스
    private int SelectedChapterIndex
    {
        get => selectedChapterIndex;
        set
        {
            cursors[selectedChapterIndex].SetActive(false);
            selectedChapterIndex = value;
            cursors[selectedChapterIndex].SetActive(true);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var cursor in cursors)
        {
            cursor.SetActive(false);
        }
        SelectedChapterIndex = 0;
    }

    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// 키보드 입력 처리
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(selectedChapterIndex > 0)
            {
                SelectedChapterIndex--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(selectedChapterIndex < chapterTexts.Length - 1)
            {
                SelectedChapterIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SceneChanger.LoadScene(chapterTexts[SelectedChapterIndex].text + "-1");
        }
    }
}
