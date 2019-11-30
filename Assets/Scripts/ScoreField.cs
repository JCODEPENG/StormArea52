using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreField : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = "Score: " + FindObjectOfType<OutdoorTrigger>().CalculatedScore;
    }
}
