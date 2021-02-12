using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed = 0.1f;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;
    
    public float maxZoomDistance;
    public float minZoomDistance;

    public float dragSpeed = 50f;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        targetOrtho = Camera.main.orthographicSize;
    }

    void Update()
    {
        Move();
        Zoom();
        CameraDrag();
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.up * yInput + transform.right * xInput;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if(scrollInput != 0.0f)
        {
            targetOrtho -= scrollInput * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }

    public void focusOnPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    void CameraDrag()
    {
        if (Input.GetMouseButton(2))
        {
            float speed = dragSpeed * Time.deltaTime;
            Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
        }
    }
}
