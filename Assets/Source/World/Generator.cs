using OpenLife.World.Interface;

namespace OpenLife.World
{
	public class Generator<T> : WorldFactory<World<T>> where T : new()
	{
		public override World<T> Create(int x, int y, int z)
		{
			return new World<T>(x, y, z);
		}
	}
}
