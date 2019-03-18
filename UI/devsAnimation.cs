using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class devsAnimation : MonoBehaviour
{

    public GameObject[] textItems = new GameObject[4];                  // items to animate.
    public float timeDuration = 1f;                                     // Fade in / out time duration.
    public float delayInit = 1f;                                        // Time to delay the animation init.
    public float totalDuration = 9f;                                    // Total time the scene is loaded for the player. Once this time is pass, the next scene is loaded.
    public float translationSpeed = 1f;                                 // Speed used by the text to move.

    private Text text;                                                  // Text component to work with.
    private GameController gameController;                              // Gamecontroller class used to control game events.
    private Vector3 direction;                                          // direction where the text is moved.

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find( "GameController" ).GetComponent<GameController>();

        if ( textItems != null && textItems.Length > 0 ) {
            initAnimations();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( textItems != null && textItems.Length > 0 ) {
            moveTexts();
        }
    }

    /// <summary>
    /// Move text in the X axis.
    /// </summary>
    private void moveTexts() {
        for ( int i = 0; i < textItems.Length; i++ ) {
            if ( textItems[i] != null ) {
                direction = ( i % 2 == 0 ) ? Vector3.right : Vector3.left;
                textItems[i].transform.Translate( direction * translationSpeed * Time.deltaTime );
            }
        }
    }

    /// <summary>
    /// Fade In Text component
    /// </summary>
    /// <param name="time">Float time animation duration</param>
    /// <param name="text">GameObject Text component</param>
    /// <param name="delay">Float time before the fade starts</param>
    public IEnumerator FadeTextToFullAlpha( float time, Text text, float delay = 0 ) {
        // delay fade if required.
        yield return new WaitForSeconds( delay );

        text.color = new Color( text.color.r, text.color.g, text.color.b, 0 );
        while ( text.color.a < 1.0f ) {
            text.color = new Color( text.color.r, text.color.g, text.color.b, text.color.a + ( Time.deltaTime / time ) );
            yield return null;
        }
    }

    /// <summary>
    /// Fade Out Text component
    /// </summary>
    /// <param name="time">Float time animation duration</param>
    /// <param name="text">GameObject Text component</param>
    /// <param name="delay">Float time before the fade starts</param>
    public IEnumerator FadeTexToZeroAlpha( float time, Text text, float delay = 0 ) {
        // delay fade if required.
        yield return new WaitForSeconds( delay );

        text.color = new Color( text.color.r, text.color.g, text.color.b, 1 );
        while ( text.color.a > 0.0f ) {
            text.color = new Color( text.color.r, text.color.g, text.color.b, text.color.a - ( Time.deltaTime / time ) );
            yield return null;
        }
    }

    /// <summary>
    /// Loads the next scene. Uses GameController class to do so, 
    /// </summary>
    /// <param name="delay">Float - Time to delay changing scenes</param>
    public IEnumerator LoadMainMenuScreen( float delay = 0 ) {
        yield return new WaitForSeconds( delay );
        gameController.LoadScene( "Level1" );
    }

    /// <summary>
    /// Init all set of animations in the dev screen.
    /// </summary>
    private void initAnimations() {
        for ( int j = 0; j < textItems.Length; j++ ) {
            // float toDelay = ( j > 0 ) ? ( (float) j + 1f ) : delayInit;
            text = textItems[j].GetComponent<Text>();
            
            if ( text != null ) {
                // fade in titles.
                StartCoroutine( FadeTextToFullAlpha( timeDuration, text, delayInit + (float) j ) );

                // fade put titles.
                StartCoroutine( FadeTexToZeroAlpha( timeDuration, text, delayInit + ( (float) j + 3f ) ) );
            }
        }

        // load main menu screen.
        if ( gameController != null ) {
            StartCoroutine( LoadMainMenuScreen( 10f) );
        }
    }
}
