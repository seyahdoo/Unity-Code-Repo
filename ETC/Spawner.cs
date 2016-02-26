using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : Singleton<Spawner> {

    public Transform UpLine;
    public Transform DownLine;
    public Transform LeftLine;
    public Transform RightLine;
    
    //how many seconds between every vertical line creation
    public float SyncTime = 1.0f;

    public int LevelsIndex = 0;
    public int InLevelIndex = 0;

    public List<Level> Levels;

    [System.Serializable]
    public class Level
    {
        public List<LevelMoment> LevelObjects;
        public float Difficulty;
        //ETC
    }

	[System.Serializable]
	public class LevelMoment
    {

        //public float Time;
        public string LineUp;
        public string LineDown;
        public string LineRight;
        public string LineLeft;
        //public string Special;  
    }

    void Awake()
    {
        EventManager.GameOverEvent += StopSpawner;
        EventManager.GameStartEvent += StartSpawner;
    }

    public void StartSpawner()
    {
        //Debug.Log("Starting Spawn");

        StopCoroutine("SpawnCycle");
        StartCoroutine("SpawnCycle");
    }

    public void StopSpawner()
    {
        StopCoroutine("SpawnCycle");
        LevelsIndex = 0;
        InLevelIndex = 0;

    }

    IEnumerator SpawnCycle()
    {
        //Coroutine
        //start at the beginning of the list
        //1: //Create stuff List[0]
        //remove List[0]
        //wait for List[0].Time
        //goto 1
        //Debug.Log("Starting Spawn");

        while (true)
        {
            //if Level is finished, go to next level
            if(InLevelIndex >= Levels[LevelsIndex].LevelObjects.Count)
            {
                InLevelIndex = 0;
                LevelsIndex++;
                if(LevelsIndex >= Levels.Count)
                {
                    Debug.Log("Levels Ended, looping");
                    LevelsIndex = 0;

                }
            }

            //create stuff
            string Up = Levels[LevelsIndex].LevelObjects[InLevelIndex].LineUp;
            string Down = Levels[LevelsIndex].LevelObjects[InLevelIndex].LineDown;
            string Right = Levels[LevelsIndex].LevelObjects[InLevelIndex].LineRight;
            string Left = Levels[LevelsIndex].LevelObjects[InLevelIndex].LineLeft;

            if (Up.Length > 2)
            {
                GameObject go = PoolManager.Instance.GetGameObject(Up);
                go.transform.position = UpLine.position;
            }

            if (Down.Length > 2)
            {
                GameObject go = PoolManager.Instance.GetGameObject(Down);
                go.transform.position = DownLine.position;
            }

            if (Left.Length > 2)
            {
                GameObject go = PoolManager.Instance.GetGameObject(Left);
                go.transform.position = LeftLine.position;
            }

            if (Right.Length > 2)
            {
                GameObject go = PoolManager.Instance.GetGameObject(Right);
                go.transform.position = RightLine.position;
            }


            yield return new WaitForSeconds(SyncTime);

            InLevelIndex++;

        }
        
    }


}
