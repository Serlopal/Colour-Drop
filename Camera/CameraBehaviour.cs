using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;                                  // player gameobject.
    public float distanceFromPlayer = 0;                       // distance where the camera is located from player.
    public float distanceFromTop = - 8f;                       // used to show more screen below the ball when is failling, so the player can see the obstacles with ease.
    public float xAdjustement = - 15f;                         // used to correct the camera's X axis when the camera distance to the player changes.

    public GameObject pivot;                                  // player's pivot gameobject.

    // Start is called before the first frame update
    void Start()
    {
        if ( player != null ) {
            InitUpdateCameraPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( player != null ) {
            InitUpdateCameraPosition();
        }
    }

    /// <summary>
    /// Initializes camera position when the level starts and points to
    /// player during the current gameplay
    /// </summary>
    private void InitUpdateCameraPosition() {
        transform.position = new Vector3( player.transform.position.x - xAdjustement, player.transform.position.y - distanceFromTop, player.transform.position.z + distanceFromPlayer );
        // transform.LookAt( player.transform );
    }

    /// <summary>
    /// Set camera rotation when the player in moving.
    /// So we ensure the camera faces the player whent the 
    /// player is moving.
    ///
    /// This method is called from Player Script every time
    /// the player moves.
    /// </summary>
    /// <param name="pivotPosition">Vector3 Player's pivot position - used to keep the same rotation as player has.</param>
    /// <param name="axis">Vector3 Axis to rotate around</param>
    /// <param name="speed">Rotation speed</param>
    public void SetCameraRotation( Vector3 pivotPosition, Vector3 axis, float speed ) {
        /*
        transform.RotateAround( pivotPosition, axis, speed );
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3( 0, speed, 0 );
        transform.rotation = q;
        */
        
    }
}
