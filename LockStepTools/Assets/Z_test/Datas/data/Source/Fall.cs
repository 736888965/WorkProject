using System;
using UnityEngine;
namespace MokeDataBase 
{
	[Serializable]
	public class Fall
	{
		[SerializeField]
		private int id;
		public int Id { get { return  id; }  set { id = value ; } } 
		[SerializeField]
		private string city;
		public string City { get { return  city; }  set { city = value ; } } 
		[SerializeField]
		private float distance;
		public float Distance { get { return  distance; }  set { distance = value ; } } 
	}
}