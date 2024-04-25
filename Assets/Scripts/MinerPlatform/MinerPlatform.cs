using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class MinerPlatform : MonoBehaviour
{
	[Header("Control")]
	[SerializeField] private Joystick _joystick;
	[SerializeField] private float _swingForce;
	[SerializeField] private float _moveDownForce;
	[SerializeField] private float _smoothVal = 0.2f;

	[Header("Danger")]
	[SerializeField] private string _dangerObjectsTag = "Danger";
	[SerializeField] private ParticleSystem _bloodEffect;

	[Header("Character")]
	[SerializeField] private Rigidbody2D _minerCharacter;
	[SerializeField] private float _characterFallSimulationDuration = 1.0f;

	[Header("Initial Positions")]
	[SerializeField] private Vector3 _characterInitialLocalPosition;
	[SerializeField] private Vector3 _platformInitialPosition;

	private Rigidbody2D _rigidBody;

	public static int CollectedCoinsCount { get; private set; }

	public static event Action<int> CoinsAmountChanged;
	public event Action CharacterFellOut;

	private Vector3 _refVelocity;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();

		CollectedCoinsCount = 0;

		_characterInitialLocalPosition = _minerCharacter.transform.localPosition;
		_minerCharacter.simulated = false;

		_platformInitialPosition = transform.position;
	}

	public void MoveToInitialState()
	{
		gameObject.SetActive(false);

		_minerCharacter.simulated = false;
		_minerCharacter.velocity = Vector3.zero;
		_minerCharacter.angularVelocity = 0;

		_minerCharacter.transform.SetLocalPositionAndRotation(_characterInitialLocalPosition, Quaternion.identity);

		transform.SetPositionAndRotation(_characterInitialLocalPosition, Quaternion.identity);

		CollectedCoinsCount = 0;
		CoinsAmountChanged?.Invoke(CollectedCoinsCount);

		_rigidBody.velocity = Vector3.zero;
		_rigidBody.angularVelocity = 0;
		_rigidBody.simulated = true;

		gameObject.SetActive(true);
	}

	private void FixedUpdate()
	{
		var direction = _joystick.Direction;

		if (direction == Vector2.zero)
		{
			return;
		}

		if (direction.y > 0)
		{
			direction.y = 0;
		}

		if (_rigidBody.simulated == false)
		{
			return;
		}

		transform.position += _moveDownForce * direction.y * Vector3.up;

		direction.y = 0;
		direction.x *= _swingForce;

		_rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, direction, ref _refVelocity, _smoothVal);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out ICollectable collectable))
		{
			collectable.Collect();

			CollectedCoinsCount++;
			CoinsAmountChanged?.Invoke(CollectedCoinsCount);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag(_dangerObjectsTag))
		{
			SoundSystem.Instance.PlayCrashSound();

			_bloodEffect.transform.position = collision.contacts[0].point;
			_bloodEffect.Play();

			_minerCharacter.simulated = true;
			_rigidBody.simulated = false;

			Invoke(nameof(OnCharacterFall), _characterFallSimulationDuration);

			return;
		}
	}

	private void OnCharacterFall()
	{
		_minerCharacter.simulated = false;
		CharacterFellOut?.Invoke();
	}
}