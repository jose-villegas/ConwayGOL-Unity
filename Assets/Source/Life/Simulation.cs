using OpenLife.Life.Interface;
using OpenLife.World;
using System;
using System.Threading.Tasks;

namespace OpenLife.Life
{
	public class Simulation<T> where T : ICell, new()
	{
		private World.World<T> _nextGeneration;
		private World.World<T> _world;

		public int Generations { get; private set; }
		public Action<World.World<T>> NextGenerationCompleted { get; private set; }
		public World<T> World { get => _world; }
		public World<T> NextGeneration { get => _nextGeneration; }

		public void Start(World.World<T> world)
		{
			_nextGeneration = new World.World<T>(world.Size.x, world.Size.y, world.Size.z);
			_world = world;

			// start first generation
			this.ProcessGeneration();
		}

		public void Tick()
		{
			// when a generation has completed
			// now flip the back buffer so we can start processing on the next generation
			var flip = this._nextGeneration;
			this._nextGeneration = this._world;
			this._world = flip;
			Generations++;

			// begin the next generation's processing
			this.ProcessGeneration();

			NextGenerationCompleted?.Invoke(this._world);
		}

		private int IsNeighborAlive(int x, int y, int z, int offsetX, int offsetY, int offsetZ)
		{
			int result = 0;

			int proposedOffsetX = x + offsetX;
			int proposedOffsetY = y + offsetY;
			int proposedOffsetZ = z + offsetZ;
			bool outOfBounds = proposedOffsetX < 0 || proposedOffsetX >= _world.Size.x |
							   proposedOffsetY < 0 || proposedOffsetY >= _world.Size.y |
							   proposedOffsetZ < 0 || proposedOffsetZ >= _world.Size.z;

			if (!outOfBounds)
			{
				result = _world[x + offsetX, y + offsetY, z + offsetZ].IsAlive() ? 1 : 0;
			}

			return result;
		}

		private void ProcessGeneration()
		{
			for (int x = 0; x < _world.Size.x; x++)
			{
				for (int y = 0; y < _world.Size.y; y++)
				{
					for (int z = 0; z < _world.Size.z; z++)
					{
						int numberOfNeighbors = 0;

						for (int i = -1; i < 2; i++)
						{
							for (int j = -1; j < 2; j++)
							{
								for (int k = -1; k < 2; k++)
								{
									if (i == 0 && j == 0 && k == 0) continue;

									numberOfNeighbors += IsNeighborAlive(x, y, z, i, j, k);
								}
							}
						}

						bool shouldLive = false;
						bool isAlive = _world[x, y, z].IsAlive();

						if (isAlive && (numberOfNeighbors == 2 || numberOfNeighbors == 3))
						{
							shouldLive = true;
						}
						else if (!isAlive && numberOfNeighbors == 3) // zombification
						{
							shouldLive = true;
						}

						if (!shouldLive)
						{
							_nextGeneration[x, y, z].Kill();
						}
						else
						{
							_nextGeneration[x, y, z].Generate();
						}
					}
				}
			}
		}
	}
}
