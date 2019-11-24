using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatScreen : MonoBehaviour
{
    public GameObject ToBeFloat;
    void Start()
    {
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.GAME_OVER_LOSE, load_scene);
    }

    private void load_scene(){
        ToBeFloat.SetActive(true);
    }
}