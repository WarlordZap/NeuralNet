using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	void OnGUI () {
		if (GUI.Button (new Rect (10,10,150,100), "I am a button")) {
			print ("You clicked the button!");
		}
	}
}
