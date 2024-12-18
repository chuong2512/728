using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Serialization<T>
{
	[SerializeField]
	private List<T> target;

	public List<T> ToList()
	{
		return this.target;
	}

	public Serialization(List<T> target)
	{
		this.target = target;
	}
}
