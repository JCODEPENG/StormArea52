using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartWithController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
