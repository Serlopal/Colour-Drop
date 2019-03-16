using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    // rotation speed.
    public float RotationSpeed = 120f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCoin();
    }

    /// <summary>
    /// Rotate coin continuosly.
    /// </summary>
    private void RotateCoin() {
        transform.Rotate( 0, RotationSpeed * Time.deltaTime, 0 );
    }
}
