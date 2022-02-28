using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public static GameController instance;
  public PlayerInput playerInput;
  private GameObject playerShip;
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
    InsertPlayerShipIntoScene();
    ResetScore();
    ResetGameOver();
  }

  // Update is called once per frame
  void Update()
  {
    if (gameOver && FirePressed)
    {
      ResetScene();
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
  void ResetScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  void InsertPlayerShipIntoScene()
  {
    if (playerShip == null)
    {
      playerShip = ObjectPool.instance.Get("Player");
      ConnectPlayerInputToPlayerShip();
    }

    playerShip.transform.position = Vector2.zero;
    playerShip.transform.rotation = Quaternion.identity;
    playerShip.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    playerShip.GetComponent<Rigidbody2D>().angularVelocity = 0;
    playerShip.SetActive(true);
  }

  void ResetScore()
  {
    score = 0;
    scoreText.text = "Score: " + score;
  }

  void ResetGameOver()
  {
    gameOver = false;
    gameOverText.SetActive(false);
  }

  void ConnectPlayerInputToPlayerShip()
  {
    InputAction fireAction = playerInput.actions["Fire"];
    InputAction rotateLeftAction = playerInput.actions["RotateLeft"];
    InputAction rotateRightAction = playerInput.actions["RotateRight"];
    InputAction thrustAction = playerInput.actions["Thrust"];

    fireAction.started += playerShip.GetComponent<PlayerShipController>().Input_Fire;
    fireAction.performed += playerShip.GetComponent<PlayerShipController>().Input_Fire;
    fireAction.canceled += playerShip.GetComponent<PlayerShipController>().Input_Fire;

    rotateLeftAction.started += playerShip.GetComponent<PlayerShipController>().Input_RotateLeft;
    rotateLeftAction.performed += playerShip.GetComponent<PlayerShipController>().Input_RotateLeft;
    rotateLeftAction.canceled += playerShip.GetComponent<PlayerShipController>().Input_RotateLeft;
    
    rotateRightAction.started += playerShip.GetComponent<PlayerShipController>().Input_RotateRight;
    rotateRightAction.performed += playerShip.GetComponent<PlayerShipController>().Input_RotateRight;
    rotateRightAction.canceled += playerShip.GetComponent<PlayerShipController>().Input_RotateRight;
    
    thrustAction.started += playerShip.GetComponent<PlayerShipController>().Input_Thrust;
    thrustAction.performed += playerShip.GetComponent<PlayerShipController>().Input_Thrust;
    thrustAction.canceled += playerShip.GetComponent<PlayerShipController>().Input_Thrust;
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
