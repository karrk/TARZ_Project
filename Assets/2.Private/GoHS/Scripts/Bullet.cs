using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float bulletSpeed;

    private void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;

        Destroy(gameObject, 2f);
    }
}
