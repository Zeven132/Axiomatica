using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run_Equation : MonoBehaviour
{
    public Button Execute;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
   .GetComponent<GameManager>();
    }
    void OnMouseDown()
    { gameManager.Mathfunc(1, 3, false, gameManager.eqIndex[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
