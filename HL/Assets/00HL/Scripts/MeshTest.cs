using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour {
    
	public GameObject field;
    
    int[] tris;
	Vector3[] verts;
	Vector3[] norms;
	Vector2[] uvs;
    
	CameraTest cam;
    
	readonly int[] quad_keys = { 0, 1, 4, 5 };
	readonly int[] tri_keys = { 0, 1, 2, 0, 2, 3 };

	void Start() {

        cam = Camera.main.gameObject.GetComponent<CameraTest>();

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        tris = mesh.triangles;
        verts = mesh.vertices;
		norms = mesh.normals;
        uvs = mesh.uv;
	}

    void InstantiateField( int _hit)
    {
		int mod = _hit % 2;
		int tri_even = _hit - mod;
		int tri_index = tri_even * 3;
        
		int vert_index = -1;
		Vector3[] quad_verts = new Vector3[ 4 ];
		Vector3[] quad_norms = new Vector3[ 4 ];
        Vector2[] quad_uvs = new Vector2[ 4 ];

		Vector3 target = Vector3.zero;
		for (int v = 0; v < 4; v++)
		{
			vert_index = tris[ tri_index + quad_keys[ v ] ];

			quad_verts[ v ] = verts[ vert_index ];
			quad_norms[ v ] = norms[ vert_index ];
			quad_uvs[ v ] = uvs[ vert_index ];

			target += verts[vert_index];

		}

		Mesh quad_mesh = field.GetComponent<MeshFilter>().mesh;
		quad_mesh.vertices = quad_verts;
		quad_mesh.triangles = tri_keys;
		quad_mesh.normals = quad_norms;
		quad_mesh.uv = quad_uvs;

		field.GetComponent<MeshFilter>().mesh = quad_mesh;
		field.transform.GetChild(0).position = target / 4;

    }

    
    //------------------------ events -------------------------
    void OnMouseDown() {

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
			cam.AdjustPath( Quaternion.LookRotation( hit.point ) );

			InstantiateField( hit.triangleIndex );

        }
    }
}