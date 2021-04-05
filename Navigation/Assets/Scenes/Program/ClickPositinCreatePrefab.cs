using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickPositinCreatePrefab : MonoBehaviour
{
    //public GameObject Plane;
    public GameObject Prefab;
    //クリックした位置座標
    private Vector3 clickPosition;

    private int CreateFlag = 0;

// Start is called before the first frame update
    void Start()
    {
        //生成したいPrefab
        GameObject Prefab = (GameObject)Resources.Load("Basic Motions Dummy.fbx");
        //GameObject Plane = GameObject.Find("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Input.GetMouseButtonDown(0))
        {
            if (CreateFlag == 0)
            {
                //レイを投げて何かのオブジェクトに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = Input.mousePosition;
                    clickPosition.z = 10f;
                    //clickPosition.y = 10f;
                    GameObject PlayerCharacter = Instantiate(Prefab, hit.point, Prefab.transform.rotation);

                    CreateFlag++;
                }
            }
            if(CreateFlag == 1)
            {
                SceneManager.LoadScene("CharacterConfirm");
            }
        }
    }
}
