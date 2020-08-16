using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarchingCubes
{
    private Vector3[] _vertices;
    private int[] _triangles;
    private float _isolevel;

    private int _vertexIndex;

    private Vector3[] _vertexList;
    private Point[] _initPoints;
    private Mesh _mesh;
    private int[,,] _cubeIndexes;
    
    private readonly Vector3 zero = Vector3.zero;

    public MarchingCubes(Point[,,] points, float isolevel, int seed)
    {
        _isolevel = isolevel;

        _mesh = new Mesh();
        
        _vertexIndex = 0;

        _vertexList = new Vector3[12];
        _initPoints = new Point[8];
        _cubeIndexes = new int[points.GetLength(0)-1, points.GetLength(1)-1, points.GetLength(2)-1];
    }

    private Vector3 VertexInterpolate(Vector3 p1, Vector3 p2, float v1, float v2)
    {
        if (Utils.Abs(_isolevel - v1) < 0.000001f)
        {
            return p1;
        }
        if (Utils.Abs(_isolevel - v2) < 0.000001f)
        {
            return p2;
        }
        if (Utils.Abs(v1 - v2) < 0.000001f)
        {
            return p1;
        }

        float mu = (_isolevel - v1) / (v2 - v1);

        Vector3 p = p1 + mu * (p2 - p1);

        return p;
    }

    private void March(Point[] points, int cubeIndex)
    {
        int edgeIndex = LookupTables.EdgeTable[cubeIndex];

        _vertexList = GenerateVertexList(points, edgeIndex);

        int[] row = LookupTables.TriangleTable[cubeIndex];

        for (int i = 0; i < row.Length; i += 3)
        {
            _vertices[_vertexIndex] = _vertexList[row[i + 0]]; 
            _triangles[_vertexIndex] = _vertexIndex;
            _vertexIndex++;

            _vertices[_vertexIndex] = _vertexList[row[i + 1]];
            _triangles[_vertexIndex] = _vertexIndex;
            _vertexIndex++;

            _vertices[_vertexIndex] = _vertexList[row[i + 2]];
            _triangles[_vertexIndex] = _vertexIndex;
            _vertexIndex++;
        }
    }

    private Vector3[] GenerateVertexList(Point[] points, int edgeIndex)
    {
        for (int i = 0; i < 12; i++)
        {
            if ((edgeIndex & (1 << i)) != 0)
            {
                int[] edgePair = LookupTables.EdgeIndexTable[i];
                int edge1 = edgePair[0];
                int edge2 = edgePair[1];

                Point point1 = points[edge1];
                Point point2 = points[edge2];

                _vertexList[i] = VertexInterpolate(point1.localPosition, point2.localPosition, point1.density, point2.density);
            }
        }

        return _vertexList;
    }

    private int CalculateCubeIndex(Point[] points, float iso)
    {
        int cubeIndex = 0;

        for (int i = 0; i < 8; i++)
            if (points[i].density < iso)
                cubeIndex |= 1 << i;

        return cubeIndex;
    }

    public int roundness;
    private Vector3[] normals;

    public Mesh CreateMeshData(Point[,,] points)
    {
        _cubeIndexes = GenerateCubeIndexes(points);
        int vertexCount = GenerateVertexCount(_cubeIndexes);

        if (vertexCount <= 0)
        {
            return new Mesh();
        }

        _vertices = new Vector3[vertexCount];
        
        _triangles = new int[vertexCount];
        
        for (int x = 0; x < points.GetLength(0)-1; x++)
        {
            for (int y = 0; y < points.GetLength(1)-1; y++)
            {
                for (int z = 0; z < points.GetLength(2)-1; z++)
                {
                    int cubeIndex = _cubeIndexes[x, y, z];
                    if (cubeIndex == 0 || cubeIndex == 255) continue;

                    March(GetPoints(x, y, z, points), cubeIndex);
                }
            }
        }

        _vertexIndex = 0;

        _mesh.Clear();
        // for (v in vertices)

        _mesh.vertices = _vertices;
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();

        // int[] subdivision = new int[] {0,2,3,4,6,8,9,12,16,18,24};

        // MeshHelper.Subdivide(_mesh, subdivision[1]);

        // Vector3[] _normals = _mesh.normals;
        // for(int n = 0; n < _vertices.Length; n++){
        //     _normals[n] = calcNormal(_vertices[n]);
        // }
        NormalSolver.RecalculateNormals(_mesh,180f,0.5f);
        // _mesh.normals = _normals;

        return _mesh;
    }

           private Vector3 calcNormal(Vector3 pnt)
	{
		int jc;
		Vector3 result=pnt;
		// result.x=0;result.y=0;result.z=0;

			float pb=_isolevel;
 
			Vector3 current=pnt;
			current.x=pnt.x-_isolevel;
			current.y=pnt.y-_isolevel;
			current.z=pnt.z-_isolevel;
			float mag=current.magnitude;
			float pwr=.5f*(1f/(mag*mag*mag))*pb;			
			result=result+(current*pwr);			
		
		return result.normalized;
	}

    private void OnDrawGizmos () {
		if (_vertices == null) {
			return;
		}
		for (int i = 0; i < _vertices.Length; i++) {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(_vertices[i], 0.1f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawRay(_vertices[i], normals[i]);
		}
	}

    private Point[] GetPoints(int x, int y, int z, Point[,,] points)
    {
        for (int i = 0; i < 8; i++)
        {
            Point p = points[x + CubePointsX[i], y + CubePointsY[i], z + CubePointsZ[i]];
            _initPoints[i] = p;
        }

        return _initPoints;
    }

    private int[,,] GenerateCubeIndexes(Point[,,] points)
    {
        for (int x = 0; x < points.GetLength(0)-1; x++)
        {
            for (int y = 0; y < points.GetLength(1)-1; y++)
            {
                for (int z = 0; z < points.GetLength(2)-1; z++)
                {
                    _initPoints = GetPoints(x, y, z, points);

                    _cubeIndexes[x, y, z] = CalculateCubeIndex(_initPoints, _isolevel);
                }
            }
        }

        return _cubeIndexes;
    }

    private int GenerateVertexCount(int[,,] cubeIndexes)
    {
        int vertexCount = 0;

        for (int x = 0; x < cubeIndexes.GetLength(0); x++)
        {
            for (int y = 0; y < cubeIndexes.GetLength(1); y++)
            {
                for (int z = 0; z < cubeIndexes.GetLength(2); z++)
                {
                    int cubeIndex = cubeIndexes[x, y, z];
                    int[] row = LookupTables.TriangleTable[cubeIndex];
                    vertexCount += row.Length;
                }
            }
        }

        return vertexCount;
    }

    public static readonly Vector3Int[] CubePoints =
    {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, 0, 1),
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(1, 1, 1),
        new Vector3Int(0, 1, 1)
    };

    public static readonly int[] CubePointsX =
    {
        0,
        1,
        1,
        0,
        0,
        1,
        1,
        0
    };

    public static readonly int[] CubePointsY =
    {
        0,
        0,
        0,
        0,
        1,
        1,
        1,
        1
    };

    public static readonly int[] CubePointsZ =
    {
        0,
        0,
        1,
        1,
        0,
        0,
        1,
        1
    };
}