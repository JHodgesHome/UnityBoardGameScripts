using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_buttons : MonoBehaviour
{
	public GameObject MenuPanel;
	public GameObject LevelSelectPanel;
    // Start is called before the first frame update
    void Start()
    {
     	MenuPanel.SetActive(true);
     	LevelSelectPanel.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelPanel() 
    {
    	MenuPanel.SetActive(false);
    	LevelSelectPanel.SetActive(true);
    }

    public void ShowMenuPanel()
    {
    	MenuPanel.SetActive(true);
    	LevelSelectPanel.SetActive(false);
    }
}
