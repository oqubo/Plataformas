using UnityEngine;
using DG.Tweening;

public class BichoDeLuz : MonoBehaviour
{
    public Transform player;          // Referencia al jugador
    public float radius = 2f;         // Radio del movimiento errante
    public float moveDuration = 1.5f; // Tiempo que tarda en moverse
    public float waitTime = 0.5f;     // Tiempo entre movimientos

    private void Start()
    {
        MoveToRandomPosition();
    }

    void MoveToRandomPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * radius;
        // mantenerla sobre el jugador
        randomOffset.y = Mathf.Abs(randomOffset.y); 
        Vector3 targetPosition = player.position + randomOffset;

        transform.DOMove(targetPosition, moveDuration)
                 .SetEase(Ease.InOutSine)
                 .OnComplete(() => Invoke(nameof(MoveToRandomPosition), waitTime));
    }
}
