using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    private GameController gameController;                      // GameController Script.
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find( "GameController" ).GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Start game button controller.
    /// </summary>
    public void startGameButtonController() {
        if ( gameController != null ) {
            gameController.LoadScene( "Level1" );
        }
    }

    /// <summary>
    /// Start tutorial levels button controller.
    /// </summary>
    public void startTutorialGameController() {
        if ( gameController != null ) {
            gameController.LoadScene( "TestLevel" );
        }
    }

    /// <summary>
    /// Exit game button controller.
    /// </summary>
    public void ExitGameController() {
        if ( gameController != null ) {
            gameController.LoadScene( "companySplashScreen" );
        }
    }
}
