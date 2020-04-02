using OpenLife.World;
using System;
using System.Threading.Tasks;

namespace OpenLife.Life
{
	public class Simulation
	{
		private World.World<bool> _nextGeneration;
		private World.World<bool> _world;

		public int Generations { get; private set; }
		public Action<World.World<bool>> NextGenerationCompleted { get; private set; }
		public World<bool> World { get => _world; }

		public void Start(World.World<bool> world)
		{
			_nextGeneration = new World.World<bool>(world.Size.x, world.Size.y, world.Size.z);
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

		private int IsNeighborAlive(int x, int y, int offsetX, int offsetY)
		{
			int result = 0;

			int proposedOffsetX = x + offsetX;
			int proposedOffsetY = y + offsetY;
			bool outOfBounds = proposedOffsetX < 0 || proposedOffsetX >= _world.Size.x |
							   proposedOffsetY < 0 || proposedOffsetY >= _world.Size.y;

			if (!outOfBounds)
			{
				result = _world[x + offsetX, y + offsetY, 0] ? 1 : 0;
			}

			return result;
		}

		private void ProcessGeneration()
		{
			for (int x = 0; x < _world.Size.x; x++)
			{
				for (int y = 0; y < _world.Size.y; y++)
				{
					int numberOfNeighbors = IsNeighborAlive(x, y, -1, 0)
							+ IsNeighborAlive(x, y, -1, 1)
							+ IsNeighborAlive(x, y, 0, 1)
							+ IsNeighborAlive(x, y, 1, 1)
							+ IsNeighborAlive(x, y, 1, 0)
							+ IsNeighborAlive(x, y, 1, -1)
							+ IsNeighborAlive(x, y, 0, -1)
							+ IsNeighborAlive(x, y, -1, -1);

					bool shouldLive = false;
					bool isAlive = _world[x, y, 0];

					if (isAlive && (numberOfNeighbors == 2 || numberOfNeighbors == 3))
					{
						shouldLive = true;
					}
					else if (!isAlive && numberOfNeighbors == 3) // zombification
					{
						shouldLive = true;
					}

					_nextGeneration[x, y, 0] = shouldLive;
				}
			}
		}
	}
}
