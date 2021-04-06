using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    //titleからmainへ
    public void PushGoToMainButton()
     {
        SceneManager.LoadScene("character");
     }

    //キャラ配置確定の場合
    public void YesButton()
    {
        SceneManager.LoadScene("character");
   
    }

    //キャラ配置置きなおす場合
    public void NoButton()
    {
        SceneManager.LoadScene("character");
    }
}
