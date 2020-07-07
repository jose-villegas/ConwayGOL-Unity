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
		public GameObject[] Trail { get; set; }

		private int _timesKilled = 0;

		public bool IsAlive()
		{
			return _cell.IsAlive();
		}

		public void Kill()
		{
			if (Instance != null)
			{
				Instance.SetActive(false);
			}

			_cell.Kill();
			_timesKilled++;

			if (Trail != null && Trail.Length > 1 && _timesKilled >= 1)
			{
				for (int i = 0; i < Trail.Length; i++)
				{
					Trail[i].SetActive(_timesKilled == i + 1);
				}
			}
		}

		public void Generate()
		{
			if (Instance != null)
			{
				Instance.SetActive(true);
			}

			_cell.Generate();
			_timesKilled = 0;
		}
	}
}
