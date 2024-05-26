using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride
{

    [System.Serializable]
    public class EffectUnit
    {
        public EffectType EffectType;
        public float Value = 0f;
    }

    public enum TargetTags
    {
        Nothing,
        All, //targets everything
        Single, //targets one
        AllFoes, //targets all foes
        AllParty, //targets all party
        Party, //target party member
        Enemy, //target enemy
        Revive = 20 //selects downed party member
    }

    public enum Module
    {
        Engine, //engine
        Game, //the game
        TBC //turn based combat
    }

    public enum EffectType
    {
        DamageDeal,
        RestoreHP_Flat,
        RestoreHP_Percent,
        RestoreSP_Flat,
        RestoreSP_Percent,
        GainDEX = 10,
        GainSTR,
        GainVIT,
        GainSHOOT,
        Resurrect = 20,
        Poison,
        Sleep,
        Burn
    }


}