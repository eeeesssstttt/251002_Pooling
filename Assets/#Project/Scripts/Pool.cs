using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
where T : IPoolClient
{
    private Transform anchor;
    private GameObject prefab;
    private Queue<T> queue = new();
    private int batch;
    public Pool(Transform anchor, GameObject prefab, int batch)
    {
        this.anchor = anchor;
        this.prefab = prefab;
        // throw exception if batch >= 0 ("batch must be superior to 0")
        CreateBatch();
    }

    private void CreateBatch()
    {
        for (int _ = 0; _ < batch; _++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            if (go.TryGetComponent(out T client))
            {
                Add(client);
            }
            else
            {
                throw new ArgumentException("Prefab does not have an IPoolClient component");
            }
        }
    }

    public void Add(T client)
    {
        queue.Enqueue(client);
        client.Fall();
    }

    public T Get()
    {
        if (queue.Count == 0) CreateBatch();
        T client = queue.Dequeue();
        client.Rise(anchor.position, anchor.rotation);
        return client;
    }
}
