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

    public PathCreator getRandomPathBack(){
        return backLevelPaths[Random.Range(0,backLevelPaths.Count)];
    }

    public PathCreator getRandomPathInChanceStage(){
        return chanceLevelPaths[Random.Range(0,chanceLevelPaths.Count)];
    }
}
