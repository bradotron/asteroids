using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPoolItem
{
  public string tag;
  public GameObject objectPrefab;
  public int poolSize;
  public bool isExpandable;
  public List<GameObject> pool;
}

public class ObjectPool : MonoBehaviour
{
  public static ObjectPool instance;
  public List<ObjectPoolItem> itemsToPool;
  private static readonly Dictionary<string, ObjectPoolItem> Pool = new Dictionary<string, ObjectPoolItem>();

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
      Pool.Add(item.tag, item);
      item.pool = new List<GameObject>();
      for (int i = 0; i < item.poolSize; i++)
      {
        GameObject obj = (GameObject)Instantiate(item.objectPrefab);
        obj.SetActive(false);
        item.pool.Add(obj);
      }
    }
  }

  public GameObject Get(string tag)
  {
    ObjectPoolItem item;
    if (Pool.TryGetValue(tag, out item))
    {
      GameObject obj = item.pool.Find(item => !item.activeInHierarchy);
      if (obj == null && item.isExpandable)
      {
        obj = (GameObject)Instantiate(item.objectPrefab);
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
