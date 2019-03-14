using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float initZ;                                      // init Z axis position for player.  
    public float rotationSpeed = 10f;                         // player speed movement.

    public GameObject pivot;                                  // player's pivot gameobject.

    public bool canMove = true;                               // whether the player can move or not.
    public bool isMoving;                                     // whether the player is moving.
    
    private GameObject mainCamera;                            // main camera gameobject - used here to tell the camera when needs to rotate to keep the player focused in the current scene.
    

    // Start is called before the first frame update
    void Start()
    {
        initZ = transform.position.z;
        //canMove = true;
        //isMoving = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //fixBallPosition();
        // resetRotation();
        mainCamera = GameObject.FindGameObjectWithTag( "MainCamera" );

        
        if ( canMove && pivot != null && isMoving ) {
            movePlayer( 1 );
            // detectMouseInputForPlayer();
        } else {
            // resetRotation();
            // fixBallPosition();
        }
        
        /**
        if ( Input.GetMouseButton(0) && canMove && pivot != null ) {
            isMoving = true;
            // detectMouseInputForPlayer();
        } else {
            isMoving = false;
            // resetRotation();
            // fixBallPosition();
        }
        */

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
    /// <param name="direction">Int - Direction to rotate the ball around - 1 is positive ( right ), left is negative ( left )
    /// 0 will not move the player.
    /// </param>
    public void movePlayer( int direction = 0 ) {
        Vector3 pivotPosition = pivot.transform.position;
        Vector3 axis = new Vector3( 0, 1, 0 );
        float speed = rotationSpeed * Time.deltaTime;

        // set move left or right.
        if ( direction < 0 ) {
            speed = - ( speed );
        }

        if ( ! canMove ) {
            rotationSpeed = 0;
        }

        transform.RotateAround( pivotPosition, axis, speed );
        if( mainCamera != null ) {
            mainCamera.GetComponent<CameraBehaviour>().SetCameraRotation( pivotPosition, axis, speed );
        }
    }

    /// <summary>
    /// Check user input for mouse / tapping for controlling player.
    /// </summary>
    private void detectMouseInputForPlayer() {
        int direction = ( Input.mousePosition.x >= transform.position.x ) ? 1 : - 1;
        movePlayer(direction);
    }
}
