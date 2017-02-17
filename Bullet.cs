using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Rigidbody2D rb2D;
    public float Speed = 2;//弾の速さ
    public GameObject Explosion;//爆発オブジェクト
    public static int Score = 0;//現在のスコア
    const int Plus_Score = 10;//加算スコアポイント
    [HideInInspector]
    public int StageClearScore;//ステージクリア用変数
    public GameObject Enemy_Createing;//敵の生成ストップ用
    bool StageClear = false;//全体のフラグの管理用フラグ
    [HideInInspector]
    public static bool NextGame_Flag = false;

    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        //Debug.Log(Spawn.StopSpawn);
    }
	
	void Update () {
        rb2D.velocity = transform.up.normalized * Speed;//弾スピード
    }
    //弾の衝突判定
    void OnCollisionEnter2D(Collision2D col2D)
    {
        if(col2D.gameObject.tag == "CANDY" || col2D.gameObject.tag == "STONE") {
            Destroy(col2D.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Score += Plus_Score;
            if (Score == StageClearScore) StageClear = true;
            if (StageClear) Boss_Appearance();//ボス出現処理
        }
        if (Score >= StageClearScore) StageClear = false;
    }
    
    public void Boss_Appearance()
    {
        //雑魚敵の出現停止
        Spawn.StopSpawn = true;
        //ボス出現
        BossSpawn.BossStart = true;
        //テキスト表示
        Boss_AppearanceText.B_AppearanceT_Flag = true;
        //ボス再出現防止
        StageClear = false;
        NextGame_Flag = true;
    }
}
