using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelButtonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startButton;
    public string nextLevel;                // Next level scene namel;

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

    /// <summary>
    /// Restart level after the player is destroyed.
    /// </summary>
    public void restartLevel() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    /// <summary>
    /// Load next level once the player taps / clicks
    /// the Next Level button.
    /// </summary>
    public void loadNextLevel() {
        if ( nextLevel != null ) {
            SceneManager.LoadScene( nextLevel );
        }
    }
}
