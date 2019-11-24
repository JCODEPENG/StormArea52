using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.GAME_OVER_LOSE, load_scene);
    }

    public void load_scene(){
        load = GameObject FailScreen;
        load.SetActive(true);
    }
}
