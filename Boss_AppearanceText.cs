using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss_AppearanceText : MonoBehaviour {

    public static bool B_AppearanceT_Flag = false;

    void Start () {
        //Debug.Log(B_AppearanceT_Flag);
        gameObject.GetComponent<Text>().enabled = false;
    }

	void Update () {
        if (B_AppearanceT_Flag)
        {
            gameObject.GetComponent<Text>().enabled = true;
            //Debug.Log(B_AppearanceT_Flag);
            if (Bullet.NextGame_Flag)
            {
                //Debug.Log("FLAG TRUE");
                Invoke("NextGame", 6);
            }
        }
    }

    public void NextGame()
    {
        SceneManager.LoadScene(2);
        Bullet.Score = 0;
        gameObject.GetComponent<Text>().enabled = false;
        //雑魚敵のリセット
        Spawn.StopSpawn = false;
        //ボス出現リセット
        BossSpawn.BossStart = false;
        //テキスト表示リセット
        Boss_AppearanceText.B_AppearanceT_Flag = false;
    }

}
