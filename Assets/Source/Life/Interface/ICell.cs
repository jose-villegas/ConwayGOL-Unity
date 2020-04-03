namespace OpenLife.Life.Interface
{
	public interface ICell
	{
		/// <summary>
		/// Tells if the cell is currenty alive
		/// </summary>
		/// <returns></returns>
		bool IsAlive();

		/// <summary>
		/// Kills this cell
		/// </summary>
		/// <returns></returns>
		void Kill();

		/// <summary>
		/// Creates a cell
		/// </summary>
		void Generate();
	}
}
