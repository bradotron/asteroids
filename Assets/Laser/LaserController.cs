using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AsteroidsNamespace;

public class LaserController : MonoBehaviour
{
  private Rigidbody2D rb;
  public Vector2 shipVelocity;
  public float speed = 5f;
  public float maxTimeAlive = 3f;
  private float timeAlive = 0f;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    Vector2 velocityTransform = transform.TransformVector(Vector2.up * speed);
    rb.velocity = shipVelocity + velocityTransform;
    gameObject.tag = Tags.Laser;
  }

  // Update is called once per frame
  void Update()
  {
    timeAlive += Time.deltaTime;

    if (timeAlive > maxTimeAlive)
    {
      DestroyMe();
    }
  }

  void OnBecameInvisible()
  {
    DestroyMe();
  }

  void DestroyMe()
  {
    Destroy(gameObject);
  }
}
