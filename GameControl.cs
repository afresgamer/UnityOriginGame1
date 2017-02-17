using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControl : MonoBehaviour {
    
    public enum NextStage
    {
        None = 0,
        Stage1 = 100,
        Stage2 = 200,
        Boss_Stage = 300 //クリアスコア
    }

    [HideInInspector]
    public NextStage n_s = NextStage.None;
    public Spin_Obj[] s_o;
    public Text StageText;
    public Bullet bullet;

    void Start()
    {
        //クリアスコア設定
        bullet.StageClearScore = (int)NextStage.Boss_Stage;
    }

    void Update()
    {
        int NowScore = Bullet.Score;
        //スコアによって処理を区別
        if (NowScore < (int)NextStage.Stage1) { n_s = NextStage.None; Set_Stage(n_s); }
        else if (NowScore == (int)NextStage.Stage1) { n_s = NextStage.Stage1; Set_Stage(n_s);  }
        else if (NowScore > (int)NextStage.Stage1) { SetCandySpeed(2); }
        if (NowScore == (int)NextStage.Stage2) { n_s = NextStage.Stage2; Set_Stage(n_s); }
        else if (NowScore > (int)NextStage.Stage2) { SetCandySpeed(4); }
        
    }

    void StageClear(NextStage n_stage)//テキスト表示
    {
        StageText.text = n_stage.ToString();
        StageText.enabled = true;
        StartCoroutine("VisibleText");
    }

    public void Set_Stage(NextStage n_stage)//ステージごとの処理
    {
        switch (n_s)
        {
            case NextStage.None://100点以下の時
                StageText.enabled = false;
                SetCandySpeed(1);
                break;
            case NextStage.Stage1://100点以上の時
                n_s = NextStage.Stage1;
                SetCandySpeed(2);
                StageClear(n_s);
                break;
            case NextStage.Stage2://200点以上の時
                n_s = NextStage.Stage2;
                SetCandySpeed(4);
                StageClear(n_s);
                break;
            default:
                //StageText.enabled = false;
                break;
        }
        //Debug.Log(s_o[0].GetComponent<Rigidbody2D>().gravityScale);
    }

    void SetCandySpeed(int c_s)//お菓子スピード
    {
        for(int i = 0; i < s_o.Length; i++)
        {
            s_o[i].GetComponent<Rigidbody2D>().gravityScale = c_s;
        }
    }

    IEnumerator VisibleText()//数秒後にテキスト非表示
    {
        yield return new WaitForSeconds(2);
        StageText.enabled = false;
    }

}
