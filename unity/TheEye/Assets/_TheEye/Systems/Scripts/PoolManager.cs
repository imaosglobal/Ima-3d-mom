using UnityEngine;

/// <summary>
/// PoolManager - Object Pooling למעוף
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private int initialPoolSize = 20;

    private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Queue<GameObject>> pools;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        pools = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Queue<GameObject>>();
    }

    /// <summary>
    /// יצירת pool חדש
    /// </summary>
    public void CreatePool(string poolName, GameObject prefab, int size)
    {
        if (pools.ContainsKey(poolName))
        {
            Debug.LogWarning($"[PoolManager] Pool '{poolName}' already exists!");
            return;
        }

        var pool = new System.Collections.Generic.Queue<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        pools.Add(poolName, pool);
        Debug.Log($"[PoolManager] Created pool '{poolName}' with {size} objects");
    }

    /// <summary>
    /// קבל אובייקט מ-pool
    /// </summary>
    public GameObject GetFromPool(string poolName, Vector3 position)
    {
        if (!pools.ContainsKey(poolName))
        {
            Debug.LogWarning($"[PoolManager] Pool '{poolName}' does not exist!");
            return null;
        }

        var pool = pools[poolName];
        if (pool.Count == 0)
        {
            Debug.LogWarning($"[PoolManager] Pool '{poolName}' is empty!");
            return null;
        }

        GameObject obj = pool.Dequeue();
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// החזר אובייקט ל-pool
    /// </summary>
    public void ReturnToPool(string poolName, GameObject obj)
    {
        if (!pools.ContainsKey(poolName))
        {
            Debug.LogWarning($"[PoolManager] Pool '{poolName}' does not exist!");
            return;
        }

        obj.SetActive(false);
        pools[poolName].Enqueue(obj);
    }

    public int GetPoolSize(string poolName)
    {
        return pools.ContainsKey(poolName) ? pools[poolName].Count : 0;
    }
}
