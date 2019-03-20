using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public bool isRotating = false;                             // Whether this platform rotates or not.
    public float rotationSpeed = 90f;                           // Base rotation speed.
    
    public enum Direction{Right, Left};                         // Which direction the platform should rotate around the game pivot.
    public Direction direction;                                 // Direction pointer.

    private GameController gameController;                      // GameController script from GameController gameObject.
    private GameObject pivot;                                   // Game pivot center. All GameObjects rotate around this pivot gameobject.


    // Start is called before the first frame update
    void Start()
    {
        pivot = GameObject.Find( "PivotPlayer" );
        gameController = GameObject.Find( "GameController" ).GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( isRotating && pivot != null ) {
            rotatePlatform( direction.ToString() );
        }
    }

    /// <summary>
    /// Rotate this platform around.
    /// </summary>
    /// <param name="direction">String - Direction to rotate the platform</param>
    private void rotatePlatform( string direction = "Rigth" ) {
        Vector3 pivorPosition = pivot.transform.position;
        Vector3 axis = new Vector3( 0, 1, 0);
        float speed = rotationSpeed * Time.deltaTime;

        if ( direction == "Left" ) {
            speed = - ( speed );
        }

        transform.RotateAround( pivorPosition, axis, speed );
    }

    /// <summary>
    /// Check collision by color. If player and item color matches you gain points
    /// and the collided item is destroyed. Otherwise, player dies.
    /// </summary>
    /// <param name="playerRenderer">Renderer - Player renderer component</param>
    /// <param name="thisRenderer">Renderer - Current gameObject renderer</param>
    /// <param name="toAdd">int - Score to add, if neccesary</param>
    private void checkColorCollision( Renderer playerRenderer, Renderer thisRenderer, int toAdd = 1 ) {
        if ( playerRenderer.sharedMaterial.name == thisRenderer.sharedMaterial.name ) {
            gameController.UpdateScore( toAdd );
            Destroy( gameObject );
        } else {
            gameController.GameOver();
        }
    }

    /// <summary>
    /// Collisions controller for any gameobject that can be collided by the player.
    /// </summary>
    /// <param name="other">Collision GameObject collided by this item</param>
    void OnCollisionEnter(Collision other)
    {
        if ( other.gameObject.tag == "Player" ) {
            Renderer playerRenderer = other.gameObject.GetComponent<Renderer>();
            Renderer thisRenderer = GetComponent<Renderer>();
            
            // collision checker for platforms.
            if (gameObject.tag == "platform" ) {
                checkColorCollision( playerRenderer, thisRenderer, 2 );
            }

            // collision checker for coins.
            if (gameObject.tag == "coin" ) {
                checkColorCollision( playerRenderer, thisRenderer, 1 );
            }

        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter( Collider other )
    {
        if ( other.tag == "Player" ) {
            Renderer playerRenderer = other.GetComponent<Renderer>();
            Renderer thisRenderer = GetComponent<Renderer>();
            
            // collision checker for platforms.
            if (gameObject.tag == "platform" ) {
                checkColorCollision( playerRenderer, thisRenderer, 2 );
            }

            // collision checker for coins.
            if (gameObject.tag == "coin" ) {
                checkColorCollision( playerRenderer, thisRenderer, 1 );
            }

        }
    }
}
