using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour {


	CameraTest cam;
	Renderer rend;
	MeshCollider col;


	//Vector3[] newVertices;
    //Vector2[] newUV;
    int[] newTriangles;

    public float speed;
    
    void Start()
    {
		cam = Camera.main.gameObject.GetComponent<CameraTest>();
		//from_rotation = Camera.main.transform.parent.rotation;
		//to_rotation = transform.rotation;

		rend = GetComponent<Renderer>();
		col = GetComponent<MeshCollider>();
        
        

        Mesh mesh = new Mesh();      
        mesh = GetComponent<MeshFilter>().mesh;
        
		//newVertices = mesh.vertices;
		//newUV = mesh.uv;
		newTriangles = mesh.triangles;

		float total = newTriangles.Length / 6f;
		Debug.Log("Total Squares: " + total.ToString());
    }
	

	void Update () {


		
	}


    void OnMouseDown() {

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      


        if (Physics.Raycast(ray, out hit))
        {         
            // Do something with the object that was hit by the raycast.
			cam.AdjustPath(Quaternion.LookRotation(hit.point));

			//Camera.main.transform.parent.rotation = rotation;
			Debug.Log("--" + hit.triangleIndex + ", " + hit.point);
        }
    }   
}
