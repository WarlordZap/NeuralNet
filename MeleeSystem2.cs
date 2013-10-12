using UnityEngine;
using System.Collections;

public class MeleeSystem2 : MonoBehaviour {

	public int damage = 50;
	public float distance;
		
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
			{
				distance = hit.distance;
				hit.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

}
