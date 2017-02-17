using UnityEngine;
using System.Collections;

public class Compliance_Camera : MonoBehaviour {

    private Vector3 offset;
    public float smoothing = 5f;
    public Transform target;

	void Start () {
        offset = transform.position - target.position;
	}
	
	
	void Update () {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
