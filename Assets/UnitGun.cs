using UnityEngine;
using System.Collections;

public class UnitGun : MonoBehaviour {
	public GameObject Node;
	public float volts = 50; //amount of voltage to add with one click
	public Material connectionMaterial;
	private int nodeCount = 0;
	private GameObject makingConnection = null;
	private GameObject mainCam;
	private GameObject secondCam;
	
	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find ("Main Camera");
		secondCam = GameObject.Find ("Camera");
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetButtonDown("Fire1"))
//		{
//			//generateNode(transform.position + (transform.forward + mainCam.transform.forward) * 2);
//			Vector3 pos = mainCam.camera.ScreenToWorldPoint(new Vector3(mainCam.camera.pixelWidth / 2, mainCam.camera.pixelHeight / 2, 2));
//			generateNode(pos);
//			
//		}
		
		if (Input.GetButtonDown("Fire1"))
		{
			mainCam.SetActive(false);
			secondCam.SetActive(true);
			
		}
		
		if (Input.GetButtonDown("Fire2"))
		{
			RaycastHit hit;
			//if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) + 
			//		mainCam.transform.TransformDirection(Vector3.forward), out hit))
			if (Physics.Raycast(mainCam.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), out hit))
			{
				GameObject target = GameObject.Find (hit.transform.name);
				if (target == null) {
					print ("Invalid Target");	
					return;
				}
				if(target.tag == "Unit") {
					target.SendMessage("beingAdded");
					if (makingConnection == null){
						makingConnection = target;
						print ("starting connection: " + target.name);
					}
					else {
						//if shift is held down remove the connection
						if (Input.GetKey("left shift")) {
							removeConnection(makingConnection, target);	
							print ("removing connection: " + target.name);
						}
						else {
							finishConnection(makingConnection, target);
							print ("Connection Added: " + target.name);
						}
							makingConnection = null;
					}
				}
			}
		}
		
		if (Input.GetButtonDown("Jump")) {
			RaycastHit hit;
			if (Physics.Raycast(mainCam.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), out hit)) {
				GameObject target = GameObject.Find (hit.transform.name);
				if (target == null) {
					print ("Invalid Target");	
					return;
				}
				if(target.tag == "Unit") {
					print ("Sending Volts");
					target.SendMessage("recieveInput", volts);
				}
			}
		}
	}
	
	void generateNode(Vector3 spot) {
		GameObject newNode = (GameObject)Instantiate(Node, spot, Quaternion.identity);
		newNode.name = "Node" + nodeCount;
		nodeCount++;
	}
	
	void finishConnection(GameObject UnitA, GameObject UnitB) {
		UnitA.SendMessage("addConnection", UnitB.name);
		drawConnectionLine(UnitA, UnitB);
	}
	
	void removeConnection(GameObject UnitA, GameObject UnitB) {
		UnitA.SendMessage("removeConnection", UnitB.name);
		removeConnectionLine(UnitA, UnitB);
	}
	
	void drawConnectionLine(GameObject UnitA, GameObject UnitB) {
		GameObject connectionLine = new GameObject();
		connectionLine.name = UnitA.name + "x" + UnitB.name + "Cxn";
		LineRenderer lineRenderer = connectionLine.AddComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, UnitA.transform.position);
		lineRenderer.SetPosition(1, UnitB.transform.position);
		lineRenderer.SetWidth(0.5F, 0.5F);
		lineRenderer.material = connectionMaterial;
		lineRenderer.material.color = Color.yellow;
		
		connectionLine.AddComponent("ConnectionPosition");
		GameObject[]tempArray = {UnitA, UnitB};
		connectionLine.SendMessage("setUnits", tempArray);
		
	}
	
	void removeConnectionLine(GameObject UnitA, GameObject UnitB) {
		GameObject connectionLine = GameObject.Find (UnitA.name + "x" + UnitB.name + "Cxn");	
		if (connectionLine == null) {
			return;
		}
		Destroy(connectionLine);
	}
}
