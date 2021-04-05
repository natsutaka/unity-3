using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositinCreatePrefab : MonoBehaviour
{
    public GameObject Prefab;
    //クリックした位置座標
    private Vector3 clickPosition;

    private int CreateFlag = 0;

// Start is called before the first frame update
    void Start()
    {
        //生成したいPrefab
        GameObject Prefab = (GameObject)Resources.Load("Basic Motions Dummy.fbx");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CreateFlag == 0)
            {
                clickPosition = Input.mousePosition;
                clickPosition.z = 10f;
                GameObject PlayerCharacter = Instantiate(Prefab, Camera.main.ScreenToWorldPoint(clickPosition), Prefab.transform.rotation);

                CreateFlag++;
            }
            if(CreateFlag == 1)
            {
                //SceneManager.LoadScene("");
            }
        }
    }
}
