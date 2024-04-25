using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineZonesSpawner : MonoBehaviour
{
	[Header("Zones")]
	[SerializeField] private List<MineZone> _zones;
	[SerializeField, Range(1, 100)] private float _zonesDistance = 10.0f;

	private MineZone _previousZone;

	private MineZone _currentZone;

	private float _currentDistance;

	private void OnEnable() => SubscribeToEvents();

	private void OnDisable() => UnsubscribeFromEvents();

	private void Start()
	{
		InitZones();
	}

	public void StartFromBeginning()
	{
		InitZones();
	}

	private void SubscribeToEvents()
	{
		foreach (var zone in _zones)
		{
			zone.PlatformReachedCenter += OnPlatformReachedZoneCenter;
		}
	}

	private void UnsubscribeFromEvents()
	{
		foreach (var zone in _zones)
		{
			zone.PlatformReachedCenter -= OnPlatformReachedZoneCenter;
		}
	}

	private void InitZones()
	{
		foreach (var zone in _zones)
		{
			zone.gameObject.SetActive(false);
		}

		_currentDistance = 0;

		_currentZone = _zones.First();
		_currentZone.transform.position = new(_currentZone.transform.position.x, _currentDistance, _currentZone.transform.position.z);
		_previousZone = null;

		_currentZone.gameObject.SetActive(true);
	}

	private void OnPlatformReachedZoneCenter(MineZone zone)
	{
		if (_previousZone != null)
		{
			_previousZone.gameObject.SetActive(false);
			_currentZone.gameObject.SetActive(false);
		}

		_previousZone = _currentZone;
		_currentZone = zone;

		_zones.Shuffle();

		foreach (var mineZone in _zones)
		{
			if (mineZone.gameObject.activeSelf)
			{
				continue;
			}

			_currentDistance -= _zonesDistance;
			mineZone.transform.position = new(mineZone.transform.position.x, _currentDistance, mineZone.transform.position.z);
			mineZone.gameObject.SetActive(true);

			break;
		}
	}
}
