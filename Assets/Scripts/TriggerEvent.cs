using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows a UnityEvent to be bound to OnTriggerEnter of a trigger
/// </summary>
[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
    [Header("Trigger settings")]
    [SerializeField] private UnityEvent OnTriggerEnterEvent = default;
    [Tooltip("The tag a gameobject has to have to trigger the event. Leave blank for any tag")]
    [SerializeField] private string RequiredTagToActivate = null;
    [SerializeField] private bool OnlyTriggerOnce = true;

    [Header("Debug options")]
    [SerializeField] private bool ShowRenderersInGame = false;

    private void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            throw new MissingComponentException("TriggerAction is missing Collider");
        }
        if (GetComponent<Collider>().isTrigger == false)
        {
            throw new System.InvalidOperationException("TriggerAction collider must be a trigger");
        }

        if (ShowRenderersInGame == false)
        {
            foreach (Renderer render in GetComponents<Renderer>())
            {
                Destroy(render);
            }
        }
        else
        {
            Debug.LogWarning("A TriggerEvent is showing it's renderer. Remember to disable ShowRenderersInGame in production");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // trigger the event if no required tag is specified, or the object that entered the trigger has the required tag
        if (RequiredTagToActivate == null || RequiredTagToActivate.Length < 1 || other.CompareTag(RequiredTagToActivate))
        {
            OnTriggerEnterEvent.Invoke();
            if (OnlyTriggerOnce)
            {
                this.enabled = false;
            }
        }
    }
}
