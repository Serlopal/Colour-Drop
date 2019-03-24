using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBlock : MonoBehaviour
{
    
    public enum Orientation{ Right, Left };     // collision orientation.
    public Orientation orientation;
    private GameObject player;                  // player gameObject.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = GameObject.Find( "Player" );
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //CheckCollisionOrientation();  
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" ) {

            // avoid player and camera to continue rotating through the solid block.
            GameObject player = other.gameObject;

            if ( player != null ) {
                player.GetComponent<Player>().canMove = false;
            }
        }
    }


    /// <summary>
    /// Check which way the player can continue moving the ball after
    /// colliding with the solid block.
    /// </summary>
    /// <param name="player">Player's gameObject</param>
    private void CheckCollisionOrientation( GameObject player ) {
        int mouseButton = ( orientation.ToString() == "Right" ) ? 1 : 0;

        if ( Input.GetMouseButton( mouseButton ) && player != null ) {
            player.GetComponent<Player>().canMove = true;
        }  
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if ( other.tag == "Player" ) {
            CheckCollisionOrientation( other.gameObject );
        }
    }
}
