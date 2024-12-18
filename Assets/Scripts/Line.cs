using System;

public class Line
{
	private int _startVertexIndex;

	private int _endVertexIndex;

	private int _vertexCount;

	public int StartVertexIndex
	{
		get
		{
			return this._startVertexIndex;
		}
	}

	public int EndVertexIndex
	{
		get
		{
			return this._endVertexIndex;
		}
	}

	public int VertexCount
	{
		get
		{
			return this._vertexCount;
		}
	}

	public Line(int startVertexIndex, int length)
	{
		this._startVertexIndex = startVertexIndex;
		this._endVertexIndex = length * 6 - 1 + startVertexIndex;
		this._vertexCount = length * 6;
	}
}
