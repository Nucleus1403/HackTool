using DG.Tweening;
using UnityEngine;

public class BackgroundImageMovement : MonoBehaviour
{

    #region Mono

    private void Start()
    {
        MoveDownwords();
    }

    #endregion Mono

    #region Methods

    private void MoveDownwords()
    {
        this.gameObject.transform.DOMoveY(1293, 10f).SetEase(Ease.Linear).onComplete += MoveUpwords;
    }

    private void MoveUpwords()
    {
        this.gameObject.transform.DOMoveY(264, 10f).SetEase(Ease.Linear).onComplete += MoveDownwords;
    }

    #endregion Methods

}
