using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
  public GameObject asteroidPrefab;
  public float spawnInterval = 5f;
  public float timeSinceLastSpawn = 0f;

  private float minRotation = -60f;
  private float maxRotation = 60f;
  private float minSpeed = 2f;
  private float maxSpeed = 5f;
  private Vector2 screenLeftBottom;
  private Vector2 screenRightTop;

  private static string[] spawnSides = new string[] { "top", "bottom", "left", "right" };

  void Start()
  {
    SetScreenEdges();
  }

  // Update is called once per frame
  void Update()
  {
    if (!GameController.instance.gameOver)
    {
      timeSinceLastSpawn += Time.deltaTime;

      if (timeSinceLastSpawn >= spawnInterval)
      {
        SpawnRandomAsteroid();
        timeSinceLastSpawn = 0f;
      }
    }
  }

  private void SetScreenEdges()
  {
    if (!Camera.main)
    {
      Debug.LogError("Make sure you have a camara tagged with 'MainCamara'");
      return;
    }

    if (!Camera.main.orthographic)
    {
      Debug.LogError("MainCamara must be orthographic");
      return;
    }

    Camera main = Camera.main;
    // lets get camara edges
    screenLeftBottom = main.ScreenToWorldPoint(new Vector3(0, 0, main.nearClipPlane));
    screenRightTop = main.ScreenToWorldPoint(new Vector3(main.pixelWidth, main.pixelHeight, main.nearClipPlane));
  }

  void SpawnRandomAsteroid()
  {
    Vector2 position = GenerateRandomPosition();

    while (IsColliderHere(position))
    {
      position = GenerateRandomPosition();
    }

    GameObject newAsteroid = ObjectPool.instance.Get(AsteroidsNamespace.Tags.Asteroid);
    if (newAsteroid != null)
    {
      newAsteroid.SetActive(true);
      Rigidbody2D rb2d = newAsteroid.GetComponent<Rigidbody2D>();
      newAsteroid.transform.position = position;
      rb2d.velocity = GenerateRandomVelocity();
      rb2d.angularVelocity = GenerateRandomAngularVelocity();
    }
  }

  private Vector2 GenerateRandomPosition()
  {
    string spawnSide = spawnSides[Random.Range(0, spawnSides.Length - 1)];
    switch (spawnSide)
    {
      case "top":
        return new Vector2(Random.Range(screenLeftBottom.x, screenRightTop.x), screenRightTop.y);
      case "bottom":
        return new Vector2(Random.Range(screenLeftBottom.x, screenRightTop.x), screenLeftBottom.y);
      case "left":
        return new Vector2(screenLeftBottom.x, Random.Range(screenLeftBottom.y, screenRightTop.y));
      case "right":
        return new Vector2(screenRightTop.x, Random.Range(screenLeftBottom.y, screenRightTop.y));
      default:
        return new Vector2(Random.Range(screenLeftBottom.x, screenRightTop.x), Random.Range(screenLeftBottom.y, screenRightTop.y));
    }
  }

  private bool IsColliderHere(Vector2 position)
  {
    return Physics2D.OverlapCircle(position, 1.5f) != null;
  }

  private Vector2 GenerateRandomVelocity()
  {
    return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(minSpeed, maxSpeed);
  }

  private float GenerateRandomAngularVelocity()
  {
    return Random.Range(minRotation, maxRotation);
  }
}
