using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void GameStart (){
        SceneManager.LoadScene("GameStateTest");
    }

    public void CloseGame(){
        Application.Quit();
    }
}
