using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;                                  // player gameobject.
    private Player playerScript;                                // player's logic script component.
    public float distanceFromPlayer = 0;                       // distance where the camera is located from player.
    public float distanceFromTop = - 8f;                       // used to show more screen below the ball when is failling, so the player can see the obstacles with ease.
    public float xAdjustement = - 15f;                         // used to correct the camera's X axis when the camera distance to the player changes.

    public float[] initCameraPosition = new float[3];       // init camera position values.
    public float[] initCameraRotation = new float[3];       // init camera rotation values.

    public GameObject pivot;                                  // player's pivot gameobject.
    private Rigidbody playerRigibody;                         // player's Rigibody component.

    // Start is called before the first frame update
    void Start()
    {
        if ( player != null ) {
            playerScript = player.GetComponent<Player>();
            playerRigibody = player.GetComponent<Rigidbody>();
            // InitUpdateCameraPosition();
            initCameraPositionRotation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( player != null ) {
            // adjustCameraOnYAxis();
        }
        adjustCameraOnYAxis( player );
    }

    /// <summary>
    /// Initializes camera position when the level starts and points to
    /// player during the current gameplay
    /// </summary>
    private void InitUpdateCameraPosition() {
        transform.position = new Vector3( player.transform.position.x - xAdjustement, player.transform.position.y - distanceFromTop, player.transform.position.z + distanceFromPlayer );
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
        transform.RotateAround( pivot.transform.position, axis, speed );
    }

    /// <summary>
    /// Initialise camera rotation and position
    /// </summary>
    public void initCameraPositionRotation() {
        if ( initCameraPosition.Length == 3 ) {
            transform.position = new Vector3( initCameraPosition[0], initCameraPosition[1], initCameraPosition[2] );
        }


        if ( initCameraRotation.Length == 3 ) {
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3( initCameraRotation[0], initCameraRotation[1], initCameraRotation[2] );
            transform.rotation = q;
        }
    }

    /// <summary>
    /// Adjust camera Y position to match player.
    /// We ensure the camera is always at the same position that
    /// the player when it falls and bounces.
    /// </summary>
    public void adjustCameraOnYAxis(GameObject player) {
        float toAdjust = transform.position.y - player.transform.position.y;
        float toUpdate = player.transform.position.y + toAdjust;

        Debug.Log( playerRigibody.useGravity );
        Debug.Log( playerRigibody.velocity );
        
        transform.position = new Vector3( transform.position.x, player.transform.position.y + toAdjust, transform.position.z );
    }
}
