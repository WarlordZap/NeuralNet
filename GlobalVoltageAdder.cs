using UnityEngine;
using System.Collections;

public class GlobalVoltageAdder : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		for (int i = 1; i < 4; i++) {
			GameObject node = GameObject.Find ("Unit" + i);
			node.SendMessage("addConnection", "Unit" + (i + 1));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		if (GUI.Button(new Rect(10, 10, 70, 50), "Add Volts!")) {
			addVolts(1);	
		}
	}
	
	void addVolts(int targetUnit) {
		GameObject node = GameObject.Find ("Unit" + targetUnit);
		node.SendMessage("recieveInput", 50);
	}
}
