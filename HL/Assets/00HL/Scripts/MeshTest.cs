using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour {

	//public GameObject field;
	public GameObject _a;
	public GameObject _b;
	public GameObject _c;
	//public GameObject _d;


	CameraTest cam;
	Renderer rend;
	MeshCollider col;

    int[] worldTriangles;
	Vector3[] worldVertices;
	Vector2[] worldUV;

	float speed = 20f;
    
    void Start()
    {
		cam = Camera.main.gameObject.GetComponent<CameraTest>();
        rend = GetComponent<Renderer>();
		col = GetComponent<MeshCollider>();

        Mesh mesh = GetComponent<MeshFilter>().mesh;      
		worldTriangles = mesh.triangles;
		worldVertices = mesh.vertices;
		worldUV = mesh.uv;

		float total = worldTriangles.Length / 6f;

		//Debug.Log(worldVertices.Length);
    }
	

	//void Update () {}


    void OnMouseDown() {

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
			cam.AdjustPath(Quaternion.LookRotation(hit.point));

			int quad_index = hit.triangleIndex * 3;
			//int quad_index = hit.triangleIndex * 3;
			//if (quad_index % 2 != 0)
            //{
				//quad_index = quad_index - 1;
            //}

			_a.transform.position = worldVertices[worldTriangles[quad_index]];
			_b.transform.position = worldVertices[worldTriangles[quad_index + 1]];
			_c.transform.position = worldVertices[worldTriangles[quad_index + 2]];
			//_d.transform.position = worldVertices[worldTriangles[quad_index + 5]];


			//Vector3[] bounds = new Vector3[4];
			//bounds[0] = worldVertices[worldTriangles[quad_index]];
			//bounds[1] = worldVertices[worldTriangles[quad_index + 3]];
			//bounds[2] = worldVertices[worldTriangles[quad_index + 4]];
			//bounds[3] = worldVertices[worldTriangles[quad_index + 5]];
			//field.GetComponent<GroundsTest>().MoveField(bounds, hit.normal);

        }
    }   
}
