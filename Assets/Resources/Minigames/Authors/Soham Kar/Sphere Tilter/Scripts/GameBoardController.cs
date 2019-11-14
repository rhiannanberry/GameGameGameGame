using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{
	void FixedUpdate () 
	{

		float horRot = (Input.GetAxis("Vertical")) * Time.deltaTime * -20;
		float vertRot = (Input.GetAxis("Horizontal")) * Time.deltaTime * 20;

		float totalHorRot;
		float totalVertRot;

		if (transform.eulerAngles.x + horRot < 350) 
		{
			totalHorRot = transform.eulerAngles.x + horRot;
		} 

		else 
		{
			totalHorRot = transform.eulerAngles.x + horRot - 360;
		}

		if (transform.eulerAngles.z + vertRot < 350) 
		{
			totalVertRot = transform.eulerAngles.z + vertRot;
		} 

		else 
		{
			totalVertRot = transform.eulerAngles.z + vertRot - 360;
		}

		transform.rotation = Quaternion.Euler(Mathf.Clamp(totalHorRot, -9f, 9f), 0, Mathf.Clamp(totalVertRot, -9f, 9f));
	}
}
