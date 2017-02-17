using UnityEngine;

public class PazzleBossAnimControl : MonoBehaviour {
    Animator anim;//アニメーション
    AudioSource a_s;//クリアBGM
    public PazzleControlSpawner P_C_S;//ボスコントローラー情報
    public BossLife B_L;//ボスHP情報
    public GameTimer G_T;
    //お菓子生成間隔
    float Fall_Timer = 0;
    //最大生成時間
    private float Max_T;

    void Start () {
        anim = GetComponent<Animator>();
        a_s = GetComponent<AudioSource>();
        //p_s = GetComponentInChildren<ParticleSystem>();
	}

    void Update()
    {
        
        int NowBossHp = (int)B_L.sliver.value;
        Fall_Timer += Time.deltaTime;
        switch (NowBossHp)
        {
            case 100:
                Max_T = 8;
                break;
            case 80:
                Max_T = 7;
                break;
            case 60:
                Max_T = 4;
                anim.SetBool("Angry", NowBossHp <= 30);
                break;
            case 50:
                Max_T = 5;
                break;
            case 30:
                Max_T = 6;
                anim.SetBool("Impatient", NowBossHp <= 20);
                break;
            case 10:
                Max_T = 10;
                break;
        }
        if(Fall_Timer > Max_T) { BossState(); Fall_Timer = 0; }
        //Debug.Log(Max_T);
        //Debug.Log(NowBossHp);
    }

    public void BossState()
    {
        
        float NowBossHp = B_L.sliver.value;
        float DeadBossHp = B_L.sliver.minValue;
        int EasyCandySpeed = 8;
        int NormalCandySpeed = 6;
        int HardCandySpeed = 4;
        //落とすお菓子の数をボス状態から取得
        //条件でアニメーション遷移
        if (NowBossHp >= 80)//体力が40以上
        {
            anim.SetBool("Fall",true);
            
            Invoke("ResetAnim", 0.1f);
            StartCoroutine(P_C_S.DropCandy(EasyCandySpeed));
        }
        else if(NowBossHp >= 30)//体力が30以上
        {
            anim.SetBool("Fall", true);

            Invoke("ResetAnim", 0.1f);
            StartCoroutine(P_C_S.DropCandy(NormalCandySpeed));
        }
        else if(NowBossHp <= 10)//体力が10以下
        {
            anim.SetBool("Fall", true);

            Invoke("ResetAnim", 0.1f);
            StartCoroutine(P_C_S.DropCandy(HardCandySpeed));
        }
        
        if(NowBossHp <= DeadBossHp)//ボスが死んだとき
        {
            if (G_T.NextGameTimer > 0)//時間制限内にクリア
            {
                Camera.main.GetComponent<AudioSource>().Stop();
                a_s.Play();

                P_C_S.IsPlaying = false;
                G_T.StopTimer();
                B_L.GameClear.enabled = true;
                B_L.GameClear_Under.enabled = true;
                anim.SetBool("Dead", NowBossHp <= DeadBossHp);
                
                StartCoroutine(G_T.GameOver());
                Debug.Log("DEATH");
            }

        }
    }
 
    void ResetAnim()//アニメーションのリセット
    {
        
        float NowBossHp = B_L.sliver.value;
        if(NowBossHp >= 40)
        {
            anim.SetBool("Fall", false);
        }
        else if(NowBossHp >= 21)
        {
            anim.SetBool("Angry", true);
        }
        else if(NowBossHp <= 20)
        {
            anim.SetBool("Impatient", true);
        }
    }

}
