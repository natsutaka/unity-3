using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{

    //titleからmainへ
    public void PushGoToMainButton()
    {
        SceneManager.LoadScene("CharaHouseScene");
    }

    //キャラ配置確定の場合
    public void YesButton()
    {
        //確定のためFlagをfalseに変更
        ClickPositinCreatePrefab.Flags.CharaFlag = false;
        ClickPositinCreatePrefab.Flags.GoalFlag = false;
        ClickPositinCreatePrefab.Flags.Flag = false;
        ClickPositinCreatePrefab.Flags.MoveCheck = false;

        // ClickPositinCreatePrefab.TextCharaHouse.SetActive(false);
        SceneManager.LoadScene("CharaHouseScene");

    }

    //キャラ配置置きなおす場合
    public void NoButton()
    {
        //置きなおすためFlagをtrueに変更
        ClickPositinCreatePrefab.Flags.CharaFlag = true;
        ClickPositinCreatePrefab.Flags.GoalFlag = true;
        ClickPositinCreatePrefab.Flags.Flag = true;
        ClickPositinCreatePrefab.Flags.MoveCheck = true;
        SceneManager.LoadScene("CharaHouseScene");
    }

    //タイトルシーンに戻る
    public void TitleButton()
    {
        ClickPositinCreatePrefab.Flags.CharaFlag = true;
        ClickPositinCreatePrefab.Flags.GoalFlag = true;
        ClickPositinCreatePrefab.Flags.Flag = true;
        ClickPositinCreatePrefab.Flags.MoveCheck = true;
        SceneManager.LoadScene("TitleScene");
    }

    //動作が途中止まってしまった場合など
    public void ResetButton()
    {
        ClickPositinCreatePrefab.Flags.CharaFlag = true;
        ClickPositinCreatePrefab.Flags.GoalFlag = true;
        ClickPositinCreatePrefab.Flags.Flag = true;
        ClickPositinCreatePrefab.Flags.MoveCheck = true;
        SceneManager.LoadScene("CharaHouseScene");
    }
}