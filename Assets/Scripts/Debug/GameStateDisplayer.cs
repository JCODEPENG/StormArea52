using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Area52.Debug
{
    /// <summary>
    /// Shows the game state from GameManager
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class GameStateDisplayer : MonoBehaviour
    {
        private Text TextElement;

        void Start()
        {
            // iterate through all the GameStates enum values to add a listener for each
            foreach (var GameState in (GameStateManager.GameStates[])Enum.GetValues(typeof(GameStateManager.GameStates)))
            {
                GameStateManager.Instance.RegisterOnStateChange(
                    GameState,
                    () => ShowUIText(GameState.ToString()) // action to run when the game state changes
                );
            }

            TextElement = GetComponent<Text>();
            if (TextElement == null)
            {
                throw new MissingReferenceException("Text UI element required");
            }
        }

        private void ShowUIText(string message)
        {
            TextElement.text = "Game state: " + message;
        }
    }
}
