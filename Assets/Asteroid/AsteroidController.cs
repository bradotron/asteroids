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

      float explosionScale = Random.Range(0.1f, 0.3f);

      GameObject explosion = ObjectPool.instance.Get("Explosion");
      explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
      explosion.transform.position = transform.position;
      explosion.SetActive(true);
      explosion.GetComponent<ParticleSystem>().Play();

      GameController.instance.PlayerScored();
    }
  }
}
