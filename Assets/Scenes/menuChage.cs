using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuChage : MonoBehaviour
{
   public  void LoadScene1() {
        SceneManager.LoadScene(1);
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadScenebyIndex(int i)
    {
        SceneManager.LoadScene(i);
    }
}
