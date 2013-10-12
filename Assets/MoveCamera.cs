using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.activeSelf) {
			if (Input.GetKey("up")) {
				//Camera.current.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
				transform.Translate(0,0,5);
			}	
			if (Input.GetKey("down")) {
				transform.Translate(0,0,-5);
			}	
			if (Input.GetKey("left")) {
				transform.Translate(-5,0,0);
			}	
			if (Input.GetKey("right")) {
				transform.Translate(5,0,0);
			}	
		}
	}
}
