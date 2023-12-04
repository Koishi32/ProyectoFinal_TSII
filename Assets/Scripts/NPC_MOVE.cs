using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MOVE : MonoBehaviour
{
    public List<Transform> destinations;
    int current_dest;
    public float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float dist;
        if (current_dest == 0)
            dist = Vector3.Distance(destinations[0].position, transform.position);
        else
        {
            dist = Vector3.Distance(destinations[1].position, transform.position);
        }

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, destinations[current_dest].position, step);

        if(dist < 0.85)
        {
            if (current_dest == 0)
            {
                current_dest = 1;
                transform.localEulerAngles = new Vector3(0, -transform.localEulerAngles.y, 0);
            }
            else
            {
                current_dest = 0;
                transform.localEulerAngles = new Vector3(0, -transform.localEulerAngles.y, 0);
            }
        }

    }
}
