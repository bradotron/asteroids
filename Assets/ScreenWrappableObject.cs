using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrappableObject : MonoBehaviour
{
  private Vector2 screenLeftBottom;
  private Vector2 screenRightTop;

  // Start is called before the first frame update
  void Start()
  {
    SetScreenEdges();
  }

  // Update is called once per frame
  void Update()
  {
    HandleScreenWrap();
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

  private void HandleScreenWrap()
  {
    // screen wrapping
    Vector2 newPosition = transform.position;
    if (transform.position.y > screenRightTop.y)
    {
      newPosition.y = screenLeftBottom.y;
    }
    if (transform.position.y < screenLeftBottom.y)
    {
      newPosition.y = screenRightTop.y;
    }
    if (transform.position.x > screenRightTop.x)
    {
      newPosition.x = screenLeftBottom.x;
    }
    if (transform.position.x < screenLeftBottom.x)
    {
      newPosition.x = screenRightTop.x;
    }
    transform.position = newPosition;
  }
}
