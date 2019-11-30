using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages sounds and visual effect for when the alarm goes off
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Image))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private Color RedColor = new Color(0.5f, 0f, 0f, 0.3f);
    [SerializeField] private float ColorCycleRate = 1f;

    private AudioSource AudioSource;
    private Image Image;

    private float timeAccumulator = 0f;
    private Coroutine AlarmColorCycleCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        if (AudioSource == null)
        {
            throw new MissingComponentException("AudioSource required");
        }
        Image = GetComponent<Image>();
        if (Image == null)
        {
            throw new MissingComponentException("Image required");
        }

        // sound the alarm when the game is lost
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.GAME_OVER_LOSE, ActivateAlarm);

        // stop the alarm when the game is reset
        GameStateManager.Instance.RegisterOnStateChange(GameStateManager.GameStates.BEFORE_ENTERING_BASE, DeactivateAlarm);
    }

    public void ActivateAlarm()
    {
        AudioSource.Play();
        AlarmColorCycleCoroutine = StartCoroutine(DoColorCycle());
    }

    private IEnumerator DoColorCycle()
    {
        Image.enabled = true;
        Color transparentColor = new Color(RedColor.r, RedColor.g, RedColor.b, 0f);
        while (true)
        {
            Image.color = Color.Lerp(
                transparentColor,
                RedColor,
                0.5f * (1f + Mathf.Sin(timeAccumulator * ColorCycleRate * Mathf.PI * 2))
            );
            timeAccumulator += Time.deltaTime;
            yield return null;
        }
    }

    public void DeactivateAlarm()
    {
        if (AlarmColorCycleCoroutine != null)
        {
            StopCoroutine(AlarmColorCycleCoroutine);
        }
        AudioSource.Stop();
        Image.enabled = false;
    }
}
