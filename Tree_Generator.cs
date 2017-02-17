using UnityEngine;
using System.Collections.Generic;

public class Tree_Generator : MonoBehaviour {

    //季節ごとに出す木の生成用条件
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    Season season;
    Background Bg;
    public float Tree_Speed = 1; //スクロールスピード
    public float Max_H;
    private Vector3 StartPos;
    public Sprite[] Spring_T;//春用木画像
    public Sprite[] Summer_T;//夏用木画像
    public Sprite[] Fall_T;//秋用木画像
    public Sprite[] Winter_T;//冬用木画像
    SpriteRenderer[] SP;
    int BG_Count = 0;//季節変換用変数
    bool ChangeSeason = false;//季節変換用bool値

    void Start () {
        StartPos = GetComponent<Transform>().position;
        SP = GetComponentsInChildren<SpriteRenderer>();
    }
	
	void Update () {
        Scroll_Tree();
        if (ChangeSeason) { Season_Change(); }
    }

    void Scroll_Tree()
    {
        //背景スクロール処理
        float y = -Time.deltaTime * Tree_Speed;
        Vector3 v = new Vector3(0, y, 0);
        transform.position += v;
        if (transform.position.y <= Max_H)
        {
            BG_Count += 1;
            transform.position = StartPos;
            if(BG_Count == 1) { ChangeSeason = true; season = Season.Spring; }
            else if(BG_Count == 2) { ChangeSeason = true;  season = Season.Summer; }
            else if(BG_Count == 3) { ChangeSeason = true; season = Season.Fall; }
            else if(BG_Count == 4) { ChangeSeason = true;  season = Season.Winter; }
            else { ChangeSeason = false; }
            if(BG_Count >= 6) { BG_Count = 1; }
        }
    }

    //季節ごとに木生成処理遷移処理
    void Season_Change()
    {
        //Debug.Log(season);
        switch (season)
        {
            case Season.Spring:
                if (BG_Count == 1)
                {
                    S_Create_Tree(season);
                    //Debug.Log("Spring Come!");
                }
                break;
            case Season.Summer:
                if (BG_Count == 2)
                {
                    S_Create_Tree(season);
                    //Debug.Log("Summer Come!");
                }
                break;
            case Season.Fall:
                if(BG_Count == 3) {
                    S_Create_Tree(season);
                    //Debug.Log("Fall Come!");
                }
                break;
            case Season.Winter:
                if (BG_Count >= 4)
                {
                    S_Create_Tree(season);
                    //Debug.Log("Winter Come!");
                }
                break;
        }

    }

    //
    void S_Create_Tree(Season s)
    {
        //Debug.Log(s);
        for (int i = 0; i < SP.Length; i++)
        {
            int Ran_Tree = Random.Range(0, Summer_T.Length);
            if (s == Season.Spring ){
                Sprite s1 = Spring_T[0];
                SP[i].sprite = s1;
            }
            if (s == Season.Summer) {
                Sprite s2 = Summer_T[Ran_Tree];
                SP[i].sprite = s2;
            }
            if (s == Season.Fall){
                Sprite s3 = Fall_T[Ran_Tree];
                SP[i].sprite = s3;
            }
            if (s == Season.Winter) {
                Sprite s4 = Winter_T[Ran_Tree];
                SP[i].sprite = s4;
            }
        }
        ChangeSeason = false;//これがないと無限生成
    }
}
