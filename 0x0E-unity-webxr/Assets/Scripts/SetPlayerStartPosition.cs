using UnityEngine;

public class SetPlayerStartPosition : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;

    void Start()
    {
        // Set the player's position and rotation to match the startPosition Transform
        if (player != null && startPosition != null)
        {
            player.position = startPosition.position;
            player.rotation = startPosition.rotation;
        }
    }
}
