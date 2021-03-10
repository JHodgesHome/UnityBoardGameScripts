using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine;


public class RoomPlacement : MonoBehaviour
{
    //TODO: Change this code to handle the scriptable objects instead
    //Look into binding a scriptableObject to each prefab
    //Enable and disable the various prefabs using SetActive()
    //Use OnEnable(), and OnDisable() methods to handle SetActive() changes
    //May need to use a bool value to determine when the room is in placement mode, or has been placed fully, so that no further code acts upon it
    RaycastHit hitInfo;
    Vector3 movePoint;
    GameObject[] prefabs;
    bool isPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        int fileCount = 0;
        Debug.Log("Name of current object: " + gameObject.name);
        //TileMap map = new TileMap();
        GameObject map = GameObject.Find("Map");
        TileMap tileMap = map.GetComponent<TileMap>();
        Debug.Log("Gateway position " + tileMap.placementGatewayX + " " + tileMap.placementGatewayY);
        Debug.Log("Gateway Facing: " + tileMap.placementGatewayFacing);

        string[] roomSplit = gameObject.name.Split('_');
        string baseRoom = roomSplit[0];
        string orientation = roomSplit[1];

        Debug.Log("baseRoom = " + baseRoom);
        Debug.Log("orientation = " + orientation);


        string facing = tileMap.placementGatewayFacing.ToString();
        var resourcesPath = System.IO.Path.Combine(Application.dataPath, "Resources/Prefabs");
        DirectoryInfo di = new DirectoryInfo(resourcesPath);
        Debug.Log("filepath = " + resourcesPath);
        Debug.Log("facing = " + facing);

        FileInfo[] files = di.GetFiles(baseRoom + "_*" + facing + "*.prefab");

        prefabs = new GameObject[files.Length];

        for(int i = 0; i < files.Length; i++)
        {
            //Debug.Log("prefab: " + files[i].Name);
            //Debug.Log("prefab path: " + files[i].FullName);
            //Debug.Log("resources path: " + "Prefabs/" + files[i].Name);
            string roomName = Path.GetFileNameWithoutExtension(files[i].Name);

            Debug.Log("Resource to load = " + "Prefabs/" + roomName);
            prefabs[i] = Resources.Load<GameObject>("Prefabs/" + roomName);
        }

        //foreach (var file in di.GetFiles(baseRoom + "_*" + facing + "*.prefab"))
        //{
        //    Debug.Log("prefab: " + file.Name);
        //    Debug.Log("prefab path: " + file.FullName);
        //    Debug.Log("resources path: " + "Prefabs/" + file.Name);
        //    string[] filenameSplit = file.Name.Split('.');
        //    //string roomName = filenameSplit[0];
        //    string roomName = Path.GetFileNameWithoutExtension(file.Name);
        //    Debug.Log("filename without extension: " + "Prefabs/" + roomName);
        //    Debug.Log("fileCount = " + fileCount);
        //    prefabs[fileCount] = Resources.Load<GameObject>("Prefabs/" + roomName);


        //    fileCount++;
        //    if (fileCount == file.Length)
        //        fileCount = 0;
        //}

        Debug.Log("Number of prefabs loaded = " + prefabs.Length);

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
            foreach(Transform child in transform)
            {
                Debug.Log("Tilename: " + child.name);
            }
            //Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //TODO: Instatiate next prefab in prefabs array and position it correctly so that it lines up with the correct gate
            //Will likely need to call the TileMap class again and use methods from there
            //May need to edit these methods to accept parameters from this class to function correctly
            //Can potentially use the Gameobject position and size, as well as gateway positions and facing direction

            //transform.RotateAround(gameObject.transform.position, Vector3.forward, 90);
            //Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }        
    }
}
