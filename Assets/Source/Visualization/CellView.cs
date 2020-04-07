using OpenLife.Life;
using OpenLife.Life.Interface;
using System;
using UnityEngine;

namespace OpenLife.Visualization
{
	public class CellView : ICell
	{
		private Cell _cell = new Cell();
		public GameObject Instance { get; set; }

		public bool IsAlive()
		{
			return _cell.IsAlive();
		}

		public void Kill()
		{
			if (Instance != null) Instance.SetActive(false);

			_cell.Kill();
		}

		public void Generate()
		{
			if (Instance != null) Instance.SetActive(true);

			_cell.Generate();
		}
	}
}
