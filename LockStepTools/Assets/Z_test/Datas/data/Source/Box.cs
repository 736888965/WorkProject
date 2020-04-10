using System;
using UnityEngine;
namespace MokeDataBase 
{
	[Serializable]
	public class Box
	{
		[SerializeField]
		private string name;
		public string Name { get { return  name; }  set { name = value ; } } 
		[SerializeField]
		private int age;
		public int Age { get { return  age; }  set { age = value ; } } 
		[SerializeField]
		private int gender;
		public int Gender { get { return  gender; }  set { gender = value ; } } 
	}
}