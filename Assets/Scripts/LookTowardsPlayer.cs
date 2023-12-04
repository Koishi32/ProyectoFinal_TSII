using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward,
            player.transform.rotation * Vector3.up);
    }
}
