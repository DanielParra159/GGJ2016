using UnityEngine;
using System.Collections;

public class change_scene : MonoBehaviour {



    public void LoadScene(int level)
    {
      
        Application.LoadLevel(level);
    }
    public void exitGame() {
        Application.Quit();
    }

}
