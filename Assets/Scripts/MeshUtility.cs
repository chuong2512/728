using System;
using System.Collections.Generic;
using UnityEngine;

public static class MeshUtility
{
	private static GameObject GetMeshGameObject(Material meshMaterial, Color color)
	{
		GameObject expr_05 = new GameObject();
		MeshRenderer arg_2C_0 = expr_05.AddComponent<MeshRenderer>();
		if (meshMaterial == null)
		{
			meshMaterial = new Material(Shader.Find("Diffuse"));
		}
		meshMaterial.color = color;
		arg_2C_0.material = meshMaterial;
		arg_2C_0.sharedMaterial = meshMaterial;
		expr_05.AddComponent<MeshFilter>();
		return expr_05;
	}

	public static GameObject DrawSectorGameObject(Material meshMaterial, Color color, float angle, float radiusLong, float radiusShort)
	{
		radiusLong = Mathf.Abs(radiusLong);
		radiusShort = Mathf.Abs(radiusShort);
		if (radiusLong < radiusShort)
		{
			float arg_1B_0 = radiusShort;
			radiusShort = radiusLong;
			radiusLong = arg_1B_0;
		}
		GameObject arg_2E_0 = MeshUtility.GetMeshGameObject(meshMaterial, color);
		Mesh mesh = MeshUtility.DrawSectorMesh(radiusLong, radiusShort, angle);
		arg_2E_0.GetComponent<MeshFilter>().mesh = mesh;
		return arg_2E_0;
	}

	public static Mesh DrawSectorMesh(float radiusLong, float radiusShort, float angle)
	{
		Mesh mesh = new Mesh();
		Matrix4x4 yAxisMatrix = MeshUtility.GetYAxisMatrix(1f);
		Vector3 vector = new Vector3(radiusShort, 0f, 0f);
		Vector3 vector2 = new Vector3(radiusLong, 0f, 0f);
		List<Vector3> list = new List<Vector3>();
		list.Add(vector);
		list.Add(vector2);
		int num = 0;
		while ((float)num < angle / 1f)
		{
			Vector3 vector3 = yAxisMatrix.MultiplyVector(vector);
			Vector3 vector4 = yAxisMatrix.MultiplyVector(vector2);
			list.Add(vector3);
			list.Add(vector4);
			vector = vector3;
			vector2 = vector4;
			num++;
		}
		List<Vector3> list2 = new List<Vector3>();
		List<int> list3 = new List<int>();
		for (int i = 2; i < list.Count; i++)
		{
			list2.Add(list[i - 2]);
			list2.Add(list[i - 1]);
			list2.Add(list[i]);
		}
		int num2 = list2.Count / 3;
		for (int j = 0; j < num2; j++)
		{
			if (j % 2 == 0)
			{
				list3.Add(j * 3);
				list3.Add(j * 3 + 1);
				list3.Add(j * 3 + 2);
			}
			else
			{
				list3.Add(j * 3 + 2);
				list3.Add(j * 3 + 1);
				list3.Add(j * 3);
			}
		}
		mesh.vertices = list2.ToArray();
		mesh.triangles = list3.ToArray();
		mesh.uv = new Vector2[list2.Count];
		for (int k = 0; k < list2.Count; k++)
		{
			mesh.uv[k] = new Vector2(list2[k].x, list2[k].y).normalized;
		}
		mesh.RecalculateNormals();
		return mesh;
	}

	public static GameObject DrawCircleGameObject(Material meshMaterial, float radius)
	{
		GameObject arg_12_0 = MeshUtility.GetMeshGameObject(meshMaterial, Color.red);
		Mesh mesh = MeshUtility.DrawCircleMesh(radius);
		arg_12_0.GetComponent<MeshFilter>().mesh = mesh;
		return arg_12_0;
	}

	public static Mesh DrawCircleMesh(float radius)
	{
		Mesh mesh = new Mesh();
		Matrix4x4 yAxisMatrix = MeshUtility.GetYAxisMatrix(1f);
		Vector3 vector = new Vector3(radius, 0f, 0f);
		Vector3 zero = Vector3.zero;
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < 360; i++)
		{
			Vector3 vector2 = yAxisMatrix.MultiplyVector(vector);
			list2.Add(list.Count);
			list.Add(zero);
			list2.Add(list.Count);
			list.Add(vector);
			list2.Add(list.Count);
			list.Add(vector2);
			vector = vector2;
		}
		mesh.vertices = list.ToArray();
		mesh.triangles = list2.ToArray();
		mesh.uv = new Vector2[list.Count];
		for (int j = 0; j < list.Count; j++)
		{
			mesh.uv[j] = Vector2.zero;
		}
		mesh.RecalculateNormals();
		return mesh;
	}

	public static Matrix4x4 GetYAxisMatrix(float angle)
	{
		return new Matrix4x4
		{
			m00 = Mathf.Cos(angle / 180f * 3.14159274f),
			m20 = -Mathf.Sin(angle / 180f * 3.14159274f),
			m11 = 1f,
			m02 = Mathf.Sin(angle / 180f * 3.14159274f),
			m22 = Mathf.Cos(angle / 180f * 3.14159274f)
		};
	}

	public static Matrix4x4 GetZAxisMatrix(float angle)
	{
		return new Matrix4x4
		{
			m00 = Mathf.Cos(angle / 180f * 3.14159274f),
			m10 = Mathf.Sin(angle / 180f * 3.14159274f),
			m01 = -Mathf.Sin(angle / 180f * 3.14159274f),
			m11 = Mathf.Cos(angle / 180f * 3.14159274f),
			m22 = 1f
		};
	}

	public static GameObject Draw6EdgeGameObject(Material meshMaterial, float gridSize)
	{
		if (meshMaterial == null)
		{
			meshMaterial = new Material(Shader.Find("Diffuse"));
		}
		GameObject expr_1F = new GameObject();
		expr_1F.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
		MeshRenderer expr_44 = expr_1F.AddComponent<MeshRenderer>();
		expr_44.material = meshMaterial;
		expr_44.sharedMaterial = meshMaterial;
		MeshFilter arg_5F_0 = expr_1F.AddComponent<MeshFilter>();
		Mesh mesh = MeshUtility.Draw6DirectionMesh(gridSize);
		arg_5F_0.mesh = mesh;
		expr_1F.transform.localEulerAngles = Vector3.zero;
		return expr_1F;
	}

	public static Mesh Draw6DirectionMesh(float gridSize)
	{
		Mesh mesh = new Mesh();
		float num = Mathf.Sin(1.04719758f);
		int[] array = new int[18];
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, gridSize, 0f),
			new Vector3(num * gridSize, 0.5f * gridSize, 0f),
			new Vector3(num * gridSize, -0.5f * gridSize, 0f),
			new Vector3(0f, -gridSize, 0f),
			new Vector3(-num * gridSize, -0.5f * gridSize, 0f),
			new Vector3(-num * gridSize, 0.5f * gridSize, 0f)
		};
		array[0] = 0;
		array[1] = 1;
		array[2] = 2;
		array[3] = 0;
		array[4] = 2;
		array[5] = 3;
		array[6] = 0;
		array[7] = 3;
		array[8] = 4;
		array[9] = 0;
		array[10] = 4;
		array[11] = 5;
		array[12] = 0;
		array[13] = 5;
		array[14] = 6;
		array[15] = 0;
		array[16] = 6;
		array[17] = 1;
		mesh.vertices = vertices;
		mesh.triangles = array;
		Vector2 vector = new Vector2(0f, 0f);
		Vector2 vector2 = new Vector2(0f, 1f);
		Vector2 vector3 = new Vector2(num, 0.5f);
		Vector2 vector4 = new Vector2(num, -0.5f);
		Vector2 vector5 = new Vector2(0f, -1f);
		Vector2 vector6 = new Vector2(-num, -0.5f);
		Vector2 vector7 = new Vector2(-num, 0.5f);
		mesh.uv = new Vector2[]
		{
			vector,
			vector2,
			vector3,
			vector4,
			vector5,
			vector6,
			vector7
		};
		mesh.RecalculateNormals();
		return mesh;
	}

	public static List<GameObject> Draw6DirectionGrid(int width, int height, Material meshMaterial, float gridSize)
	{
		List<GameObject> list = new List<GameObject>();
		float num = Mathf.Sin(1.04719758f);
		for (int i = 0; i < height; i++)
		{
			float y = gridSize + gridSize * 1.5f * (float)i;
			for (int j = 0; j < width; j++)
			{
				float num2 = (num + num * 2f * (float)j) * gridSize;
				if (i % 2 != 0)
				{
					num2 += num;
				}
				GameObject gameObject = MeshUtility.Draw6EdgeGameObject(meshMaterial, gridSize);
				gameObject.transform.localScale = Vector3.one * 0.9f;
				gameObject.transform.position = new Vector2(num2, y);
				list.Add(gameObject);
			}
		}
		return list;
	}
}
