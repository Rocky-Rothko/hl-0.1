using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class MeshTest : MonoBehaviour {

	//public MeshFilter _plane;
	public GameObject _a;
	public GameObject _b;
	public GameObject _c;
	public GameObject _d;
    public GameObject _e;
    public GameObject _f;
	int[] quad_pair;

	CameraTest cam;
	Renderer rend;
	MeshCollider col;

	int[] tris;
	Vector3[] verts;
	Vector2[] uvs;

	void Start()
	{
		cam = Camera.main.gameObject.GetComponent<CameraTest>();
		rend = GetComponent<Renderer>();
		col = GetComponent<MeshCollider>();

		//_plane.GetComponent<Plane>      
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		tris = mesh.triangles;
		verts = mesh.vertices;
		uvs = mesh.uv;

	}

	void InstantiateField ( int hit_triangle_index ) {

		int[] tri_indices = GetHitIndices( hit_triangle_index );      
		float[] tri_sides = CalculateVertDistances( tri_indices );    
		int tri_type = GetTriOrientation( tri_sides );

		int[] tri_hypo = new int[2];
		switch (tri_type)
        {
            case 0:
				tri_hypo[0] = tri_indices[0];
				tri_hypo[1] = tri_indices[1];
                break;
			case 1:
                tri_hypo[0] = tri_indices[2];
                tri_hypo[1] = tri_indices[0];
                break;
            default:
				Debug.LogError("Polygon unrecognised!");
				break;
        }

		IdentifyQuadPair(tri_hypo, hit_triangle_index);

		_a.transform.position = verts[ tris[ quad_pair[ 0 ] ] ];
		_b.transform.position = verts[ tris[ quad_pair[ 0 ] + 1 ] ];
		_c.transform.position = verts[ tris[ quad_pair[ 0 ] + 2 ] ];
        _d.transform.position = verts[ tris[ quad_pair[ 1 ] ] ];
        _e.transform.position = verts[ tris[ quad_pair[ 1 ] + 1 ] ];
        _f.transform.position = verts[ tris[ quad_pair[ 1 ] + 2 ] ];
		Debug.Log("T1: " + quad_pair[0] + ", T2: " + quad_pair[1]);
		
	}

	int[] GetHitIndices(int _hit)
	{
		int[] _indices = new int[3];
		_indices[0] = tris[ _hit ];
		_indices[1] = tris[ _hit + 1 ];
		_indices[2] = tris[ _hit + 2 ];
        
		return _indices;
	}
    
	float[] CalculateVertDistances(int[] _tris)
    {
		float[] _sides = new float[3];
		_sides[0] = Vector3.Distance(verts[_tris[0]]
		                               , verts[_tris[1]]);
		_sides[1] = Vector3.Distance(verts[_tris[2]]
                                       , verts[_tris[0]]);
		_sides[2] = Vector3.Distance(verts[_tris[2]]
                                       , verts[_tris[1]]);

		return _sides;
	}

	int GetTriOrientation(float[] _lengths)
	{
		float max_value = _lengths.Max();
		int max_index = Array.IndexOf(_lengths, max_value);

		return max_index;
	}

	void IdentifyQuadPair(int[] _hypo, int _hit)
	{
        bool h1_found;
        bool h2_found;
        int vert_index;

        for (int t = 0; t < tris.Length; t += 3)
        {
            h1_found = false;
            h2_found = false;
            
            for (int v = 0; v < 3; v++)
            {
                vert_index = t + v;
				if (_hypo[0] == tris[vert_index]) { h1_found = true; }
				if (_hypo[1] == tris[vert_index]) { h2_found = true; }
            }

            if (h1_found && h2_found && t != _hit)
            {
				quad_pair = new int[2] { _hit, t };
            }
        }
	}
    
    //------------------------ events -------------------------
    void OnMouseDown() {

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
			cam.AdjustPath( Quaternion.LookRotation( hit.point ) );
         
			InstantiateField( hit.triangleIndex * 3 );
        }
    }
}
