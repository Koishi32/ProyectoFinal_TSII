using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSimulation : MonoBehaviour
{
    private Vector3 x, v, a, f;
    public float   m = 1.0f;
    public Vector3 gravity = new Vector3(0.0f, -9.81f, 0.0f);
    public float K = 10.0f;

    public List<GameObject> connectedParticles;

    // Start is called before the first frame update
    void Start()
    {
        x = new Vector3(0.0f, 0.0f, 0.0f);
        v = new Vector3(0.0f, 0.0f, 0.0f);
        a = new Vector3(0.0f, 0.0f, 0.0f);
        f = new Vector3(0.0f, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        f = m * gravity - K * x;
        a = f / m;
        v += a * Time.deltaTime;
        x += v * Time.deltaTime;

        transform.position = x;

        Debug.DrawLine(new Vector3(0.0f, 0.0f, 0.0f), x, Color.green);

    }
}
