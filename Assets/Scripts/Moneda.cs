using UnityEngine;


public class Moneda : MonoBehaviour
{

    protected Tweens tweens;
    void Awake()
    {
        tweens = GetComponent<Tweens>();
        if (tweens == null)
        {
            tweens = gameObject.AddComponent<Tweens>();
        }
    }

    void Start()
    {
        tweens.Giro2D();
    }


    void Update()
    {
        
    }
}
