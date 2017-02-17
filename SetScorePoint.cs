using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetScorePoint : MonoBehaviour {

    public static int Shooting_Score = 0;
    public static Text ScorePointText;

	void Start () {
        ScorePointText = GetComponent<Text>();
        ScorePointText.text = Shooting_Score.ToString();
	}
	
	void Update () {
        ScorePointText.text = Bullet.Score.ToString();
	}
}
