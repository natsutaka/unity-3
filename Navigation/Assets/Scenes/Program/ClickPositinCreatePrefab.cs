using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class ClickPositinCreatePrefab : MonoBehaviour
{
    //キャラクタの詳細情報
    public GameObject Prefab;
    //クリックした位置座標
    private Vector3 clickPosition;

    //Moveメソッド利用するためのGameObject
    //private GameObject MoveObject;
    
    //生成されたかの判定フラグ
    private int CreateFlag = 0;

    void Start()
    {
        //生成したいPrefab
        GameObject Prefab = (GameObject)Resources.Load("Basic Motions Dummy.fbx");

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
            if (CreateFlag == 0)
            {
                //レイを投げて何かのオブジェクトに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = Input.mousePosition;
                    clickPosition.z = 10f;

                    //キャラクタ生成
                    Instantiate(Prefab, hit.point, Prefab.transform.rotation);

                    //生成したのでフラグを1
                    CreateFlag++;
                }
            }

        }
        //キャラクタが生成されている場合
        if (CreateFlag == 1)
        {
            //PlayerControllerのコンポーネントを利用
            Prefab.GetComponent<PlayerController>().MoveArea();


            //確認画面へ遷移
            SceneManager.LoadScene("CharacterConfirm");
            CreateFlag++;
        }
        
    }
}
