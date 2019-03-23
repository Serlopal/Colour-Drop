using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private GameController gameController;
    public bool avoidCheckingColor = false;             // If true, no color checker is required.
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter( Collision other )
    {
        if ( other.gameObject.tag == "Player" ) {
            if ( ! avoidCheckingColor ) {
                Renderer playerRenderer = other.gameObject.GetComponent<Renderer>();
                Renderer thisRenderer = GetComponent<Renderer>();
                checkColorCollision( playerRenderer, thisRenderer );
            } else {
                gameController.endLevel();
            }
        }
    }

    /// <summary>
    /// Check collision by color. If player and item color matches you gain points
    /// and the collided item is destroyed. Otherwise, player dies.
    /// </summary>
    /// <param name="playerRenderer">Renderer - Player renderer component</param>
    /// <param name="thisRenderer">Renderer - Current gameObject renderer</param>
    /// <param name="toAdd">int - Score to add, if neccesary</param>
    private void checkColorCollision( Renderer playerRenderer, Renderer thisRenderer, int toAdd = 1 ) {
        if ( playerRenderer.sharedMaterial.name == thisRenderer.sharedMaterial.name ) {
            gameController.endLevel();
        } else {
            gameController.GameOver();
        }
    }
}
