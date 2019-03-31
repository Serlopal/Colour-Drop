using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject player;                                   // Player GameObject.
    public GameObject mainCamera;                               // Game Main Camera.
    public bool isPlain = false;                                // Check if the camera has to be in plain mode or in vertical mode.
    public int totalLevels = 15;                                // Total amount of levels. Used to build playable list of levels only when the main menu is trigger.
    public Material[] colors = new Material[3];                 // List of possible materials the player can swich to.
    public GameObject uiColors;                                 // Colors's list panel ui gameobject.
    public GameObject uiScore;                                 // Total current level score.
    public GameObject uiGameOverPanel;                          // GameOverPanel UI gameobject.
    public GameObject uiLevelCompletedPanel;                    // Level Completed Panle UI gameObject.
    public GameObject uiScoreLabel;                             // Label for Score gameObject in LevelCompletePanel
    public GameObject uiLevelScore;                             // Total Level Score in level completed gameObject.
    public GameObject uiTotalLabel;                             // Total score label gameobject in level completed panel.
    public GameObject uiTotalScore;                             // Total player score gameobject in level completed panel.
    private Vector3 active = new Vector3( 1f, 1f, 1f );         // Vector 3 to use with the active item in the player colors UI.
    private Vector3 inactive = new Vector3( 0.5f, 0.5f, 0.5f ); // Vector3 to use with inactive items in the player colors UI.
    private GameObject[] uiPlayerColors;                        // List of possible player colors in the UI.
    private int levelScoreCounter = 0;                          // Level score counter.

    Dictionary<string, string> playableScenes = new Dictionary<string, string>();   // Playable scenes where gameOver can be invoked.

    // Start is called before the first frame update
    void Start()
    {
        uiPlayerColors = GameObject.FindGameObjectsWithTag( "uiplayercolor" );

        // build playable scenes to invoke them in game over and other methods.
        buildPlayableScenes();

        // set PlayerPrefs if user is in the main menu.
        if ( SceneManager.GetActiveScene().name == "MainTitle" ) {
            setPlayerPrefs();
        }
    }

    // Update is called once per frame.
    void Update()
    {
        if ( Input.GetKeyDown( "space" ) && uiColors.active ) {
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
        string currentScene = SceneManager.GetActiveScene().name;

        /// check if gameOver is being called in a playable scene.
        if ( playableScenes.Count > 0 && playableScenes[ SceneManager.GetActiveScene().name ] == currentScene ) {
            if ( uiGameOverPanel != null ) {
                uiGameOverPanel.SetActive( true );
                Destroy(player);
            }
        }

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

    /// <summary>
    /// Build dictionary of playable scenes. This scenes are used to
    /// check in which scenes the game has access to level objects like
    /// the player or the game over UI.
    /// </summary>
    private void buildPlayableScenes() {
        string key;

        for( int i = 0; i < totalLevels; i++ ) {
            key = "Level" + ( i + 1 ).ToString();
            playableScenes.Add( key, key );
        }
        
        // debug level.
        playableScenes.Add( "TestLevel", "TestLevel" );   
    }

    /// <summary>
    /// Set PlayerPrefs when the user is in the main
    //  menu. PlayerPrefs will save total score trough the
    /// levels.
    /// </summary>
    public void setPlayerPrefs() {
        PlayerPrefs.SetInt( "TotalScore", 0 );
    }

    /// <summary>
    /// Displays completed level when the 
    /// player collides with the end level
    /// platform.
    /// </summary>
    public void endLevel() {
        if ( uiScore == null ) {
            return;
        }

        // freeze player to avoid controlling during popup.
        player.GetComponent<Player>().FreezePlayer();


        Text uiScoreText = uiScore.GetComponent<Text>();
        int score = int.Parse( uiScoreText.text );

        // set total score before animations.
        int totalScore = PlayerPrefs.GetInt( "TotalScore" );
        PlayerPrefs.SetInt( "TotalScore", totalScore + score );
        
        if ( uiScoreLabel != null && uiLevelScore != null ) {
            StartCoroutine( DisplayLevelScore( score, totalScore ) );
        }
    }


    /// <summary>
    /// Displays level score at completed level panel.
    /// </summary>
    /// <param name="score">int - Level's score</param>
    /// <param name="initTotalScore">int - Total score before adding the new score</param>
    public IEnumerator DisplayLevelScore( int score, int initTotalScore ) {
        uiLevelCompletedPanel.SetActive( true );

        yield return new WaitForSeconds( 1f );

        uiScoreLabel.SetActive( true );
        uiLevelScore.SetActive( true );

        yield return new WaitForSeconds( 1f );

        while ( levelScoreCounter < score ) {
            levelScoreCounter++;
            uiLevelScore.GetComponent<Text>().text = levelScoreCounter.ToString();
            yield return null;
        }

        if ( uiTotalLabel != null && uiTotalScore != null ) {
            int totalScore = PlayerPrefs.GetInt( "TotalScore" );
            StartCoroutine( DisplayTotalScore( totalScore, initTotalScore ) );
        } 
    }

    /// <summary>
    /// Displays total score at completed level panel.
    /// </summary>
    /// <param name="totalScore">int - total level score</param>
    /// <param name="initTotalScore">int - total score before adding the new score from this level</param>
    public IEnumerator DisplayTotalScore( int totalScore, int initTotalScore ) {
        // display current total score before the addition.
        uiTotalScore.GetComponent<Text>().text = initTotalScore.ToString();

        yield return new WaitForSeconds( 1f );

        uiTotalLabel.SetActive( true );
        uiTotalScore.SetActive( true );

        yield return new WaitForSeconds( 1f );

        if ( initTotalScore < totalScore ) {

            while( initTotalScore < totalScore ) {
                initTotalScore++;
                uiTotalScore.GetComponent<Text>().text = initTotalScore.ToString();
                yield return null;
            }
        } else {
            uiTotalScore.GetComponent<Text>().text = totalScore.ToString();
        }
    }

    /// <summary>
    /// Change mode controller.
    /// When the player enters the change mode área,
    /// the ball moves to the center of the circle whilist
    /// the camera goes to no-plain level position.
    /// After that is completed, gravity is enabled for the ball
    /// and the player gets the ball control again.
    /// </summary>
    /// <param name="changeParticles">GameObject change mode particles</param>
    public void ChangeModeController( GameObject changeParticles ) {
        if ( player ==  null || mainCamera == null ) {
            Debug.LogWarning( "Player and / or camera are not available" );
            return;
        }

        // move player to the center of the changeParticles.
        StartCoroutine( player.GetComponent<Player>().changePlayerMode( changeParticles ) );

        // set camera to non-plain levels position and rotation.
        // TODO: Call set camera coroutine here.
    }

 }


