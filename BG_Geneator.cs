using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BG_Geneator : MonoBehaviour {

    const int StageSize = 20;
    int currentIndex;

    public Transform player; //ターゲットプレイヤー
    public GameObject[] stage; //ステージ配列
    public int startIndex; //自動生成開始インデックス
    public int preInstantiate; //先読み個数
    public List<GameObject> generatedstageList = new List<GameObject>();//ステージ保持リスト
    
	void Start () {
        //初期化
        currentIndex = startIndex - 1;
        UpdateStage(preInstantiate);
	}
	
	void Update () {
        //プレイヤーの現在の座標を計算
        int charaPosIndex = (int)(player.position.y / StageSize);
        //ステージ更新処理
        if(charaPosIndex + preInstantiate > currentIndex) {
            UpdateStage(charaPosIndex + preInstantiate);
        }
        //Debug.Log(currentIndex);
	}

    //ステージを生成して管理化
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentIndex) return;
        //指定のステージを作成
        for(int i = currentIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObj = GenerateStage(i);
            //生成したステージをリストで管理
            generatedstageList.Add(stageObj);
        }
        //ステージ上限内まで古いステージを削除
        while (generatedstageList.Count > preInstantiate + 2) DestroyOldStage();

        currentIndex = toTipIndex;
    }

    //指定のインデックス位置にStageオブジェクトをランダムに生成
    GameObject GenerateStage(int Index)
    {
        int nextstage = Random.Range(0, stage.Length);
        GameObject stageObj = (GameObject)Instantiate(
                                stage[nextstage],
                                new Vector3(0, Index * StageSize,0),
                                Quaternion.identity);
        return stageObj;
    }

    //一番古いステージを削除
    void DestroyOldStage()
    {
        GameObject oldstage = generatedstageList[0];
        generatedstageList.RemoveAt(0);
        Destroy(oldstage);
        //Debug.Log("DESTROY");
    }
}
