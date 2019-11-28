using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatScreen : MonoBehaviour
{
    [SerializeField] private GameObject ToBeFloat = null;
    [SerializeField] private GameStateManager.GameStates ChangingState;
    void Start()
    {
        GameStateManager.Instance.RegisterOnStateChange(ChangingState, load_scene);
    }

    private void load_scene(){
        ToBeFloat.SetActive(true);
    }
}
