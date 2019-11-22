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
        }
    }

    private void ResetTimer()
    {
        TimerActivated = false;
        timer = GameTime;
        FailState = false;
        twodig = String.Format("{0:0.00}", timer);
        num.text = twodig;
    }
}
