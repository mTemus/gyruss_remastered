using UnityEngine;
using PathCreation;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PathsDatabase pathsDatabase;
    [SerializeField] private PathCreator pathCreator;

    private void Start() {
        setPathForWave();
    }

    public void setPathForWave(){
        pathCreator = pathsDatabase.getRandomPathIn();
    }

    public PathCreator getCurrentPath(){
        return pathCreator;
    }
}
