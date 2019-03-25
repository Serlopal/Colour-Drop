using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlatforms : MonoBehaviour
{
    public bool rotateItself = false;                   // Wheter this platform rotetes over itself.
    public enum RotateAxis{x, y};
    public RotateAxis rotateAxis;                       // axis to rotate over itself.
    public float speed = 50f;                           // rotation speed.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( rotateItself ) {
            RotateOverItself();
        }
    }

    /// <summary>
    /// Rotate over itself.
    /// </summary>
    private void RotateOverItself() {
        Vector3 rotation = ( rotateAxis.ToString() == "x" ) ? new Vector3( speed * Time.deltaTime, 0, 0 ) : new Vector3( 0, speed * Time.deltaTime, 0 );
        transform.Rotate( rotation );
    }

}
