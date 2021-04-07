using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class jump : MonoBehaviour
{

    public Transform goal;
    private NavMeshAgent agent;

    private Animator animator;          //　アニメータコントローラ
    private Vector3 targetPosition;     //　移動する位置
    private float normalizedTime = 0f;  //　再生秒数
    private bool useLinkJump = false;   //　オフメッシュリンクをジャンプ中かどうか
    private float offsetY;              //　リンクジャンプ中のY座標の値
    [SerializeField]
    private AnimationCurve animCurve;   //　ジャンプアニメーションカーブ
    private float animationTime;        //　ジャンプアニメーションの再生時間
    private UnityEngine.AI.OffMeshLinkData offMeshLinkData;     //　オフメッシュリンクデータ
    private Vector3 startPos;           //　オフメッシュリンクのスタート位置
    private Vector3 endPos;              //　オフメッシュリンクのエンド位置


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        targetPosition = transform.position;

        foreach (var item in animator.runtimeAnimatorController.animationClips)
        {
            if (item.name == "LinkJump4")
            {
                animationTime = item.length;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            if (!useLinkJump)
            {
                offMeshLinkData = agent.currentOffMeshLinkData;     //　エージェントの現在のオフメッシュリンクデータを取得
                startPos = offMeshLinkData.startPos;                 //　オフメッシュリンクのスタート位置
                endPos = offMeshLinkData.endPos;

                if (offMeshLinkData.linkType == UnityEngine.AI.OffMeshLinkType.LinkTypeJumpAcross || offMeshLinkData.linkType == UnityEngine.AI.OffMeshLinkType.LinkTypeManual)
                {
                    animator.SetBool("LinkJump", true);
                }
                useLinkJump = true;
                normalizedTime = 0f;

            }
            normalizedTime += Time.deltaTime;
            offsetY = animCurve.Evaluate(normalizedTime * (1f / animationTime));    //　アニメーションカーブの横軸から縦軸を取得
            if (normalizedTime * (1f / animationTime) >= 1f)    //　アニメーション終了時にジャンプ終了
            {
                agent.CompleteOffMeshLink();
                useLinkJump = false;
                animator.SetBool("LinkJump", false);
            }
            
        }
    }
}
