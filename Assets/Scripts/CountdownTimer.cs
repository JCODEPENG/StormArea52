using UnityEngine;
using UnityEngine.UI;
using System;

// countdown timer for the stage
// once the timer reaches 0.0 sec activate fail state

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float GameTime = 0.0f;
    public Text num ;
    public Image circle;

    private float timer;
    private string twodig;
    private float percentage;
    private float total_time;
    private Color newcolor;

    public bool FailState = false;
    public bool TimerActivated = false;
    
    // sets the timer to the timer set in unity
    void Start()
    {
        ResetTimer();

        // when game is reset, restart the timer
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.BEFORE_ENTERING_BASE, ResetTimer);

        // start the timer when the base is entered
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.BASE_ENTERED, () => TimerActivated = true);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerActivated)
        {
            if (timer >= 0.0f && FailState == false)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0.0f;
                FailState = true;
                GameStateManager.Instance.OnTimeUp();
                TimerActivated = false;
            }
            twodig = String.Format("{0:0.00}", timer);
            num.text = twodig;
            //image of timer
            percentage = timer / total_time;
            circle.fillAmount = percentage;
            //color of timer
            if ( timer <= 5f && FailState == false){
            newcolor.g -= Time.deltaTime ;
            newcolor.b = newcolor.g;
            circle.color = newcolor;}   
        }
    }

    private void ResetTimer()
    {
        TimerActivated = false;
        timer = GameTime;
        FailState = false;
        twodig = String.Format("{0:0.00}", timer);
        num.text = twodig;
        //image of timer
        total_time = GameTime;
        percentage = 1;
        newcolor = Color.white;
    }
}
