using UnityEngine;
using PathCreation;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PathsDatabase pathsDatabase;
    private PathCreator pathIn;
    private PathCreator pathOut;

    private void Start() {
        setPathIn();
        setPathOut();
    }

    public void setPathIn(){
        pathIn = pathsDatabase.getRandomPathIn();
    }

    public PathCreator getCurrentPath(){
        return pathIn;
    }

    public void setPathOut(){
        pathOut = pathsDatabase.getRandomPathOut();
    }

    public PathCreator getCurrentPathOut(){
        setPathOut();
        return pathOut;
    }
}
