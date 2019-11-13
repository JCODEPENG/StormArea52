using UnityEngine;
using UnityEngine.UI;
using System;

// countdown timer for the stage
// once the timer reaches 0.0 sec activate fail state

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float GameTime = 0.0f;
    public Text num ;

    private float timer;
    private string twodig;
    public bool FailState = false;
    // sets the timer to the timer set in unity
    void Start()
    {
        timer = GameTime; 
        twodig = String.Format("{0:0.00}", timer);
        num.text = twodig;
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
        twodig = String.Format("{0:0.00}", timer);
        num.text = twodig;
    }
}
