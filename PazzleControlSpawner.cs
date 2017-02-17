using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PazzleControlSpawner : MonoBehaviour {

    public float Timer = 1;//生成させる時間
    public Transform Spawn_Pos;
    public GameObject[] PazzleObj;//生成させるオブジェクト
    public float P_range = 3;//生成範囲
    public float Plus_timer = 1;//生成した後でプラスさせる時間
    private GameObject FirstPazzleObj;//最初にドラッグしたボール
    private GameObject LastPazzleObj;//最後にドラッグしたボール
    private string CurrentPazzleObj;// 名前判定用のstring変数
    //削除するお菓子のリスト
    List<GameObject> RemoveCandyList = new List<GameObject>();
    //ボスHP情報
    public BossLife B_L;
    //ボスに与えるダメージ
    float Boss_Damage = 0.3f;
    //プレイフラグ
    [HideInInspector]
    public bool IsPlaying = true;
    public GameTimer G_T;
    TouchInfo info;

    /// <summary>
    /// 最初のスポーンの処理
    /// </summary>
    Vector3 RandomPos()
    {
        Vector3 S_NewPos = Vector3.zero;
        float R_range = Random.Range(-P_range, P_range);
        S_NewPos = new Vector3(Spawn_Pos.position.x + R_range,
                                Spawn_Pos.position.y,
                                Spawn_Pos.position.z);
        return S_NewPos;
    }

    GameObject RandomCandy()
    {
        int Candy_Num = Random.Range(0, PazzleObj.Length);//ランダムにお菓子を選択
        return PazzleObj[Candy_Num];
    }

    void Start()
    {
        StartCoroutine(DropCandy(20));//最初に落ちるお菓子
    }
    
    public IEnumerator DropCandy(int Repeat_Num)
    {
        for (int i = 0; i < Repeat_Num; i++)
        {
            Instantiate(RandomCandy(),
                RandomPos(),
                Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward
            ));
            yield return new WaitForSeconds(0.03f);
        }
    }
    
    /// <summary>
    /// ドラックしたときの処理
    /// </summary>
    void Update()
    {
        info = AppUtil.GetTouch();
        if (IsPlaying)
        {
            //最初のクリックでFirstPazzleObjが空の時
            if (info == TouchInfo.Began && FirstPazzleObj == null) { OnDragStart(); }
            else if(Input.GetMouseButtonDown(0) && FirstPazzleObj == null){ OnDragStart(); }
            //クリックを終えた時
            else if(Input.GetMouseButtonUp(0)) { OnDragEnd(); }
            else if (info == TouchInfo.Ended) { OnDragEnd(); }
            //OnDragStartメソッド実行後
            else if (FirstPazzleObj != null) { OnDragging(); }
        }
    }

    void OnDragStart()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
        if(hit2D.collider != null)
        {
            GameObject hitObj = hit2D.collider.gameObject;
            string CandyName = hitObj.name;
            if (CandyName.IndexOf("CANDY") == 0)
            {
                FirstPazzleObj = hitObj;
                LastPazzleObj = hitObj;
                CurrentPazzleObj = hitObj.name;
                //リストの初期化
                RemoveCandyList = new List<GameObject>();
                //削除対象のオブジェクトを格納
                PushToCandy(hitObj);
            }
            //Debug.Log(CandyName.IndexOf("CANDY") == 0);
        }
    }

    void OnDragging()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit2D.collider != null)
        {
            GameObject hitObj = hit2D.collider.gameObject;
            //同じ名前のブロックをクリック＆lastBallとは別オブジェクトである時
            if (hitObj.name == CurrentPazzleObj && LastPazzleObj != hitObj)
            {
                //２つのオブジェクトの距離を取得
                float dis = Vector2.Distance(
                    hitObj.transform.position, LastPazzleObj.transform.position);
                if (dis < 0.8f)//ここで削除難易度決定
                {
                    //削除対象のオブジェクトを格納
                    LastPazzleObj = hitObj;
                    PushToCandy(hitObj);
                }
            }
        }
    }

    void OnDragEnd()
    {
        //要素数を取得
        int Candy_Cnt = RemoveCandyList.Count;
        if(Candy_Cnt >= 2)//消すお菓子の数の最低数
        {
            for(int i = 0; i < Candy_Cnt; i++)
            {
                Destroy(RemoveCandyList[i]);
                G_T.AddScore();
                B_L.Damage(Candy_Cnt * Boss_Damage);//ダメージ処理
            }
            //ボールを新たに生成
            StartCoroutine(DropCandy(Candy_Cnt));
        }
        else
        {
            for (int i = 0; i < Candy_Cnt; i++)
            {
                ColorChange(RemoveCandyList[i], 1);
            }
        }
        FirstPazzleObj = null;
        LastPazzleObj = null;
    }

    void PushToCandy(GameObject candy)
    {
        RemoveCandyList.Add(candy);
        ColorChange(candy,0.5f);
    }

    void ColorChange(GameObject candy,float tra)
    {
        //画像情報取得
        SpriteRenderer CandySprite = candy.GetComponent<SpriteRenderer>();
        //Colorプロパティのうち、透明度のみ変更する
        CandySprite.color = new Color(CandySprite.color.r,
                                      CandySprite.color.g,
                                      CandySprite.color.b,
                                      tra);
    }
  
}
