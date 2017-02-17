using UnityEngine;

public class Boss : MonoBehaviour {

    Animator anim;//ボスアニメーション
	
	void Start () {
        anim = GetComponent<Animator>();   
	}
	
	void Update () {
        anim.SetBool("BossStart", true);
        anim.SetBool("BossDefault", true);
	}
}
