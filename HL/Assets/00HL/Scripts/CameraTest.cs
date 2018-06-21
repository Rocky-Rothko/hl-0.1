using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{   
	//Quaternion from_rotation;
	Quaternion target;
	float speed = 25f;
	float step = 0f;

	void Start()
	{

	}

	void Update()
	{
		step = speed * Time.deltaTime;
        transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, target, step);
	}
    
	public void AdjustPath(Quaternion rot) 
	{
		target = rot;
	}
}
