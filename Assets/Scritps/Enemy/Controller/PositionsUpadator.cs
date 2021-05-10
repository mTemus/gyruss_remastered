using UnityEngine;

public class PositionsUpadator : MonoBehaviour
{
    private Transform miniBossModule;
    private Transform centerPoint;
    private EnemyController myEnemyController;

    private string updateTarget;
    
    void Update()
    {
        if (myEnemyController.Equals(null)) return;

        switch (updateTarget) {
            case "point":
                myEnemyController.CenterPosition = centerPoint.position;
                break;
            case "module":
                myEnemyController.ModulePosition = miniBossModule.transform.position;
                break;
        }
    }

    public void SetPointToUpdate(Transform point)
    {
        centerPoint = point;
        updateTarget = "point";
    }

    public void SetModuleToUpdate(Transform module)
    {
        miniBossModule = module;
        updateTarget = "module";
    }
    
    public EnemyController MyEnemyController
    {
        get => myEnemyController;
        set => myEnemyController = value;
    }
}
