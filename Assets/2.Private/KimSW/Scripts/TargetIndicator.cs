using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform target;
    [SerializeField] TMP_Text disText;
    [SerializeField] float offset;

    public ReactiveProperty<int> Distance;

    Camera mainCam;

   

    void Awake()
    {
      
        mainCam = Camera.main;
        Distance = new ReactiveProperty<int>(0);

        Distance.Subscribe(value=> SetDistance(value));


        DisableIndicator();

    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        ClampTarget();
        Distance.Value = (int)Vector3.Distance(playerTransform.position, target.position);

    }

    /// <summary>
    /// 인디케이터 활성화
    /// </summary>
    /// <param name="target"> 목표 위치 설정 </param>
    public void EnableIndicator(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);

    }

    public void DisableIndicator()
    {
        gameObject.SetActive(false);
        this.target = null;
    }

    void ClampTarget()
    {
        if (target is null)
            return;

        Vector3 targetVec = mainCam.WorldToScreenPoint(target.position);
        transform.position = targetVec;


        Vector3 calmp = transform.position;

        calmp.x = Mathf.Clamp(calmp.x, offset, Screen.width - offset);
        calmp.y = Mathf.Clamp(calmp.y, offset, Screen.height - offset);

        transform.position = calmp;
    }

    void SetDistance(float value)
    {
        disText.text = value.ToString();
    }
}
