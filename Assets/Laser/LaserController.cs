using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidsNamespace;

public class LaserController : MonoBehaviour
{
  private Rigidbody2D rb;
  public Vector2 shipVelocity;
  public float speed = 5f;
  public float maxTimeAlive = 3f;
  private float timeAlive = 0f;
  private bool velocityInitialized = false;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    gameObject.tag = Tags.Laser;
  }

  // Update is called once per frame
  void Update()
  {
    if (!velocityInitialized)
    {
      Vector2 velocityTransform = transform.TransformVector(Vector2.up * speed);
      rb.velocity = shipVelocity + velocityTransform;
      velocityInitialized = true;
    }

    timeAlive += Time.deltaTime;

    if (timeAlive > maxTimeAlive)
    {
      gameObject.SetActive(false);
    }
  }

  void OnDisable()
  {
    timeAlive = 0;
    velocityInitialized = false;
  }
}
