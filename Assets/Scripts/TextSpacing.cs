using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/TextSpacing")]
public class TextSpacing : BaseMeshEffect
{
	public float _textSpacing = 1f;

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive() || vh.currentVertCount == 0)
		{
			return;
		}
		Text component = base.GetComponent<Text>();
		if (component == null)
		{
			UnityEngine.Debug.LogError("Missing Text component");
			return;
		}
		List<UIVertex> list = new List<UIVertex>();
		vh.GetUIVertexStream(list);
		int arg_3F_0 = vh.currentIndexCount;
		string[] array = component.text.Split(new char[]
		{
			'\n'
		});
		Line[] array2 = new Line[array.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			if (i == 0)
			{
				array2[i] = new Line(0, array[i].Length + 1);
			}
			else if (i > 0 && i < array2.Length - 1)
			{
				array2[i] = new Line(array2[i - 1].EndVertexIndex + 1, array[i].Length + 1);
			}
			else
			{
				array2[i] = new Line(array2[i - 1].EndVertexIndex + 1, array[i].Length);
			}
		}
		for (int j = 0; j < array2.Length; j++)
		{
			for (int k = array2[j].StartVertexIndex + 6; k <= array2[j].EndVertexIndex; k++)
			{
				if (k >= 0 && k < list.Count)
				{
					UIVertex uIVertex = list[k];
					uIVertex.position += new Vector3(this._textSpacing * (float)((k - array2[j].StartVertexIndex) / 6), 0f, 0f);
					list[k] = uIVertex;
					if (k % 6 <= 2)
					{
						vh.SetUIVertex(uIVertex, k / 6 * 4 + k % 6);
					}
					if (k % 6 == 4)
					{
						vh.SetUIVertex(uIVertex, k / 6 * 4 + k % 6 - 1);
					}
				}
			}
		}
	}
}
