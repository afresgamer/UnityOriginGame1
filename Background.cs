using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    /// <summary>
    /// 背景用変数
    /// </summary>
    private Vector3 startPos;//初期位置
    public float Speed = 0.8f;//背景スクロールスピード
    public float Max_Height = -14;//最高点
    /// <summary>
    /// 背景上の山用変数
    /// </summary>
    public Sprite[] Mt; //山の画像
    public GameObject[] Children_Mt;//子のオブジェクトの数と情報
    
    //初期化
    void Start () {
        startPos = GetComponent<Transform>().position;
    }
	
	void Update () {
        Scroll_BG();
    }

    //背景スクロール処理(見せる必要ないからprivate)
    public void Scroll_BG()
    {
        //背景画像をスクロール
        float y = -Time.deltaTime * Speed;
        Vector3 v = new Vector3(0, y, 0);
        transform.position += v;

        //最高点までいったら背景と位置を再セット
        if (transform.position.y <= Max_Height)
        {
            transform.position = startPos;
            BG_Mt();
        }
    }

    //背景自動生成処理
    void BG_Mt()
    {
        //山の処理
        for (int i = 0; i < Children_Mt.Length; i++)
        {
            int RanMt = Random.Range(0, Mt.Length);
            Children_Mt[i].GetComponent<SpriteRenderer>().sprite = Mt[RanMt];
        }        
    }
}
