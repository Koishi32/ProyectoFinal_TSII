using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f, mouseSpeed = 1f;
    private bool submerged = false, onShip = false;
    private Vector3 translation;
    public Light Dlight;
    public GameObject changeButton;
    private Rigidbody playerRB;
    private Scene actualScene;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Prueba");
        Movement();
        if(onShip && Input.GetKeyDown(KeyCode.X))
        {
            actualScene = SceneManager.GetActiveScene();
            if (actualScene.buildIndex == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(actualScene.buildIndex + 1);
            }
        }
    }

    void Movement()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector3 mousePosition = new Vector3(-Input.GetAxis("Mouse Y") * mouseSpeed, Input.GetAxis("Mouse X") * mouseSpeed, 0);
        Vector3 rot = transform.localEulerAngles;
        rot.x += mousePosition.x;
        rot.y += mousePosition.y;
        if (submerged)
        {
            translation = new Vector3(movX * speed, 0, movY * speed);
            Dlight.color = Color.cyan;
            playerRB.useGravity = false;
        }
        else
        {
            translation = new Vector3(movX * speed, 0, movY * speed);
            Dlight.color = new Color(1, 0.9557064f, 0.8349056f, 1);
            playerRB.useGravity = true;
        }
        transform.localEulerAngles = rot;
        transform.Translate(translation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            submerged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            submerged = false;
        }else
        {
            if (other.gameObject.tag == "Barco")
            {
                onShip = false;
                changeButton.SetActive(false);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Barco")
        {
            onShip = true;
            changeButton.SetActive(true);
        }
    }
}
