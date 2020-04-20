using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame() {
        SceneManager.LoadScene("main");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
