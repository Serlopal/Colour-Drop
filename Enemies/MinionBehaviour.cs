using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehaviour : MonoBehaviour
{
    public float rotationSpeed = 90f;                           // Base rotation speed.
    private GameObject parent;                                  // Enemy body gameObject.

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        animateMinion();
    }

    /// <summary>
    /// Animate minion. Minions rotate around the enemy
    /// gameObject and around themselves in both X and Y axis.
    /// </summary>
    private void animateMinion() {
        float speed = rotationSpeed * Time.deltaTime;

        // rotate around parent.
        if ( parent != null ) {
            Vector3 parentPosition = parent.transform.position;
            Vector3 axis = new Vector3( 0, 1, 0 );

            transform.RotateAround( parentPosition, axis, speed ); 
        }

        // rotate around itself.
        transform.Rotate( speed, speed, 0 );
    }
}
