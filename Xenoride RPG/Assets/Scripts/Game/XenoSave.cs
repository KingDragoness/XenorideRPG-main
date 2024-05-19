using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Xenoride
{

    [System.Serializable]
    public class SaveData
    {
        [System.Serializable]
        public class BattleStat
        {
            public int VIT = 1; //vitality, max health point
            public int STR = 1; //strength, damage
            public int SHOOT = 1; //gun attack miss chance
            public int DEX = 1; //agility, miss chance, attack hit chance, melee damage
            public int ENE = 1; //energy, max SP point

            public int MaxHitpoint
            {
                get { return VIT * 9; }
            }

            public int MaxSP
            {
                get { return ENE * 4; }
            }
        }

        [System.Serializable]
        public class PartyStat
        {
            public string partyID = "";
            public string OverrideName = "";
            public BattleStat battleStat;
            public int currentHP = 67;
            public int currentSP = 8;

            public int MaxHitpoint
            {
                get { return battleStat.VIT * 9; }
            }

            public int MaxSP
            {
                get { return battleStat.ENE * 4; }
            }

        }

        public List<PartyStat> allPartySaves = new List<PartyStat>();


    }

    public class XenoSave : GameModule
    {

        public SaveData saveData;


        private void Awake()
        {
            Engine.AddGameModule(this);

        }
    }
}