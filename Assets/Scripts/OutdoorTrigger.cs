using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Keeps track of which players are inside and outside the base.
/// Has public properties for checking how many players in inside and outside
/// </summary>
public class OutdoorTrigger : MonoBehaviour
{
    private enum PlayerStatus
    {
        INSIDE_BASE,
        OUTSIDE_BASE
    }
    private Dictionary<GameObject, PlayerStatus> PlayerStatuses;

    public int NumberOfPlayersInsideTheBase => PlayerStatuses.Count(status => status.Value == PlayerStatus.INSIDE_BASE);
    public int NumberOfPlayersOutsideTheBase => PlayerStatuses.Count(status => status.Value == PlayerStatus.OUTSIDE_BASE);

    void Start()
    {
        PlayerStatuses = new Dictionary<GameObject, PlayerStatus>();
        if (GetComponent<Collider>() == null || GetComponent<Collider>().isTrigger == false)
        {
            throw new MissingComponentException("A collider that is a trigger is required");
        }

        foreach (Renderer render in GetComponents<Renderer>())
        {
            render.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (PlayerStatuses.ContainsKey(other.gameObject) == false)
            {
                PlayerStatuses.Add(other.gameObject, PlayerStatus.OUTSIDE_BASE);
            }
            else
            {
                PlayerStatuses[other.gameObject] = PlayerStatus.OUTSIDE_BASE;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (PlayerStatuses.ContainsKey(other.gameObject) == false)
            {
                PlayerStatuses.Add(other.gameObject, PlayerStatus.INSIDE_BASE);
            }
            else
            {
                PlayerStatuses[other.gameObject] = PlayerStatus.INSIDE_BASE;
            }

            // when this trigger gets exited, that means a player entered the base
            GameStateManager.Instance.OnBaseEntered();
        }
    }
}
