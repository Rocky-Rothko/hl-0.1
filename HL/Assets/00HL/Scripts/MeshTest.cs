using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshTest : MonoBehaviour {
	
	public GameObject field;

	CameraTest cam;

	void Start() {
		
		cam = Camera.main.gameObject.
                GetComponent<CameraTest>();

		TerrainHelper.SetTerrainData(
			    GetComponent<MeshFilter>().mesh );
	}

	//------------------------ events -------------------------
	void OnMouseDown() {
		
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			cam.AdjustPath(Quaternion.LookRotation( hit.point ) );

			int[] selected_quad = TerrainHelper.TriangleIndices( hit.triangleIndex );

			string debug_text = hit.triangleIndex.ToString() + ": "
			        + selected_quad[ 0 ].ToString() + ", "
			        + selected_quad[ 1 ].ToString();         

			Debug.Log( debug_text );

		}
	}

}

public static class TerrainHelper {
	
	static int[] tris;
	static Vector3[] verts;
	static Vector3[] norms;
	static Vector2[] uvs;
	static Quadrant[] quads;

	public static void SetTerrainData( Mesh world ) {

		tris = world.triangles;
		verts = world.vertices;
		norms = world.normals;
		uvs = world.uv;

        quads = GenerateQuadData();
	}

	public static int[] TriangleIndices( int tri ) {

		int[] tri_indices = { -2, -4 };

		for (int q = 0; q < quads.Length; q++) {

			if (quads[q].Contains(tri))
			{            
				tri_indices[0] = quads[q].Triangle1();
				tri_indices[1] = quads[q].Triangle2();

				break; //?
			}
		}

		return tri_indices;
    }
    
	static Quadrant[] GenerateQuadData() {
      
		int quad_quantity = tris.Length / 6;
		Quadrant[] quad_data = new Quadrant[ quad_quantity ];

        int tri_1;
        int tri_2;
		List<int> added_tris = new List<int>();
        int tri_index;
        Vector3[] points;
        float[] lengths;
        float max_value;
        int factor;
        int[] hypotenuse = new int[ 2 ];
		int placement = 0;

		for( int quad = 0; quad < quad_quantity; quad++ ) {
			
			tri_1 = placement;
            
			if( added_tris.Contains( tri_1 ) ) {

				tri_1++;

				while ( added_tris.Contains(tri_1) ) {

					tri_1++;
				}
			}
            placement = tri_1 + 1;         
            tri_index = tri_1 * 3;

            points = GetTrianglePoints( tri_index );
            lengths = GetTriangleLengths( points );

            max_value = lengths.Max();
            factor = System.Array.IndexOf( lengths, max_value );

            hypotenuse[ 0 ] = tris[ tri_index ];
            hypotenuse[ 1 ] = tris[ tri_index + factor ];
            tri_2 = GetQuadTwinIndex( tri_index, hypotenuse );

            if (tri_2 != -1)
            {
                added_tris.Add( tri_1 );
                added_tris.Add( tri_2 );
                quad_data[quad] = new Quadrant( tri_1, tri_2 );
            }
		}

		return quad_data;
    }
    
    static Vector3[] GetTrianglePoints( int _t ) {
		
		Vector3[] _points = { verts[ tris[ _t ] ]
			    , verts[ tris[ _t + 1 ] ]
			    , verts[ tris[ _t + 2 ] ] };

		return _points;
	}

	static float[] GetTriangleLengths( Vector3[] _p ) {
		
	    float[] _lengths = { Vector3.Distance( _p[ 1 ], _p[ 2 ] )
	            , Vector3.Distance( _p[ 0 ], _p[ 1 ] )
	            , Vector3.Distance( _p[ 0 ], _p[ 2 ] ) };

		return _lengths;
	}

	static int GetQuadTwinIndex( int _i, int[] _h ) {
		
		int _t2 = -1;
		int[] _verts = new int[3];     

		for( int twin = 0; twin < tris.Length; twin+=3 ) {
			
			_verts[0] = tris[ twin ];
			_verts[1] = tris[ twin + 1 ];
			_verts[2] = tris[ twin + 2 ];

			if ( _verts.Contains(_h[0]) && _verts.Contains(_h[1])
			        && twin != _i ) {
				
				_t2 = twin / 3;            
				break;
			}
        }

		return _t2;
    }

	class Quadrant {
		
		readonly int triangle1;
		readonly int triangle2;

		public Quadrant(int _t1, int _t2) {
			
			this.triangle1 = _t1;
			this.triangle2 = _t2;
        }

        public int Triangle1() {
			
			return this.triangle1;
        }
        
        public int Triangle2() {
			
			return this.triangle2;
        }

		public bool Contains( int _t ) {

			return this.triangle1 == _t || this.triangle2 == _t;
		}
	}

}