using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == AsteroidsNamespace.Tags.Laser)
    {
      collision.gameObject.SetActive(false);
      gameObject.SetActive(false);
      GameController.instance.PlayerScored();
    }
  }
}
