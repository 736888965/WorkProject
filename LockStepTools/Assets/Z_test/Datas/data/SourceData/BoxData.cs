using System;
using System.Collections.Generic;
using UnityEngine;
namespace MokeDataBase 
{
	[Serializable]
	public class BoxData  : DataBase
	{
		public BoxData ()
		{
			boxList = new List<Box>();
		}
		 [SerializeField]
		private List<Box> boxList ;
		public List<Box> Get() 
		{
			return  boxList;
		}
		public override void Set (List<object> list)
		{
			boxList.Clear();
			for (int i = 0; i < list.Count; i++)
				 boxList.Add((Box)list[i]);
		}
	}
}