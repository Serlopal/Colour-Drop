using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float initZ;                                // init Z axis position for player.

    // Start is called before the first frame update
    void Start()
    {
        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        fixBallPosition();
        resetRotation();
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
}
