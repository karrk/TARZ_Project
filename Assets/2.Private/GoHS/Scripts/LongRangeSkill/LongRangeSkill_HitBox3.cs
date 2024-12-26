using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeSkill_HitBox3 : MonoBehaviour
{
    [SerializeField] private GameObject punchObj;

    private void Start()
    {
        RandomPunchSpawn();
        StartCoroutine(MoveAndDestroryCoroutine());
    }

    private void Update()
    {
        
    }

    private void RandomPunchSpawn()
    {
        BoxCollider boxCol = GetComponent<BoxCollider>();
        if(boxCol != null )
        {
            Vector3 boxCenter = boxCol.center;
            Vector3 boxSize = boxCol.size;

            float randomX = Random.Range(boxCenter.x - boxSize.x / 2f, boxCenter.x + boxSize.x / 2f);
            float randomY = Random.Range(boxCenter.y - boxSize.y / 2f, boxCenter.y + boxSize.y / 2f);
            float randomZ = Random.Range(boxCenter.z - boxSize.z / 2f, boxCenter.z + boxSize.z / 2f);

            Vector3 randomSpawnPos = new Vector3(randomX, randomY+0.5f, randomZ);
            Vector3 spawnPos = transform.TransformPoint(randomSpawnPos);
            

            GameObject newPunchObj = Instantiate(punchObj, spawnPos, transform.rotation);
            newPunchObj.transform.SetParent(transform);
            Destroy(newPunchObj, 1f);
            //newPunchObj.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.Log("콜라이더 감지 안됨");
        }
    }


    /// <summary>
    /// 히트박스가 움직이고 목표위치에 도착하면 히트박스를 삭제시킼는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveAndDestroryCoroutine()
    {
        float moveDistance = 3f;                                         // 이동 거리
        float moveTime = 0.7f;                                             // 이동 시간
        Vector3 startPos = transform.position;                           // 시작 위치
        Vector3 targetPos = startPos + transform.forward * moveDistance; // 목표 위치

        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("물체에 닿았다.");
        }

    }
}
