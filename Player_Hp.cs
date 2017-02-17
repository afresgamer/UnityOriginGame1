using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Hp : MonoBehaviour {

    Slider slider;
    [HideInInspector]
    public bool death = false;
    public GameObject Explosion;
    public GameObject _Player;
    public Text g_o;
    public Text g_o_u;

    void Start()
    {
        slider = GetComponent<Slider>();
        // HPゲージに値を設定
        slider.value = slider.maxValue;
        g_o.enabled = false;
        g_o_u.enabled = false;
    }

	void Update () {

        if ( slider.value <= 0) {
            slider.value = 0;
            death = true;
            Death();
        }
        else if(slider.maxValue < slider.value) { slider.value = slider.maxValue; }
    }

    public void Damage(float damage)
    {
        // HPゲージに値を改めて設定
        slider.value -=  damage;
    }

    public void Recover(float Recovery)
    {
        // HPゲージに値を改めて設定
        slider.value += Recovery;
    }

    void Death()
    {
        //Debug.Log("Death");
        if (!_Player) { return; }//プレイヤーがいなかったら返す
        Instantiate(Explosion, _Player.transform.position, Quaternion.identity);
        Destroy(_Player);
        g_o.enabled = true;
        g_o_u.enabled = true;
        StartCoroutine("_DeathAnim");
    }

    IEnumerator _DeathAnim()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
        g_o.enabled = false;
        g_o_u.enabled = false;
    }
}
