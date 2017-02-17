using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public static bool FinishFlag;
    Animator anim;
    AudioSource _AS;

	void Start () {
        FinishFlag = false;
        StopAllCoroutines();
        anim = GetComponent<Animator>();
        _AS = GetComponent<AudioSource>();
	}
	
	
	void Update () {
        StartCoroutine("FontFunc");
	}

    IEnumerator FontFunc()
    {
        anim.SetBool("GameStart", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("FontFlag", true);
        //Debug.Log("FontAnim");
    }

    public void _GameStart()
    {
        StartCoroutine("FinishAnim");
    }

    IEnumerator FinishAnim()
    {
        anim.SetBool("TitleFinished", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main");

    }

    public void Audio_Play_Select()
    {
        _AS.Play();
    }
}
