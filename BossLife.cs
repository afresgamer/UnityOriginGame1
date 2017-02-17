using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour {

    public Slider sliver;
    [HideInInspector]
    public bool Dead = false;
    public Text GameOver;//ゲームオーバー用テキスト
    public Text GameClear;//ゲームクリア用テキスト
    public Text GameClear_Under;//ゲームオーバーエフェクト用テキスト
    public Text GameOver_Under;//ゲームクリアエフェクト用テキスト

    void Start () {
        sliver = GetComponent<Slider>();
        sliver.value = sliver.maxValue;
        GameClear.enabled = false;
        GameClear_Under.enabled = false;
        GameOver.enabled = false;
        GameOver_Under.enabled = false;
        //Debug.Log(sliver.value);
    }
	
	void Update () {
        if (sliver.minValue > sliver.value) { sliver.value = 0; }
        else if (sliver.maxValue < sliver.value) { sliver.value = sliver.maxValue; }
	}

    //ダメージ処理
    public void Damage(float damage)
    {
        sliver.value -= damage;
        //Debug.Log(sliver.value);
    }

    //回復処理(使うかわかんないけど一応保険)
    public void Recover(float recover)
    {
        sliver.value += recover;
    }
}
