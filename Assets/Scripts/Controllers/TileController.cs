using System.Collections;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Animator _animator;

    [SerializeField] private ColorScheme[] _colorSchemes;

    public void SetTile(int value)
    {
        SetActive(value);
        SetColor(value);
    }

    public void SetActive(int value)
    {
        _animator.SetBool("Open", value != 0);
    }

    public void SetColor(int value)
    {
        var main = _colorSchemes[value].Main;
        var dark = _colorSchemes[value].Dark;
        var bright = _colorSchemes[value].Bright;

        var delay = value == 0 ? 1 : 0;
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
