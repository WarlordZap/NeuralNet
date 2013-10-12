using UnityEngine;
using System.Collections;

public class JumpBox : IntegrateAndFire {
	public float force = 1000;
	
	
	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
	
	protected override void addConnection(string nodeName) {
		print("Can't add connections to output.");
		return;
	}
	
	protected override IEnumerator generateSpike() {
		voltage = restingState;
		//change the unit color
		renderer.material.color = Color.red;
		stayRed++;
		StartCoroutine(changeColorInSeconds(.75F));
		
		rigidbody.AddForce(Vector3.up * force);		
		
		yield return new WaitForSeconds(0);
	}
}
