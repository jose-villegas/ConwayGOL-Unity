using OpenLife.World.Interface;
using UnityEngine;

namespace OpenLife.World
{
	public class World<T> : IWorld<T>
	{
		private Vector3Int _size;
		private T[,,] _cells;

		public T this[int x, int y, int z] { get => _cells[x, y, z]; set { _cells[x, y, z] = value; } }

		public Vector3Int Size { get => _size; }

		public World(int x, int y, int z)
		{
			_size = new Vector3Int(x, y, z);
			_cells = new T[x, y, z];
		}
	}
}