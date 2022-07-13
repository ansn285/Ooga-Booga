using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "LevelsCollection", menuName = "Gameplay/Levels Collection")]
    public class LevelsCollection : ScriptableObject
    {
        [SerializeField] public List<LevelInfo> LevelInfos;

        public LevelInfo GetLevelNumberFromName(string name)
        {
            return LevelInfos.First(x => x.LevelName == name);
        }

        public LevelInfo GetLevelFromNumber(int number)
        {
            return LevelInfos.First(x => x.LevelNumber == number);
        }
    }

    [Serializable]
    public class LevelInfo
    {
        public int LevelNumber;
        public string LevelName;
    }
}