using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startGame : MonoBehaviour
{
    public string loadTarget;
    public void LoadLevel()
    {
        SceneManager.LoadScene(loadTarget); //https://www.youtube.com/watch?v=NRUk7YzXyhE credit
    }
}