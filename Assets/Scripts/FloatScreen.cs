using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatScreen : MonoBehaviour
{
    [SerializeField] private GameObject ToBeFloat = null;
    public GameStateManager.GameStates a;
    void Start()
    {
        GameStateManager.Instance.RegisterOnStateChange(a, load_scene);
    }

    private void load_scene(){
        ToBeFloat.SetActive(true);
    }
}