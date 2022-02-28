using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTextController : MonoBehaviour
{
  public Text DebugText;
  public GameObject playerShip;
  private Rigidbody2D rbPlayerShip;
  // Start is called before the first frame update
  void Start()
  {
    if (playerShip != null)
    {
      rbPlayerShip = playerShip.GetComponent<Rigidbody2D>();
      DebugText.text = GetFormatedDebugText();
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (rbPlayerShip != null)
    {
      DebugText.text = GetFormatedDebugText();
    }
  }

  private string GetFormatedDebugText()
  {
    string formattedText = "";

    float velocity = rbPlayerShip.velocity.magnitude;
    formattedText += $"Speed: {velocity.ToString("0.00")}" + Environment.NewLine;

    Vector2 position = rbPlayerShip.position;
    formattedText += $"Position: {position.ToString()}" + Environment.NewLine;

    int laserCount = GameObject.FindGameObjectsWithTag(AsteroidsNamespace.Tags.Laser).Length;
    formattedText += $"Lasers: {laserCount}" + Environment.NewLine;

    return formattedText;
  }
}
