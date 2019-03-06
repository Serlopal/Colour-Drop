using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float initZ;                                      // init Z axis position for player.  
    public float rotationSpeed = 10f;                         // player speed movement.

    public GameObject pivot;                                  // player's pivot gameobject.

    private bool canMove;                                      // whether the player can move or not.

    // Start is called before the first frame update
    void Start()
    {
        initZ = transform.position.z;
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        //fixBallPosition();
        // resetRotation();

        if ( canMove && pivot != null ) {
            Debug.Log( "here" );
            movePlayer( 1 );
        } else {
            resetRotation();
            fixBallPosition();
        }
    }

    /// <summary>
    /// Adjust the ball to keep falling in the same direction.
    /// <summary>
    public void fixBallPosition() {
        transform.position = new Vector3( transform.position.x, transform.position.y, initZ );
    }

    /// <summary>
    /// Reset the ball rotation. Rotate adds a force that we want
    /// to avoid, so the ball does not rotate.
    /// </summary>
    private void resetRotation() {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3( 0, 0, 0 );
        transform.rotation = q;
    }

    /// <summary>
    /// Player movement. This method moves the ball 
    /// rotating around the cilindre pivot in the center
    /// of the scene.
    /// </summary>
    /// <param name="direction">int - Direction to rotate the ball around - 1 is positive ( right ), left is negative ( left )
    /// 0 will not move the player.
    /// </param>
    public void movePlayer( int direction = 0 ) {
        float speed = rotationSpeed * Time.deltaTime;
        if ( direction < 0 ) {
            speed = - ( speed );
        }

        if ( ! canMove ) {
            rotationSpeed = 0;
        }

        Debug.Log( pivot.transform.rotation );

        transform.RotateAround( pivot.transform.position, new Vector3( 0, 1, 0 ), speed );
    }
}
