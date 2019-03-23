using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelButtonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startButton;
    public string nextLevel;                // Next level scene namel;
    private GameController gameController;  // GameController script component.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameController = GameObject.Find( "GameController").GetComponent<GameController>();   
    }

    /// <summary>
    /// Start level enabling player gravity and player movement,
    /// so the ball starts falling if is not a plain level.
    /// </summary>
    public void startLevel() {
        if ( player != null && startButton != null && gameController != null ) {

            if ( ! gameController.isPlain ) {
                player.GetComponent<Rigidbody>().useGravity = true;
            }

            player.GetComponent<Player>().canMove = true;
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
