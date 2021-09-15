using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManagement : MonoBehaviour
{
    void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}