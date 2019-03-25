using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationSpeed = 10f;                         // player speed movement.

    public GameObject pivot;                                  // player's pivot gameobject.

    public bool canMove = false;                               // whether the player can move or not.
    public bool isMoving;                                     // whether the player is moving.
    public float maxVelocity;                                 // max velocity allowed for the ball when failing.
    private float initZ;                                    // init Z axis position for player.  
    
    private GameObject mainCamera;                            // main camera gameobject - used here to tell the camera when needs to rotate to keep the player focused in the current scene.
    private int direction;                                    // in which direction the player is moving;
    private Rigidbody rigibody;                               // Player Rigibody component.
    

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Init CanMove.
        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = GameObject.FindGameObjectWithTag( "MainCamera" );
        rigibody = GetComponent<Rigidbody>();

        if ( Input.GetMouseButton(0) && canMove ) {
            direction = 1;
            movePlayer( direction );
        }

        if ( Input.GetMouseButton(1) && canMove ) {
            direction = - 1;
            movePlayer( direction );
        }

        if ( rigibody != null ) {
            CheckMaxVelocity();
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
    public void resetRotation() {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3( 0, 0, 0 );
        transform.rotation = q;
    }

    /// <summary>
    /// Stop player rotation. Used when colliding
    /// with solid blocks.
    /// </summary>
    public void stopRotation() {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3( q.x, q.y, q.z );
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

        if ( ! canMove || direction == 0 ) {
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

    /// <summary>
    /// Set max falling velocity
    /// </summary>
    private void CheckMaxVelocity() {
        if ( rigibody.velocity.sqrMagnitude > maxVelocity ) {
            rigibody.velocity *= 0.99f;
        }
    }

    /// <summary>
    /// Freeze player movement - used on gameover
    /// and end level popups.
    /// </summary>
    public void FreezePlayer() {
        canMove = false;
        rigibody.useGravity = false;
        rigibody.velocity = Vector3.zero;
        rigibody.angularVelocity = Vector3.zero;
    }
}
