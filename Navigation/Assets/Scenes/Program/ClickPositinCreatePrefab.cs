using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

//フラグの構造体
public struct FLAG
{
    public bool CharaFlag;
    public bool GoalFlag;
    public bool Flag;
    public bool MoveCheck;

    public FLAG(bool CharaFlag, bool GoalFlag, bool Flag, bool MoveCheck)
    {
        this.CharaFlag = CharaFlag;
        this.GoalFlag = GoalFlag;
        this.Flag = Flag;
        this.MoveCheck = MoveCheck;
    }
}


public class ClickPositinCreatePrefab : MonoBehaviour
{
    //キャラクタのプレハブ
    public GameObject Prefab;

    //キャラクタのクローン
    public static GameObject Chara;

    //Goalのプレハブ
    public GameObject GoalPrefab;

    //Goalのクローン
    public static GameObject GoalPosition;

    //クリックした位置座標
    private Vector3 clickPosition;

    //Textオブジェクトの取得
    public GameObject TextCharaHouse;

    //Moveメソッド利用するためのGameObject
    //private GameObject MoveObject;

    //各フラグの初期値
    public static FLAG Flags = new FLAG(true, true, true, true);

    void Start()
    {
        //生成したいPrefab
        GameObject Prefab = (GameObject)Resources.Load("Basic Motions Dummy.fbx");

        //生成したいPrefab
        GameObject GoalPrefab = (GameObject)Resources.Load("Goal");

        //Textの取得
        GameObject TextCharaHouse = GameObject.Find("CharaText");

        if (Flags.CharaFlag && Flags.GoalFlag && Flags.Flag)
        {
            Destroy(Chara);
            Destroy(GoalPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //レイの追加
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        //左クリックを押されたとき
        if (Input.GetMouseButtonDown(0))
        {
            //生成されていない場合
            if (Flags.CharaFlag)
            {
                //レイを投げて何かのオブジェクトに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = Input.mousePosition;
                    clickPosition.z = 10f;

                    //キャラクタを生成しオブジェクトに保存
                    Chara = Instantiate(Prefab, hit.point, Prefab.transform.rotation);

                    //生成したのでフラグ変更
                    Flags.CharaFlag = false;
                }
            }

            else if(Flags.GoalFlag)
            {
                //レイを投げて何かのオブジェクトに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = Input.mousePosition;
                    clickPosition.z = 10f;

                    //ゴールの生成
                    GoalPosition = Instantiate(GoalPrefab, hit.point, GoalPrefab.transform.rotation);

                    //生成したのでフラグ変更
                    Flags.GoalFlag = false;
                }
            }

        }
        //キャラクタが生成されている場合
        if (!Flags.CharaFlag && !Flags.GoalFlag && Flags.Flag)
        {
            Flags.Flag = false;

            //PlayerControllerのコンポーネントを利用
            Chara.GetComponent<PlayerController>().MoveArea();

            //CharaHouseSceneを破棄しないようにする
            DontDestroyOnLoad(Chara);
            DontDestroyOnLoad(GoalPosition);

            //配置してくださいというテキストを非表示にする
            TextCharaHouse.SetActive(false);

            //次のシーンに切り替えてもそのままCharaHouseSceneが映る
            Application.LoadLevelAdditive("CharacterConfirm");


            //確認画面へ遷移
            //SceneManager.LoadScene("CharacterConfirm");
        }
        
    }


}
