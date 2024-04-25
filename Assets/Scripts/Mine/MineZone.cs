using System;
using System.Collections.Generic;
using UnityEngine;

public class MineZone : MonoBehaviour
{
	[Header("Coins")]
	[SerializeField] private CollectableCoin _prefab;
	[SerializeField, Min(0)] private int _minCoinsCount = 1;
	[SerializeField, Range(1, 5)] private int _maxCoinsCount = 2;
	[SerializeField] private Transform _coinsSpawnPositionsParent;

	private readonly List<CollectableCoin> _coins = new();
	private readonly List<Transform> _spawnPositions = new();

	public event Action<MineZone> PlatformReachedCenter;
	public bool IsPlatformReachedCenter { get; private set; }

	private void Awake()
	{
		InitSpawnPositions();
		InstantiateCoins();
	}

	private void OnEnable()
	{
		IsPlatformReachedCenter = false;

		PlaceCoins();
	}

	private void InstantiateCoins()
	{
		_coins.Clear();

		for (int i = 0; i < _maxCoinsCount; i++)
		{
			var coin = Instantiate(_prefab, transform);
			_coins.Add(coin);

			coin.gameObject.SetActive(false);
		}
	}

	private void InitSpawnPositions()
	{
		_spawnPositions.Clear();

		foreach (Transform spawnPoint in _coinsSpawnPositionsParent)
		{
			_spawnPositions.Add(spawnPoint);
		}
	}

	private void PlaceCoins()
	{
		foreach (var coin in _coins)
		{
			coin.gameObject.SetActive(false);
		}

		var randomCoinsCount = UnityEngine.Random.Range(_minCoinsCount, _maxCoinsCount + 1);

		if (_spawnPositions.Count < randomCoinsCount)
		{
			Debug.LogWarning("Not enough spawn points.");
			return;
		}

		_spawnPositions.Shuffle();

		for (int i = 0; i < randomCoinsCount; i++)
		{
			_coins[i].transform.position = _spawnPositions[i].position;
			_coins[i].gameObject.SetActive(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsPlatformReachedCenter)
		{
			return;
		}

		if (collision.TryGetComponent(out MinerPlatform platform))
		{
			PlatformReachedCenter?.Invoke(this);
			IsPlatformReachedCenter = true;
		}
	}
}