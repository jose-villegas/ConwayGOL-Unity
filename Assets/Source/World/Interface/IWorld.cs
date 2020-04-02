namespace OpenLife.World.Interface
{
	public interface IWorld
	{
	}

	public interface IWorld<T> : IWorld
	{
		T this[int x, int y, int z] { get; set; }
	}
}