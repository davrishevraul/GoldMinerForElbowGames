using UnityEngine;

public class TargetYAxisFollow : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private Vector3 _offset;

	private Transform _transform;

	private float _velocity;

	[SerializeField] private float _smoothTime = 0.1f;

	private void Awake()
	{
		_transform = transform;
	}

	private void LateUpdate()
	{
		_transform.position = new(_offset.x, Mathf.SmoothDamp(_transform.position.y, _target.position.y + _offset.y, ref _velocity, _smoothTime), _offset.z);
	}
}