using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPoolItem
{
  public GameObject objectToPool;
  public int poolSize;
  public bool isExpandable;
  public List<GameObject> pool;
}

public class ObjectPool : MonoBehaviour
{
  public static ObjectPool instance;
  public List<ObjectPoolItem> itemsToPool;
  private static readonly Dictionary<Type, ObjectPoolItem> Pool = new Dictionary<Type, ObjectPoolItem>();

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    foreach (ObjectPoolItem item in itemsToPool)
    {
      Pool.Add(item.objectToPool.GetType(), item);
      item.pool = new List<GameObject>();
      for (int i = 0; i < item.poolSize; i++)
      {
        GameObject obj = (GameObject)Instantiate(item.objectToPool);
        obj.SetActive(false);
        item.pool.Add(obj);
      }
    }
  }

  public GameObject Get(Type type)
  {
    ObjectPoolItem item;
    if (Pool.TryGetValue(type, out item))
    {
      GameObject obj = item.pool.Find(item => !item.activeInHierarchy);
      if (obj == null && item.isExpandable)
      {
        obj = (GameObject)Instantiate(item.objectToPool);
        obj.SetActive(false);
        item.pool.Add(obj);
        return obj;
      }
      else
      {
        return obj;
      }
    }
    return null;
  }
}
