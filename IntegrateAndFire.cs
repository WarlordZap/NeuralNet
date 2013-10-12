using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntegrateAndFire : MonoBehaviour {
	
	public float voltage = 0;
	public float restingState = 0;
	public float threshold = 100;
	public float spikeValue = 75;
	public float delay = 0.25F;
	public AudioClip winning;
	private int stayRed = 0;
	private List<string> outConnections = new List<string>();
	
	
	// Use this for initialization
	void Start () {	
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void addConnection(string nodeName) {
		if (!outConnections.Contains(nodeName)) {
			outConnections.Add(nodeName);	
		}
	}
	
	void removeConnection(string nodeName) {
		if (outConnections.Contains(nodeName)) {
			outConnections.Remove(nodeName);
		}
	}
	
	void recieveInput (float incVoltage) {
		voltage += incVoltage;
		if (voltage > threshold) {
			StartCoroutine(generateSpike());	
		}
	}
	
	IEnumerator generateSpike() {
		

		voltage = restingState;
		//change the unit color
		renderer.material.color = Color.red;
		stayRed++;
		StartCoroutine(changeColorInSeconds(.75F));
		
		//propegate the spike
		if (outConnections.Count > 0) {
			yield return new WaitForSeconds(delay);
			foreach (string s in outConnections) {
				GameObject node = GameObject.Find(s);
				node.SendMessage("recieveInput", spikeValue);
			}
		}
		//yield return new WaitForSeconds(5.0F);
		if (gameObject.name.Equals("Unit4")) {
			GameObject camera = GameObject.Find ("Main Camera");
			camera.SendMessage("changeSong");
		}
		
	}
	
	IEnumerator changeColorInSeconds(float seconds){
		yield return new WaitForSeconds(seconds);
		stayRed--;
		if (stayRed == 0) {
			renderer.material.color = Color.black;
		}
	}
 }
