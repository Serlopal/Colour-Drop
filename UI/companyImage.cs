using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class companyImage : MonoBehaviour
{
    public GameObject theImageObject;                       // company gameobject image.
    public float timeDuration = 1f;                         // Fade in / out time duration.
    public float delayInit = 1f;                            // Time to delay the animation init.
    public float totalDuration = 9f;                        // Total time the scene is loaded for the player. When this time passes, the next scene is loaded.

    private Image image;                                    // Image component to work with.
    private GameController gameController;                  // GameController - used to load scenes.

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        image = theImageObject.GetComponent<Image>();

        // fade in title.
        StartCoroutine( FadeTextToFullAlpha( timeDuration, image, delayInit ) );

        // fade out title
        StartCoroutine( FadeTexToZeroAlpha( timeDuration, image, delayInit + 3f ) );

        if ( gameController != null ) {
            // load devs scene.
            StartCoroutine( LoadDevsScreen( 7f ) );
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Fade Image component
    /// </summary>
    /// <param name="time">Float time animation duration</param>
    /// <param name="text">GameObject Text component</param>
    /// <param name="delay">Float time before the fade starts</param>
    public IEnumerator FadeTextToFullAlpha( float time, Image image, float delay = 0 ) {
        // delay fade if required.
        yield return new WaitForSeconds( delay );

        image.color = new Color( image.color.r, image.color.g, image.color.b, 0 );
        while ( image.color.a < 1.0f ) {
            image.color = new Color( image.color.r, image.color.g, image.color.b, image.color.a + ( Time.deltaTime / time ) );
            yield return null;
        }
    }

    /// <summary>
    /// Fade Out Image component
    /// </summary>
    /// <param name="time">Float time animation duration</param>
    /// <param name="text">GameObject Text component</param>
    /// <param name="delay">Float time before the fade starts</param>
    public IEnumerator FadeTexToZeroAlpha( float time, Image image, float delay = 0 ) {
        // delay fade if required.
        yield return new WaitForSeconds( delay );

        image.color = new Color( image.color.r, image.color.g, image.color.b, 1 );
        while ( image.color.a > 0.0f ) {
            image.color = new Color( image.color.r, image.color.g, image.color.b, image.color.a - ( Time.deltaTime / time ) );
            yield return null;
        }
    }

    /// <summary>
    /// Loads the next scene. Uses GameController class to do so, 
    /// </summary>
    /// <param name="delay">Float - Time to delay changing scenes</param>
    public IEnumerator LoadDevsScreen( float delay = 0 ) {
        yield return new WaitForSeconds( delay );
        gameController.LoadScene( "devsSplashScreen" );
    }
    
}
