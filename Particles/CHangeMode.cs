using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHangeMode : MonoBehaviour
{
    private GameController gameController;              // GameController script.

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find( "GameController" ).GetComponent<GameController>();    
    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player" && gameController != null ) {
            gameController.ChangeModeController( this.gameObject );
        }
    }
}
