using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewGame() {
        Debug.Log("새 게임");
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickLoadGame() {
        Debug.Log("로드");
    }

    public void OnClickOption() {
        Debug.Log("옵션");
    }

    public void OnClickExit() {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
