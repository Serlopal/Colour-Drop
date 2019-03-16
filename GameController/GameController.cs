using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject player;                          // Player GameObject.

    public Material[] colors = new Material[3];        // List of possible materials the player can swich to.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown( "space" ) ) {
            ChangePlayerColor();
        }
    }

    /// <summary>
    /// Utility to get current player material name;
    /// </summary>
    public string GetPlayerMaterialName() {
        if ( player != null )
            return player.GetComponent<Renderer>().material.name;
        else {
            return "null";
        }
    }

    /// <summary>
    /// Changes player material, so color is changed.
    /// </summary>
    public void ChangePlayerColor() {
        Renderer playerRenderer = player.GetComponent<Renderer>();

        for( int i = 0; i < colors.Length; i++ ) {
            if ( playerRenderer.sharedMaterial.name == colors[i].name ) {
                // if player has the last material, we assign the first one in the array.
                if ( ( colors.Length - 1 ) == i ) {  
                    playerRenderer.material = colors[0];
                } else {
                    playerRenderer.material = colors[i + 1];
                }
                break;
            }
        }
    }
 }
