﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Vector3
{
	public float x=0;
	public float y=0;
	public float z=0;

	public Vector3(float _x, float _y, float _z)
	{
		x = _x;
		y = _y;
		z = _z;
	}
	
	public Vector3 ()
	{
		//zero vector;
	}
	
	public Vector3 Add(Vector3 tmpVect)
	{
		return new Vector3(x+tmpVect.x, y+tmpVect.y, z+tmpVect.z);
	}
	
	public Vector3 Add(float value)
	{
		return new Vector3(x + value, y + value, z + value);
	}

	public Vector3 Substract(Vector3 tmpVect)
	{
		return new Vector3(x - tmpVect.x, y - tmpVect.y, z - tmpVect.z);
	}
	
	public Vector3 Substract(float value)
	{
		return new Vector3(x - value, y - value, z - value);
	}
	
	public Vector3 Multiply(Vector3 tmpVect)
	{
		return new Vector3(x * tmpVect.x, y * tmpVect.y, z * tmpVect.z);
	}
	
	public Vector3 Multiply(float value)
	{
		return new Vector3(x * value, y * value, z * value);
	}
	
	public Vector3 invert()
	{
		return new Vector3(1f/x, 1f/y, 1f/z);
	}
	
	public Vector3 negative()
	{
		return new Vector3(-x, -y, -z);
	}

	public float Magnitude()
	{
		return Math.Abs(x)+Math.Abs(y)+Math.Abs(z);
	}

	public float SqrMagnitude()
	{
		return (float)Math.Sqrt((double)x * x + (double)y * y + (double)z * z);
	}

	public bool isZero()
	{
		return Magnitude()==0;
	}

	public Vector3 getNewInstance()
	{
		return new Vector3(x, y, z);
	}

	public String toString()
	{
		return x + "_" + y + "_" + z;
	}
	
	public String toPosRefId(float step)
	{
		return Math.floor(x/step)*step + "_" + Math.floor(y/step)*step + "_" + Math.floor(z/step)*step;
	}
	
	private float defaultStep = 1;
	public String toPosRefId()
	{
		return Math.floor(x/defaultStep)*defaultStep + "_" + Math.floor(y/defaultStep)*defaultStep + "_" + Math.floor(z/defaultStep)*defaultStep;
	}
}
