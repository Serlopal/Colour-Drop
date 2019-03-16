using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject player;                                   // Player GameObject.

    public Material[] colors = new Material[3];                 // List of possible materials the player can swich to.
    private GameObject[] uiPlayerColors;                        // List of possible player colors in the UI.

    private Vector3 active = new Vector3( 1f, 1f, 1f );         // Vector 3 to use with the active item in the player colors UI.
    private Vector3 inactive = new Vector3( 0.5f, 0.5f, 0.5f ); // Vector3 to use with inactive items in the player colors UI.

    // Start is called before the first frame update
    void Start()
    {
        uiPlayerColors = GameObject.FindGameObjectsWithTag( "uiplayercolor" );
        
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
                // change active color in the UI.
                if ( uiPlayerColors != null ) {
                   SetUiPlayerColor( ColorUtility.ToHtmlStringRGB( playerRenderer.sharedMaterial.color) );
                }
                break;
            }
        }
    }

    /// <summary>
    /// Set UI color to match player color when the player's color changes
    /// </summary>
    /// <param name="hexaColor">string - Player's material color value in hexadecimal string</param>
    private void SetUiPlayerColor( string hexaColor ) {
        Debug.Log( "called" );
        
        for( int j = 0; j < uiPlayerColors.Length; j++ ) {
            RectTransform uiPlayerColorRectTransform = uiPlayerColors[j].GetComponent<RectTransform>();
            uiPlayerColorRectTransform.localScale = inactive;

            if ( ColorUtility.ToHtmlStringRGB( uiPlayerColors[j].GetComponent<Image>().color ) == hexaColor ) {
                uiPlayerColorRectTransform.localScale = active;
            }
        }
    }
 }
