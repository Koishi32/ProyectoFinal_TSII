using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float cyclesPerSecond;
    float deggresToRotate;
    //360 / N deggres to be spinned
    // Update is called once per frame
    void Update()
    {
        deggresToRotate = 360*cyclesPerSecond;
        //deggresToRotate = deggresToRotate;
        transform.Rotate(0,0,deggresToRotate*Time.deltaTime);
        Debug.Log(deggresToRotate);
    }
}
