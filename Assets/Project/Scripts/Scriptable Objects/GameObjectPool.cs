using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Object Pool", order = 23)]
public class GameObjectPool : ScriptableObject
{
    public GameObject prefab;

    public int amount;

    public bool canExpand;

    private Queue<GameObject> spawnedObjects;

    private Transform parent;

    public void SpawnPool()
    {
        if(spawnedObjects == null || spawnedObjects.Count == 0) spawnedObjects = new Queue<GameObject>();
        
        if (spawnedObjects.Count >= amount) return;

        if(!parent) parent = new GameObject(name).transform;

        while (spawnedObjects.Count < amount)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            spawnedObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject(Vector2 position, Quaternion rotation)
    {
        if (spawnedObjects == null || spawnedObjects.Count == 0)
        {
            SpawnPool();
            Debug.LogWarning($"{name} spawned mid-game.");
        }

        GameObject obj = spawnedObjects.Dequeue();
        spawnedObjects.Enqueue(obj);

        if (obj.activeSelf && canExpand)
        {
            obj = Instantiate(prefab, parent);
            spawnedObjects.Enqueue(obj);
        }  

        obj.SetActive(false);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }
}
