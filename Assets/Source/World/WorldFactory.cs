using OpenLife.World.Interface;

namespace OpenLife.World
{
	public abstract class WorldFactory<T> where T : IWorld
	{
		public abstract T Create(int x, int y, int z);
	}
}