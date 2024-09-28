using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Control : MonoBehaviour
{
    private GameManager gameManager;
    public int speed_mod = 50;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager")
        .GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            //gameManager.MoveLeft(1);
            Debug.Log("A key was pressed");
            transform.Translate(Vector3.left * Time.deltaTime * speed_mod);
            //speed_mod += 1;
        }
        

        if (Input.GetKey(KeyCode.D))
        {
            //gameManager.MoveRight(1);
            Debug.Log("d key was pressed");
            transform.Translate(Vector3.right * Time.deltaTime * speed_mod);
            //speed_mod += 1;
        }
        //transform.Translate(Vector3.right * Time.deltaTime * speedmod);

      

    }
}
