using System;
using System.Collections.Generic;
using UnityEngine;
namespace MokeDataBase 
{
	[Serializable]
	public class FallData  : DataBase
	{
		public FallData ()
		{
			fallList = new List<Fall>();
		}
		 [SerializeField]
		private List<Fall> fallList ;
		public List<Fall> Get() 
		{
			return  fallList;
		}
		public override void Set (List<object> list)
		{
			fallList.Clear();
			for (int i = 0; i < list.Count; i++)
				 fallList.Add((Fall)list[i]);
		}
	}
}