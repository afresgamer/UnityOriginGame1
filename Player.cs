using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    Vector2 InitPos;
    public GameObject Bullet;//弾丸
    Rigidbody2D rb2D;
    public float _Speed = 2.0f;
    float Yaw = -15;//移動軸補正
    public Player_Hp P_hp;
    float Origin_R = 0.95f;//上画面補正
    float Origin_L = 0.05f;//下画面補正
    static public float C_Atk = 1;//お菓子ダメージ
    static public float S_Atk = 3;//岩石ダメージ

    void Start()
    {
        InitPos = transform.position;
        StopCoroutine("Flashing");//コルーチンの初期化
        rb2D = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        TouchInfo info = AppUtil.GetTouch();
        if (!P_hp.death)
        {
            P_Move();
            //弾丸発射
            if (Input.GetKeyDown(KeyCode.Space)) Shot();
            else if (info == TouchInfo.Began) Shot();
        }
        MoveClamp();//移動制限
    }

    void P_Move()//移動処理
    {
#if UNITY_EDITOR || UNITY_STANDALONE	
        //移動(window)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h, v);
        rb2D.velocity = move * _Speed;
        //向き補正
        float Rot = h * Yaw;
        transform.rotation = Quaternion.Euler(0, 0, Rot);
#endif
#if UNITY_EDITOR || UNITY_ANDROID
        //移動(Android and IOS)
        TouchInfo info = AppUtil.GetTouch();
        Vector2 A_move = AppUtil.GetTouchWorldPosition(Camera.main);
        Vector2 A_v = A_move - InitPos;
        if (info == TouchInfo.Began)
        {
            InitPos = A_move;
        }
        else if (info == TouchInfo.Moved)
        {
            Vector2 TargetPoint = Vector2.Lerp(InitPos, A_move, 1);
            transform.position = TargetPoint;
            //Debug.Log(transform.position);
            float A_Rot = A_v.normalized.x * Yaw;
            transform.rotation = Quaternion.Euler(0, 0, A_Rot);
        }
#endif
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {
        if (col2D.gameObject.tag == "CANDY")//お菓子に当たった時
        {
            //Debug.Log("CANDY");
            Destroy(col2D.gameObject);
            P_hp.gameObject.SendMessage("Damage", C_Atk);
            StartCoroutine("Flashing");
        }
        else if (col2D.gameObject.tag == "STONE")//石に当たった時
        {
            //Debug.Log("STONE");
            Destroy(col2D.gameObject);
            P_hp.gameObject.SendMessage("Damage", S_Atk);
            StartCoroutine("Flashing");
        }
    }

    private void Shot()
    {
        Instantiate(Bullet, transform.position + Vector3.up, transform.rotation);
    }

    /// <summary>
    /// 点滅のアニメーション
    /// </summary>
    IEnumerator Flashing()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.9f);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    /// <summary>
    /// 画面外にプレイヤーを出さない処理
    /// </summary>
    public void MoveClamp()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(Origin_L, Origin_L));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(Origin_R, Origin_R));
        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
}
 
