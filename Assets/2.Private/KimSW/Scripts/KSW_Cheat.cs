using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class KSW_Cheat : MonoBehaviour
{
    [Inject] InGameUI gameUI;
    [Inject] PlayerUIModel playerModel;
    [Inject] PoolManager poolManager;
    Transform target;

    [SerializeField] TMP_Text stopWatch;
    float stopWatchTime;

    public float tempMaxHp;
    public float tempHp;

    public bool isHpCheat;
    public bool isStopWatch;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        ResourceChaet();
        DistanceCheat();
        InvincibleCheat();
        StopWatchCheat();

       
    }

    public void ResourceChaet()
    {
        // 쓰레기 생성 치트키
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Vector3 targetPos = target.position;
            targetPos.x += Random.Range(-5,5);
            targetPos.z += Random.Range(-5, 5);


            Garbage garbage = poolManager.GetObject<Garbage>((E_Garbage)Random.Range((int)E_Garbage.Garbage1,(int)E_Garbage.Size));
            garbage.transform.position = targetPos;

        }


    }

    public void DistanceCheat()
    {
        // 거리 치트키
        if (Input.GetMouseButton(0))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                gameUI.TargetIndicator.EnableIndicator(hit.point);
            }
        }
    }

    public void InvincibleCheat()
    {
        // 무적 치트키
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (!isHpCheat)
            {
                isHpCheat = true;
                tempMaxHp = playerModel.MaxHp.Value;
                tempHp = playerModel.Hp.Value;
                playerModel.MaxHp.Value = 1000000;
                playerModel.Hp.Value = playerModel.MaxHp.Value;
            }
            else
            {
                isHpCheat = false;
                playerModel.MaxHp.Value = tempMaxHp;
                playerModel.Hp.Value = tempHp;
            }
        }
    }

    public void StopWatchCheat()
    {
        // 스톱워치 온오프 치트키
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            stopWatch.gameObject.SetActive(!stopWatch.gameObject.activeSelf);
            stopWatchTime = 0;
            stopWatch.text = stopWatchTime.ToString("F1");

        }
        // 스톱워치 작동 치트키
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            isStopWatch = !isStopWatch;

        }

        if (isStopWatch)
        {
            stopWatchTime += Time.deltaTime;
            stopWatch.text = stopWatchTime.ToString("F1");
        }
    }
}
