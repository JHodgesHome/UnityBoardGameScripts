using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacement : MonoBehaviour
{
    RaycastHit hitInfo;
    Vector3 movePoint;
    // Start is called before the first frame update
    void Start()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hitInfo, 50000.0f, (1 << 8)))
        //{
        //    transform.position = hitInfo.point;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    Vector3 gridPoint = hitInfo.point;
        //    float gridSize = 1f;

        //    gridPoint.x = Mathf.Round(gridPoint.x / gridSize) * gridSize;
        //    gridPoint.y = Mathf.Round(gridPoint.y / gridSize) * gridSize;
        //    gridPoint.z = Mathf.Round(gridPoint.z / gridSize) * gridSize;

        //    transform.position = gridPoint;
        //    //gameObject.transform.position = gridPoint;
        //    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            transform.RotateAround(gameObject.transform.position, Vector3.forward, 90);
        }        
    }
}
