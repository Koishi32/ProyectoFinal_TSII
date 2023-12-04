using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public List<GameObject> information;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entro al trigger");
        if(other.gameObject.tag == "Player")
        {
            foreach(GameObject flag in information)
                flag.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("salio del trigger");
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject flag in information)
                flag.SetActive(false);
        }
    }
}
