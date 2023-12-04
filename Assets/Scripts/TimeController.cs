using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllers : MonoBehaviour
{
    public float DeltaTime = 0.0f;
    private Renderer rendererEje;
    // Start is called before the first frame update
    void Start()
    {
        rendererEje = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rendererEje.material.SetFloat("_DeltaTime",DeltaTime);
        DeltaTime += Time.deltaTime;

    }
}
