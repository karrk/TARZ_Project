using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float rotationSpeed = 360f; // 회전 속도

    public float health;

    private void Update()
    {
        // 입력 받기
        float horizontal = Input.GetAxisRaw("LeftStickX"); // 좌우 이동 (A, D)
        float vertical = Input.GetAxisRaw("LeftStickY"); // 앞뒤 이동 (W, S)

        // 이동 처리
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized; // 정규화된 이동 방향

        if (movement.magnitude > 0.01f)
        {
            // 이동
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            // 이동 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"플레이어 체력: {health}");
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
