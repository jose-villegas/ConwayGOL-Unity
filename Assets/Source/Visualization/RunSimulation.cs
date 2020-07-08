using OpenLife.Life;
using OpenLife.World;
using UnityEngine;
using UnityEngine.UI;

namespace OpenLife.Visualization
{
	public class RunSimulation : MonoBehaviour
	{
		[SerializeField] private Vector2Int _worldSize;

		[SerializeField] private int _separation;
		[SerializeField] private GameObject _current;
		[SerializeField] private GameObject[] _trail;

		private World<CellView> _world;
		private Simulation<CellView> _simulation;

		private void Start()
		{
			Restart();
		}

		public void Restart()
		{
			foreach (Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
			}

			CancelInvoke("UpdateSimulation"); 
			var worldFactory = new Generator<CellView>();
			_world = worldFactory.Create(_worldSize.x, _worldSize.y, 1);

			for (int x = 0; x < _world.Size.x; x++)
			{
				for (int y = 0; y < _world.Size.y; y++)
				{
					for (int z = 0; z < _world.Size.z; z++)
					{
						_world[x, y, z].Instance = Instantiate(_current, Vector3.zero, Quaternion.identity);
						_world[x, y, z].Instance.transform.SetParent(transform);
						_world[x, y, z].Instance.transform.localPosition =
							new Vector3(x * _separation, y * _separation, z * _separation);

						// get callbacks

						_world[x, y, z].Trail = new GameObject[_trail.Length];

						for (var i = 0; i < _trail.Length; i++)
						{
							_world[x, y, z].Trail[i] = Instantiate(_trail[i], Vector3.zero, Quaternion.identity);
							_world[x, y, z].Trail[i].transform.SetParent(transform);
							_world[x, y, z].Trail[i].transform.localPosition =
								new Vector3(x * _separation, y * _separation, z * _separation);
						}


						if (Random.Range(0f, 1f) > 0.5f)
						{
							_world[x, y, z].Generate();
						}
					}
				}
			}

			_simulation = new Simulation<CellView>();
			_simulation.Start(_world);

			InvokeRepeating("UpdateSimulation", 0, 0.05f);
		}

		private void UpdateSimulation()
		{
			_simulation.Tick();
		}

		private void LateUpdate()
		{
#if UNITY_EDITOR
			if (Input.GetMouseButton(0))
			{
				var x = Mathf.RoundToInt((Input.mousePosition.x / Screen.width) * _worldSize.x);
				var y = Mathf.RoundToInt((Input.mousePosition.y / Screen.height) * _worldSize.y);


				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						var lookX = Mathf.Clamp(x + i, 0, _worldSize.x - 1);
						var lookY = Mathf.Clamp(y + j, 0, _worldSize.y - 1);

						var cell = _simulation.NextGeneration[lookX, lookY, 0];

						if (cell.IsAlive())
						{
							cell.Kill();
						}
						else
						{
							cell.Generate();
						}
					}
				}
			}

#else
			for (int index = 0; index < Input.touchCount; ++index)
			{
				if (Input.GetTouch(index).phase == TouchPhase.Moved)
				{
					var touch = Input.GetTouch(index);
					var x = Mathf.RoundToInt((touch.position.x / Screen.width) * _worldSize.x);
					var y = Mathf.RoundToInt((touch.position.y / Screen.height) * _worldSize.y);

					for (int i = -1; i < 2; i++)
					{
						for (int j = -1; j < 2; j++)
						{
							var lookX = Mathf.Clamp(x + i, 0, _worldSize.x - 1);
							var lookY = Mathf.Clamp(y + j, 0, _worldSize.y - 1);

							var cell = _simulation.NextGeneration[lookX, lookY, 0];

							if (cell.IsAlive())
							{
								cell.Kill();
							}
							else
							{
								cell.Generate();
							}
						}
					}
				}
			}
#endif
		}
	}
}