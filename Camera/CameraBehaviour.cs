using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    public float distanceFromPlayer = 0;
    public float distanceFromTop = - 8f;
    public float xAdjustement = - 15f;

    // Start is called before the first frame update
    void Start()
    {
        if ( player != null ) {
            InitCameraPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( player != null ) {
            InitCameraPosition();
        }
    }

    /// <summary>
    /// Initializes camera position when the level starts
    /// </summary>
    private void InitCameraPosition() {
        transform.position = new Vector3( player.transform.position.x - xAdjustement, player.transform.position.y - distanceFromTop, player.transform.position.z + distanceFromPlayer );
    }
}
