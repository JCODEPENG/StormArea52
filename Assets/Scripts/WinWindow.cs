using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles behavior for the window that appears when the game is won
/// </summary>
public class WinWindow : MonoBehaviour
{
    private Canvas Canvas;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GetComponent<Canvas>();
        if (Canvas == null)
        {
            throw new MissingComponentException("WinWindow must be attached to a canvas");
        }

        // hide window when game starts or is reset
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.BEFORE_ENTERING_BASE, HideWindow);
        // show window when game is won
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.GAME_OVER_WIN, ShowWinResultsWindow);
    }

    private void ShowWinResultsWindow()
    {
        Canvas.enabled = true;

        // todo: update UI elements to show relevant game end info like score, etc
    }

    private void HideWindow()
    {
        Canvas.enabled = false;
    }
}
