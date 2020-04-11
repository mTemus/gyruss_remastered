using UnityEngine;

public class CenterPointUpdator : MonoBehaviour
{
    private Transform centerPoint;
    private EnemyController myEnemyController;

    void Update()
    {
        if (myEnemyController.Equals(null)) return;
        myEnemyController.CenterPosition = centerPoint.position;
    }

    public EnemyController MyEnemyController
    {
        get => myEnemyController;
        set => myEnemyController = value;
    }

    public Transform CenterPoint
    {
        get => centerPoint;
        set => centerPoint = value;
    }
}
