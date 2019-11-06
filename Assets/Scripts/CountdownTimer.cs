using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not finished, need a visualization for the timer (ex. text)
// countdown timer for the stage
// once the timer reaches 0.0 sec activate fail state

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float GameTime = 5000;

    private float timer;
    public bool FailState = false;
    // sets the timer to the timer set in unity
    void Start()
    {
        timer = GameTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0.0f && FailState == false)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
            FailState = true;
        }
    }
}
