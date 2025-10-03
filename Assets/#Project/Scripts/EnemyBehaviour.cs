using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IPoolClient
{
    [HideInInspector] public SpawnPoint sp;
    // should be private variable with accessor;
    public void Rise(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, rotation);
    }

    public void Fall()
    {
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        sp.Kill(this);
    }
}