using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[CreateAssetMenu(fileName = "PathsDatabase", menuName = "pam_gyruss_remastered/PathsDatabase", order = 0)]
public class PathsDatabase : ScriptableObject {
    public List<PathCreator> normalLevelPaths;
    public List<PathCreator> chanceLevelPaths;
    public List<PathCreator> backLevelPaths;

    public PathCreator getRandomPathIn(){
        return normalLevelPaths[Random.Range(0,normalLevelPaths.Count)];
    }

    public PathCreator getRandomPathOut(){
        return backLevelPaths[Random.Range(0,backLevelPaths.Count)];
    }

    public PathCreator getRandomPathInChanceStage(){
        return chanceLevelPaths[Random.Range(0,chanceLevelPaths.Count)];
    }

    public PathCreator getClosestPathToPlayer(){
        Vector3 playerPos = GyrussGameManager.Instance.GetPlayerShipPosition();
        foreach(var pathOut in backLevelPaths){
            if(Vector3.Distance(pathOut.path.GetClosestPointOnPath(playerPos), playerPos) <= 1){
                return pathOut;
            }
        }
        return backLevelPaths[Random.Range(0,normalLevelPaths.Count)];
    }
}
