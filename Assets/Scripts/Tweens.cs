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
    }

    public void MoverArribaAbajo(float distancia, float velocidad)
    {
        transform.DOMoveY(transform.position.y + distancia, velocidad)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    
    public void MoverIzquierdaDerecha(float distancia, float velocidad)
    {
        transform.DOMoveX(transform.position.x + distancia, velocidad)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    
    public void Giro2D()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleX(0f, 0.5f).SetEase(Ease.InSine));
        seq.AppendCallback(() => FlipSprite());
        seq.Append(transform.DOScaleX(1f, 0.5f).SetEase(Ease.OutSine));
        seq.SetLoops(-1);
    }
    void FlipSprite()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.flipX = !sr.flipX;
    }
    
    public void Giro3D()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
                 .SetLoops(-1)
                 .SetEase(Ease.Linear);
    }




}
