using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tweens : MonoBehaviour
{

    
    public void Parpadeo()
    {
        int parpadeos = 3;
        float duracionParpadeo = 0.1f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sequence secuencia = DOTween.Sequence();

        for (int i = 0; i < parpadeos; i++)
        {
            secuencia.Append(spriteRenderer.DOColor(Color.red, duracionParpadeo));
            secuencia.Append(spriteRenderer.DOColor(Color.white, duracionParpadeo));
        }

    }

    public void Aparecer()
    {
        transform.DOMoveX(transform.position.x - 7, 1f).SetEase(Ease.InOutQuad);
        transform.DOMoveY(1f, 5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void Desaparecer()
    {

        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        Destroy(gameObject, 0.3f);
    }

    public void MoverArribaAbajo()
    {
        transform.DOMoveY(transform.position.y + 0.5f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    
    




}
