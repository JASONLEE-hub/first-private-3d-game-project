using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMEOVER : MonoBehaviour
{

    void Update() {
        Invoke("GGAMEOVER",9f);
    }

    void GGAMEOVER(){
        SceneManager.LoadScene(8);
    }
}
