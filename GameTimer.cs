using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour {

    public Text GameCountTimer;//カウント用テキスト
    float DelayTime = 1; //カウント補正秒数
    public float NextGameTimer = 60;//制限時間
    public Text Score;//スコア用テキスト
    public int PlusScore = 10;//加算スコア
    private int ScorePoint = 0;//表示用変数
    //クラス呼び出し
    public BossLife B_L;
    public PazzleControlSpawner P_C_S;
    public PazzleBossAnimControl P_B_A_C;
    static public bool GameStop;//ストップ用フラグ
    //public GameObject[] Candy; 

    void Start()
    {
        GameStop = true;
        GameCountTimer.text = NextGameTimer.ToString();
        Score.text = ScorePoint.ToString();
    }

    void Update()
    {
        if (GameStop) { StartCoroutine(SetTimer()); }
    }

    public IEnumerator SetTimer()//タイマー用関数
    {
        NextGameTimer -= Time.deltaTime;
        if (NextGameTimer < 0) {
            NextGameTimer = 0;
            P_C_S.IsPlaying = false;//お菓子生成止める
            //ゲームオーバー用テキスト表示
            B_L.GameOver.enabled = true;
            B_L.GameOver_Under.enabled = true;
            StartCoroutine(GameOver());//ゲームオーバー
        }
        GameCountTimer.text = NextGameTimer.ToString("f0");//表示
        yield return new WaitForSeconds(DelayTime);
    }

    public void AddScore()//加算スコア用
    {
        ScorePoint += PlusScore;
        Score.text = ScorePoint.ToString();
    }

    public void StopTimer()//タイマー止める用
    {
        GameStop = false;
        GameCountTimer.text = NextGameTimer.ToString("f0");
        //Debug.Log("Stop");
    }

    public IEnumerator GameOver()//ゲームオーバー
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }
}
