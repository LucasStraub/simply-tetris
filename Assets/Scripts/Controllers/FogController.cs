using UnityEngine;

[ExecuteInEditMode]
public class FogController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Animator _animator;

    [SerializeField] private float _startDistance = 20;
    [SerializeField] private float _endDistance = 24;
    [SerializeField] private float _fogOffset = 0;
#pragma warning restore 0649

    private void Update()
    {
        RenderSettings.fogStartDistance = _startDistance + _fogOffset;
        RenderSettings.fogEndDistance = _endDistance + _fogOffset;
    }

    public void SetActive(bool value)
    {
        _animator.SetBool("Open", value);
    }
}
