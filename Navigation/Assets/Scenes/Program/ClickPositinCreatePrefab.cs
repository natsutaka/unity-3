using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class ClickPositinCreatePrefab : MonoBehaviour
{
    //キャラクタのプレハブ
    public GameObject Prefab;

    //Goalのプレハブ
    public GameObject Destination;

    //クリックした位置座標
    private Vector3 clickPosition;

    //Moveメソッド利用するためのGameObject
    //private GameObject MoveObject;

    private bool CharaFlag = true;
    private bool GoalFlag = true;

    void Start()
    {
        //生成したいPrefab
        GameObject Prefab = (GameObject)Resources.Load("Basic Motions Dummy.fbx");

        //生成したいDestination
        GameObject Destination = (GameObject)Resources.Load("Goal");
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
            if (CharaFlag)
            {
                //レイを投げて何かのオブジェクトに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = Input.mousePosition;
                    clickPosition.z = 10f;

                    //キャラクタ生成
                    Instantiate(Prefab, hit.point, Prefab.transform.rotation);

                    //生成したのでフラグ変更
                    CharaFlag = false;
                }
            }

            else if(GoalFlag)
            {
                //レイを投げて何かのオブジェクトに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = Input.mousePosition;
                    clickPosition.z = 10f;

                    //キャラクタ生成
                    Instantiate(Destination, hit.point, Destination.transform.rotation);

                    //生成したのでフラグ変更
                    GoalFlag = false;
                }
            }

        }
        //キャラクタが生成されている場合
        if (!CharaFlag && !GoalFlag)
        {
            //PlayerControllerのコンポーネントを利用
            Prefab.GetComponent<PlayerController>().MoveArea();

            //確認画面へ遷移
           // SceneManager.LoadScene("CharaHouseScene");
        }
        
    }
}
