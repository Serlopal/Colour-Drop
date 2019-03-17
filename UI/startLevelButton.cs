using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startLevelButton : MonoBehaviour
{
    public GameObject player;
    public GameObject startButton;

    /// <summary>
    /// Start level enabling player gravity,
    /// so the ball starts falling.
    /// </summary>
    public void startLevel() {
        if ( player != null && startButton != null ) {
            player.GetComponent<Rigidbody>().useGravity = true;
            Destroy( startButton );
        }
    }
}
