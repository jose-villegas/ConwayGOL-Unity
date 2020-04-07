using OpenLife.Life;
using OpenLife.World;
using UnityEngine;

namespace OpenLife.Visualization
{
	public class RunSimulation : MonoBehaviour
	{
		[SerializeField]
		private Vector3Int _worldSize;
		[SerializeField]
		private GameObject _asset;

		private World.World<CellView> _world;
		private Simulation<CellView> _simulation;

		private void Start()
		{
			var worldFactory = new Generator<CellView>();
			_world = worldFactory.Create(_worldSize.x, _worldSize.y, _worldSize.z);

			for (int x = 0; x < _world.Size.x; x++)
			{
				for (int y = 0; y < _world.Size.y; y++)
				{
					for (int z = 0; z < _world.Size.z; z++)
					{
						_world[x, y, z].Instance = Instantiate(_asset, Vector3.zero, Quaternion.identity);
						_world[x, y, z].Instance.transform.SetParent(transform);
						_world[x, y, z].Instance.transform.localPosition = new Vector3(x, y, z);

						if (Random.Range(0f, 1f) > 0.5f)
						{
							_world[x, y, z].Generate();
						}
					}
				}
			}

			_simulation = new Simulation<CellView>();
			_simulation.Start(_world);
		}

		private void Update()
		{
			_simulation.Tick();
		}
	}
}
