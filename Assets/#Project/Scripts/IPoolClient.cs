using UnityEngine;

public interface IPoolClient
{
    void Rise(Vector3 position, Quaternion rotation);

    void Fall();
}
