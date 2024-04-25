using DG.Tweening;
using UnityEngine;

public class RotationAnimationZAxis : MonoBehaviour
{
	[Header("Rotation")]
	[SerializeField, Range(0.1f, 2.0f)] private float _rotationSpeed = 0.5f;

	private Tween _tween;

	private void OnEnable() => Rotate();

	private void OnDisable() => _tween.Kill();

	private void Rotate()
	{
		_tween = transform.DOLocalRotate(new Vector3(0, 0, -360), _rotationSpeed, RotateMode.FastBeyond360)
			.SetEase(Ease.Linear)
			.SetLoops(-1);
	}
}
