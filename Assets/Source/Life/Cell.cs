using OpenLife.Life.Interface;

namespace OpenLife.Life
{
	public class Cell : ICell
	{
		protected bool cell;

		public bool IsAlive()
		{
			return cell;
		}

		public void Kill()
		{
			cell = false;
		}

		public void Generate()
		{
			cell = true;
		}
	}
}
