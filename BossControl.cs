using UnityEngine;
using System.Collections;

public class BossControl : MonoBehaviour {

    public PazzleBossAnimControl p_b_a_c;
    public PazzleControlSpawner p_c_s;
    public GameTimer g_t;
    public BossLife b_l;
    Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
        p_b_a_c.enabled = false;
        p_c_s.enabled = false;
        g_t.enabled = false;
        b_l.enabled = false;
	}
		
	void Update () {
        anim.SetBool("Boss_Start", true);
        if (anim.GetBool("Boss_Start"))
        {
            p_b_a_c.enabled = true;
            p_c_s.enabled = true;
            g_t.enabled = true;
            b_l.enabled = true;
        }
	}
}
