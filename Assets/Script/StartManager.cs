using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public int lifeCount = 2;
    public int stageCount = 0;
    public GameObject sStart;
    public GameObject sContinue;
    public GameObject newStart;

    void Awake()
    {
        if(stageCount != 0){ // 처음이 아니라면
            sContinue.SetActive(true); // 컨티뉴 켜기
            newStart.SetActive(true);
            sStart.SetActive(false);
        }
        else if (stageCount == 0){
            sContinue.SetActive(false);
            newStart.SetActive(false);
            sStart.SetActive(true);
        }
    }

    public void NewStart(){ // 새로 시작 또는 시작
        PlayerPrefs.SetInt("Life",2);
        PlayerPrefs.SetInt("Stage",0);
        LoadingSceneController.LoadScene(0);
    }

    public void SContinue(){ // 이어서 하기 
        stageCount = PlayerPrefs.GetInt("Stage");
        lifeCount = PlayerPrefs.GetInt("Life");
        LoadingSceneController.LoadScene(lifeCount);
    }
}
