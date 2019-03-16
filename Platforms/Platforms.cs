﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public bool isRotating = false;                     // Whether this platform rotates or not.
    public float rotationSpeed = 90f;                   // Base rotation speed.
    
    public enum Direction{Right, Left};                 // Which direction the platform should rotate around the game pivot.
    public Direction direction;                         // Direction pointer.

    private GameObject pivot;                            // Game pivot center. All GameObjects rotate around this pivot gameobject.


    // Start is called before the first frame update
    void Start()
    {
        pivot = GameObject.Find( "PivotPlayer" );
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
}
