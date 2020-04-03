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

		private Simulation<Cell> _simulation;

		private World.World<GameObject> _visualWorld;

		private void Start()
		{
			var worldFactory = new Generator<Cell>();
			var world = worldFactory.Create(_worldSize.x, _worldSize.y, _worldSize.z);

			var visualWorldFactory = new Generator<GameObject>();
			_visualWorld = visualWorldFactory.Create(_worldSize.x, _worldSize.y, _worldSize.z);

			for (int x = 0; x < world.Size.x; x++)
			{
				for (int y = 0; y < world.Size.y; y++)
				{
					for (int z = 0; z < world.Size.z; z++)
					{
						_visualWorld[x, y, z] = Instantiate(_asset, Vector3.zero, Quaternion.identity);
						_visualWorld[x, y, z].transform.SetParent(transform);
						_visualWorld[x, y, z].transform.localPosition = new Vector3(x, y, z);
					}
				}
			}

			// Toggle cells
			for (int x = 0; x < world.Size.x; x++)
			{
				for (int y = 0; y < world.Size.y; y++)
				{
					for (int z = 0; z < world.Size.z; z++)
					{
						if (Random.Range(0f, 1f) > 0.5f)
						{
							world[x, y, z].Generate();
						}
					}
				}
			}

			_simulation = new Simulation<Cell>();
			_simulation.Start(world);

			//InvokeRepeating("Tick", 0, 0.1f);
		}

		private void Update()
		{
			for (int x = 0; x < _simulation.World.Size.x; x++)
			{
				for (int y = 0; y < _simulation.World.Size.y; y++)
				{
					for (int z = 0; z < _simulation.World.Size.z; z++)
					{
						_visualWorld[x, y, z].SetActive(_simulation.World[x, y, z].IsAlive());
					}
				}
			}
			_simulation.Tick();
		}
	}
}
