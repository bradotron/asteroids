using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public static GameController instance;
  public GameObject playerShip;
  public GameObject gameOverText;
  public bool gameOver;
  private bool FirePressed;
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
    ResetGame();
  }

  // Update is called once per frame
  void Update()
  {
    if (gameOver && FirePressed)
    {
      ResetGame();
    }
  }
  public void Input_Fire(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      FirePressed = true;
    }
    if (context.canceled)
    {
      FirePressed = false;
    }
  }
  void ResetGame()
  {
    gameOver = false;
    gameOverText.SetActive(false);
    score = 0;
    scoreText.text = "Score: " + score;

    // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    playerShip.transform.position = Vector2.zero;
    playerShip.transform.rotation = Quaternion.identity;
    playerShip.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    playerShip.GetComponent<Rigidbody2D>().angularVelocity = 0;
    playerShip.SetActive(true);
  }

  public void PlayerDied()
  {
    gameOver = true;
    gameOverText.SetActive(true);
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
