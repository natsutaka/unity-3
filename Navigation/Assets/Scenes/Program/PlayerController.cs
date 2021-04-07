using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //キャラクタのナビメッシュ
    public static NavMeshAgent Player_Nav;
    //目的地のオブジェクト
    public static GameObject Destination;

    void Start()
    {

    }


    void Update()
    { 
      //キャラクタが生成されたのが確認できた場合
      if (!ClickPositinCreatePrefab.Flags.MoveCheck)
        {
            Player_Nav.SetDestination(Destination.transform.position);
            ClickPositinCreatePrefab.Flags.MoveCheck = true;
        }

    }

     public void MoveArea()
    {
        if (!ClickPositinCreatePrefab.Flags.CharaFlag && !ClickPositinCreatePrefab.Flags.GoalFlag && !ClickPositinCreatePrefab.Flags.Flag)
        {
            //プレイヤー(クローン)のNavMeshAgentを取得
            Player_Nav = GameObject.Find("Player(Clone)").GetComponent<NavMeshAgent>();
            //目的地のオブジェクトを取得
            Destination = GameObject.Find("Goal(Clone)");

        }

    }
}




///using System.Collections;
///using System.Collections.Generic;
///using UnityEngine.AI;
///using UnityEngine;
///using UnityEngine.Events;
///
///public class PlayerController : MonoBehaviour
///{
///    NavMeshAgent Player_Nav;
///    GameObject Destination;
///    GameObject JumpStartPlace;
///    GameObject JumpFinishPlace;
///
///    ///<summary>
///    ///    ジャンプ処理に使用するRigidbody
///    ///</summary>
///    private Rigidbody _rigidBody;
///
///    ///<summery>
///    ///    ジャンプアニメーションを担当するAnimator
///    ///</summery>
///    private Animator _animator;
///
///    ///<summary>
///    ///    ジャンプ入力フラグ
///    ///    ジャンプ入力が一度でもあったらON、着地したらOFF
///    ///</summary>
///    private bool _jumpInput = false;
///
///    ///<summary>
///    ///    ジャンプ処理中フラグ
///    ///    ジャンプ処理が開始されたらON、着地したらOFF
///    ///</summary>
///    private bool _isJumping = false;
///
///
///    ///<summary>
///    ///    Start()
///    ///</summary>
///    private void Start()
///    {
///        _rigidBody = GetComponent<Rigidbody>();
///        _animator = GetComponent<Animator>();
///
///        //プレイヤーのNavMeshAgentを取得
///        Player_Nav = GetComponent<NavMeshAgent>();
///        //目的地のオブジェクトを取得
///        Destination = GameObject.Find("Destination");
///        //ジャンプ地点のオブジェクトを取得
///        JumpStartPlace = GameObject.Find("Cylinder");
///        JumpFinishPlace = GameObject.Find("Cylinder(1)");
///    }
///
///    ///<summary>
///    ///    Update()
///    ///</summary>
///    private void Update()
///    {
///        //目的地を設定
///        Player_Nav.SetDestination(Destination.transform.position);
///
///        CheckGroundDistance(() => {
///            _jumpInput = false;
///            _isJumping = false;
///            _animator.SetBool("Run", true);    // <- 追加
///            _animator.SetBool("Jump", false);    // <- 追加
///        });
///
///        // 既にジャンプ入力が行われていたら、ジャンプ入力チェックを飛ばす
///        if (_jumpInput || JumpInput()) _jumpInput = true;
///    }
///
///    ///<summary>
///    ///    FixedUpdate()
///    ///</summary>
///    private void FixedUpdate()
///    {
///        if (_jumpInput)
///        {
///            if (!_isJumping)
///            {
///                _isJumping = true;
///                DoJump();
///                _animator.SetBool("Jump", true);    // <- 追加
///                _animator.SetBool("Run", false);    // <- 追加
///            }
///        }
///    }
///
///    ///オブジェクト同士が触れているか判定
///    void OnCollisionStay(Collision collision)
///    {
///        Debug.Log("Collision Stay: " + collision.gameObject.name);
///        if (collision.gameObject.name == "Cylinder") DoJump();
///    }
///
///    ///<summary>
///    ///    ジャンプ入力チェック
///    ///</summary>
///    private bool JumpInput()
///    {
///        // ジャンプ最速入力のテスト用にGetButton
///        //if (Input.GetButton("Jump")) return true;    // ジャンプキー押しっぱなしで連続ジャンプ
///        //if (Input.GetButtonDown("Jump")) return true;    // ジャンプキー押しっぱなしで連続ジャンプ
///        //if (OnCollisionStay() == Cylinder) return true;                                             //if (Input.GetButtonDown("Jump")) return true;    // ジャンプキーが押された時だけジャンプにする時はこっち
///                                                                                                    // または、 if (Input.GetKeyUp(KeyCode.Space)) return true; とかでも可
///        return false;
///    }
///
///
///    ///<summary>
///    ///    ジャンプの強さ
///    ///</summary>
///    [SerializeField] private float jumpPower = 5f;
///    //private const float jumpPower = 5f;
///
///    ///<summary>
///    ///    ジャンプのための上方向への加圧
///    ///</summary>
///    private void DoJump()
///    {
///        _rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
///    }
///
///
///    ///<summary>
///    ///    接地してから何フレーム経過したか
///    ///    接地してない間は常にゼロとする
///    ///</summary>
///    private int _isGround = 0;
///
///    ///<summary>
///    ///    接地してない間、何フレーム経過したか
///    ///    接地している間は常にゼロとする
///    ///</summary>
///    private int _notGround = 0;
///
///    ///<summary>
///    ///    このフレーム数分接地していたらor接地していなかったら
///    ///    状態が変わったと認識する（ジャンプ開始したor着地した）
///    ///    接地してからキャラの状態が安定するまでに数フレーム用するため、
///    ///    キャラが安定する前に再ジャンプ入力を受け付けてしまうとバグる（ジャンプ出来なくなる）
///    ///    筆者PCでは 3 で安定するが、安全をとって今回は 5 とした
///    ///</summary>
///    private const int _isGroundStateChange = 5;
///
///    ///<summary>
///    ///    プレイヤーと地面の間の距離
///    ///    IsGround()が呼ばれるたびに更新される
///    ///</summary>
///    [SerializeField] private float _groundDistance = 0f;
///
///    ///<summary>
///    ///    _groundDistanceがこの値以下の場合接地していると判定する
///    ///</summary>
///    private const float _groundDistanceLimit = 0.01f;
///
///    ///<summary>
///    ///    判定元の原点が地面に極端に近いとrayがヒットしない場合があるので、
///    ///    オフセットを設けて確実にヒットするようにする
///    ///</summary>
///    private Vector3 _raycastOffset = new Vector3(0f, 0.005f, 0f);
///
///    ///<summary>
///    ///    プレイヤーキャラから下向きに地面判定のrayを飛ばす時の上限距離
///    ///    ゲーム中でプレイヤーキャラと地面が最も離れると考えられる場面の距離に、
///    ///    マージンを多少付けた値にしておくのが良
///    ///    Mathf.Infinityを指定すれば無制限も可能だが重くなる可能性があるかも？
///    ///</summary>
///    private const float _raycastSearchDistance = 100f;
///
///    ///<summary>
///    ///    接地判定
///    ///</summary>
///    private void CheckGroundDistance(UnityAction landingAction = null, UnityAction takeOffAction = null)
///    {
///        RaycastHit hit;
///        var layerMask = LayerMask.GetMask("Ground");
///
///        // プレイヤーの位置から下向きにRaycast
///        // レイヤーマスクでGroundを設定しているので、
///        // 地面のGameObjectにGroundのレイヤーを設定しておけば、
///        // Groundのレイヤーを持つGameObjectで一番近いものが一つだけヒットする
///        var isGroundHit = Physics.Raycast(
///                transform.position + _raycastOffset,
///                transform.TransformDirection(Vector3.down),
///                out hit,
///                _raycastSearchDistance,
///                layerMask
///            );
///
///        if (isGroundHit)
///        {
///            _groundDistance = hit.distance;
///        }
///        else
///        {
///            // ヒットしなかった場合はキャラの下方に地面が存在しないものとして扱う
///            _groundDistance = float.MaxValue;
///        }
///
///        // 地面とキャラの距離は環境によって様々で
///        // 完全にゼロにはならない時もあるため、
///        // ジャンプしていない時の値に多少のマージンをのせた
///        // 一定値以下を接地と判定する
///        // 通常あり得ないと思われるが、オーバーフローされると再度アクションが実行されてしまうので、越えたところで止める
///        if (_groundDistance < _groundDistanceLimit)
///        {
///            if (_isGround <= _isGroundStateChange)
///            {
///                _isGround += 1;
///                _notGround = 0;
///            }
///        }
///        else
///        {
///            if (_notGround <= _isGroundStateChange)
///            {
///                _isGround = 0;
///                _notGround += 1;
///            }
///        }
///
///        // 接地後またはジャンプ後、特定フレーム分状態の変化が無ければ、
///        // 状態が安定したものとして接地処理またはジャンプ処理を行う
///        if (_isGroundStateChange == _isGround && _notGround == 0)
///        {
///            if (landingAction != null) landingAction();
///        }
///        else
///        if (_isGroundStateChange == _notGround && _isGround == 0)
///        {
///            if (takeOffAction != null) takeOffAction();
///        }
///    }
///}
