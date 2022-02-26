using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipController : MonoBehaviour
{
  private Rigidbody2D rb;
  private bool isFiring;
  public float fireCooldown = 3f;
  private float fireCooldownRemaining = 0f;
  public GameObject laserPrefab;
  public GameObject laserEmitter;
  private Animator laserEmitterAnimator;
  public float angularAcceleration = 180f;
  public float maxAngularVelocity = 180f;
  public float maxVelocity = 5f;
  public float acceleration = 10f;
  private bool isRotatingLeft;
  private bool isRotatingRight;
  private bool isThrusting;
  public GameObject thruster;
  private Animator thrusterAnimator;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    SetupEngineThruster();
    SetupLaserEmitter();
  }

  void SetupEngineThruster()
  {
    thrusterAnimator = thruster.GetComponent<Animator>();
  }

  void SetupLaserEmitter()
  {
    laserEmitterAnimator = laserEmitter.GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    HandleFiring();
  }

  void FixedUpdate()
  {
    HandleRotation();
    HandleThrust();
  }

  private void RotateLeft(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      isRotatingLeft = true;
    }
    if (context.canceled)
    {
      isRotatingLeft = false;
    }
  }

  private void RotateRight(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      isRotatingRight = true;
    }
    if (context.canceled)
    {
      isRotatingRight = false;
    }
  }

  private void Thrust(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      isThrusting = true;
    }
    if (context.canceled)
    {
      isThrusting = false;
    }
  }

  private void Fire(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      isFiring = true;
    }
    if (context.canceled)
    {
      isFiring = false;
    }
  }

  private void HandleFiring()
  {
    if (fireCooldownRemaining > 0f)
    {
      fireCooldownRemaining -= Time.deltaTime;
    }

    if (isFiring && fireCooldownRemaining <= 0f)
    {
      GameObject laserInstance = Instantiate(laserPrefab, transform.TransformPoint(Vector2.zero), transform.rotation);
      laserInstance.GetComponent<LaserController>().shipVelocity = rb.velocity;
      fireCooldownRemaining = fireCooldown;
      laserEmitterAnimator.SetTrigger("Fire");
    }
  }

  private void HandleRotation()
  {
    if (!(isRotatingLeft && isRotatingRight))
    {
      float deltaAngularVelocity = 0f;

      if (isRotatingLeft)
      {
        deltaAngularVelocity = angularAcceleration * Time.deltaTime;
      }

      if (isRotatingRight)
      {
        deltaAngularVelocity = -angularAcceleration * Time.deltaTime;
      }

      rb.angularVelocity = Mathf.Clamp(rb.angularVelocity + deltaAngularVelocity, -maxAngularVelocity, maxAngularVelocity);
    }
  }

  private void HandleThrust()
  {
    if (isThrusting)
    {
      if (!thrusterAnimator.GetBool("IsThrusting"))
      {
        thrusterAnimator.SetBool("IsThrusting", true);
        thrusterAnimator.SetBool("IsStopped", false);
      }

      Vector2 thrustForce = transform.up * rb.mass * acceleration;

      float thrustInVelocityDirection = Vector2.Dot(thrustForce, rb.velocity.normalized);

      if (thrustInVelocityDirection > 0)
      {
        float thrustFalloffFactor = Mathf.Clamp(1 - ((maxVelocity - rb.velocity.magnitude) / maxVelocity), 0f, 1f);
        Vector2 adjustedThrustForce = thrustForce - (thrustInVelocityDirection * rb.velocity.normalized * thrustFalloffFactor);
        rb.AddForce(adjustedThrustForce);
      }
      else
      {
        rb.AddForce(thrustForce);
      }
    }
    else
    {
      if (thrusterAnimator.GetBool("IsThrusting"))
      {
        thrusterAnimator.SetBool("IsThrusting", false);
        thrusterAnimator.SetBool("IsStopped", true);
      }
    }
  }
}
