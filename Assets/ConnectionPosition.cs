using UnityEngine;
using System.Collections;

public class ConnectionPosition : MonoBehaviour {
	public GameObject unitA;
	private GameObject unitB;
	public Vector3 posA;
	public Vector3 posB;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (unitA != null && unitB != null) {
			if (unitA.transform.position != posA) {
				LineRenderer lineRenderer = GetComponent<LineRenderer>();
				lineRenderer.SetPosition(0, unitA.transform.position);
			}
			if (unitB.transform.position != posB) {
				LineRenderer lineRenderer = GetComponent<LineRenderer>();
				lineRenderer.SetPosition(1, unitB.transform.position);
			}
		}
	}
	
	void setUnits(GameObject[] unitArray) {
		unitA = unitArray[0];
		unitB = unitArray[1];
		posA = unitA.transform.position;
		posB = unitB.transform.position;
	}
}
