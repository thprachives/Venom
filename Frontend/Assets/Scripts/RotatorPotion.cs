using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPotion : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, and Z axes by specified amounts, adjusted for frame rate.
        transform.Rotate(new Vector3(0,-45, 0) * Time.deltaTime);
    }
    
}
