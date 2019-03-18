using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject player;                                   // Player GameObject.

    public Material[] colors = new Material[3];                 // List of possible materials the player can swich to.
    private GameObject[] uiPlayerColors;                        // List of possible player colors in the UI.
    public GameObject uiScore;                                 // Total current level score.

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
        
        for( int j = 0; j < uiPlayerColors.Length; j++ ) {
            RectTransform uiPlayerColorRectTransform = uiPlayerColors[j].GetComponent<RectTransform>();
            uiPlayerColorRectTransform.localScale = inactive;

            if ( ColorUtility.ToHtmlStringRGB( uiPlayerColors[j].GetComponent<Image>().color ) == hexaColor ) {
                uiPlayerColorRectTransform.localScale = active;
            }
        }
    }

    /// <summary>
    /// GameOver method. Called when the player dies.
    /// </summary>
    public void GameOver() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    /// <summary>
    /// Update level score.
    /// </summary>
    /// <param name="toAdd">int - Amount to add. Default to 1</param>
    public void UpdateScore( int toAdd = 1 ) {
        if ( uiScore == null ) {
            return;
        }

        Text uiScoreText = uiScore.GetComponent<Text>();
        int score = int.Parse( uiScoreText.text );
        uiScoreText.text = ( score + toAdd ).ToString();
    }

    /// <summary>
    /// Load scene utility
    /// </summary>
    /// <param name="sceneName">string - Scene to be loaded. Must be added in build settings.</param>
    public void LoadScene( string sceneName ) {
        SceneManager.LoadScene( sceneName );
    }
 }
