using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private GameObject prefab;
    private Pool<EnemyBehaviour> pool;
    [SerializeField] private Transform target;

    private void Start()
    {
        pool = new(gameObject.transform, prefab, 10);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            // Time can be scaled, for absolutely real seconds: WaitForSecondsRealTime().
            EnemyBehaviour enemy = pool.Get();
            enemy.sp = this;
            enemy.GetComponent<NavMeshAgent>().SetDestination(target.position);
        }
    }

    public void Kill(EnemyBehaviour enemy)
    {
        // because SpawnPoint is access point to pool.
        pool.Add(enemy);
    }

}