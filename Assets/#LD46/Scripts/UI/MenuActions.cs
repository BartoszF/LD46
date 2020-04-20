using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuActions : MonoBehaviour
{
    public void exitGame() {
        Application.Quit();
    }

    public void reloadGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
