using System.Collections;
using UnityEngine;

public class TileController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Animator _animator;

    [SerializeField] private ColorScheme[] _colorSchemes;
#pragma warning restore 0649

    public void SetTile(TetrominoType value)
    {
        SetActive(value);
        SetColor(value);
    }

    public void SetActive(TetrominoType value)
    {
        _animator.SetBool("Open", value != TetrominoType.None);
    }

    public void SetColor(TetrominoType value)
    {
        var main = _colorSchemes[(int)value].Main;
        var dark = _colorSchemes[(int)value].Dark;
        var bright = _colorSchemes[(int)value].Bright;

        var delay = value == TetrominoType.None ? 1 : 0;
        IEnumerator Co()
        {
            yield return new WaitForSeconds(delay);
            _meshRenderer.material.SetColor("_Color1_B", main);
            _meshRenderer.material.SetColor("_Color1_L", dark);
            _meshRenderer.material.SetColor("_Color1_R", bright);
            _meshRenderer.material.SetColor("_Color1_T", bright);
            _meshRenderer.material.SetColor("_Color1_D", dark);
        }
        StopAllCoroutines();
        StartCoroutine(Co());
    }
}
