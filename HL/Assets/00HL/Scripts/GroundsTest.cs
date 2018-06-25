using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundsTest : MonoBehaviour {

	Mesh mesh;

	void Start () {

		mesh = GetComponent<MeshFilter>().mesh;
		
	}
    
	//void Update () {
		
	//}
    
	public void MoveField (Vector3[] b, Vector3 n) {





                
		//mesh.vertices = b;


		//int[] tris = new int[6];

  //      //  Lower left triangle.
		//tris[0] = 0;
		//tris[1] = 2;
		//tris[2] = 1;

  //      //  Upper right triangle.   
		//tris[3] = 2;
		//tris[4] = 3;
		//tris[5] = 1;
        
		//mesh.triangles = tris;

		//Vector3[] normals = new Vector3[4];
        
  //      normals[0] = n;
  //      normals[1] = n;
  //      normals[2] = n;
  //      normals[3] = n;

  //      mesh.normals = normals;

		//GetComponent<MeshFilter>().mesh = mesh;
	}
    
}
