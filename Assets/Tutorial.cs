using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    
    private GameManager gameManager;
    public int TutorialNumber;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
        .GetComponent<GameManager>();
    }

    public void OnMouseDown()
    {
        //gameManager.TutorialDesc = TutorialNumber;
        Debug.Log("Tutorial mouse enter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
