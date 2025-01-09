using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerMobSpawner : MonsterSpawner
{
    protected BoxCollider coll;

    protected override void Start()
    {
        coll = GetComponent<BoxCollider>();
        coll.isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        if (coll == null)
            coll = GetComponent<BoxCollider>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + coll.center, coll.size);
    }

    protected virtual async void OnTriggerEnter(Collider other)
    {
        coll.enabled = false;
        await Spawn();
    }
}
