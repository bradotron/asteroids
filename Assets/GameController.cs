using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public static GameController instance;
  public GameObject playerShipPrefab;
  public bool gameOver;
  public Text scoreText;
  private int score = 0;

  // Awake precedes Start, so we need this to be active before Start
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

  // Start is called before the first frame update
  void Start()
  {
    scoreText.text = "Score: " + score;
    //gameOver = true;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void PlayerDied()
  {
    gameOver = true;
  }

  public void PlayerScored()
  {
    if (!gameOver)
    {
      score++;
      scoreText.text = "Score: " + score;
    }
  }
}
