using DG.Tweening;
using UnityEngine;

public class ResizeAnimations : MonoBehaviour
{

    #region Fields

    public GameObject objectToManipulate;
    public float animationDuration = 1.0f;

    #endregion Fields

    #region Mono

    private void OnEnable()
    {
        SizeTo1();
    }

    #endregion Mono

    #region Methods

    private void SizeTo1()
    {
        objectToManipulate.transform.localScale = Vector3.zero;
        objectToManipulate.transform.DOScale(Vector3.one, animationDuration);
    }

    public void SizeTo0()
    {
        objectToManipulate.transform.localScale = Vector3.one;
        objectToManipulate.transform.DOScale(Vector3.zero, animationDuration).onComplete += DisableGameObject;
    }

    private void DisableGameObject()
    {
        objectToManipulate.SetActive(false);
    }

    #endregion Methods

}
