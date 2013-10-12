using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntegrateAndFire : MonoBehaviour {
	
	public float voltage = 0;
	public float restingState = 0;
	public float threshold = 100;
	public float spikeValue = 75;
	public float delay = 0.25F;
	protected int stayRed = 0;
	private List<string> outConnections = new List<string>();
	
	// Use this for initialization
	public virtual void Start () {	
		renderer.material.color = Color.black;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}
	
	protected virtual void addConnection(string nodeName) {
		if (!outConnections.Contains(nodeName) && name != nodeName) {
			outConnections.Add(nodeName);	
		}
		else {
			print ("Connection already exists or is invalid.");	
		}
	}
	
	void removeConnection(string nodeName) {
		if (outConnections.Contains(nodeName)) {
			outConnections.Remove(nodeName);
		}
		else {
			print ("No such connection");	
		}
	}
	
	void beingAdded() {
		StartCoroutine(flashBlue(0.25F));	
	}
	
	void recieveInput (float incVoltage) {
		voltage += incVoltage;
		if (voltage > threshold) {
			StartCoroutine(generateSpike());	
		}
		else {
			StartCoroutine (flashWhite(0.25F));	
		}
	}
	
	protected virtual IEnumerator generateSpike() {
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
	}
	
	protected IEnumerator changeColorInSeconds(float seconds){
		yield return new WaitForSeconds(seconds);
		stayRed--;
		if (stayRed == 0) {
			renderer.material.color = Color.black;
		}
	}
	
	IEnumerator flashWhite(float seconds) {
		renderer.material.color = Color.white;
		yield return new WaitForSeconds(seconds);
		if (renderer.material.color == Color.white) {
			renderer.material.color = Color.black;	
		}
	}
	
	IEnumerator flashBlue(float seconds) {
		renderer.material.color = Color.blue;
		yield return new WaitForSeconds(seconds);
		if (renderer.material.color == Color.blue) {
			renderer.material.color = Color.black;	
		}
	}
 }
