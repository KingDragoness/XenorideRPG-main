using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride
{

    [Serializable]
    public abstract class CC_Base
    {
        public abstract string CommandName { get; }
        public abstract Module ModuleType { get; } 
        public virtual string Description { get { return ""; } }

        public abstract void ExecuteCommand(string[] args);
    }




    public class CC_PackSave : CC_Base
    {
        public override string CommandName { get { return "packsave"; } }
        public override Module ModuleType { get { return Module.Engine; } }
        public override string Description { get { return "DEV ONLY: pack save file data."; } }


        public override void ExecuteCommand(string[] args)
        {
            //FactoryGame.Game.DEBUG_PackSaveDatas();
        }
    }


    public class CC_QuickSave : CC_Base
    {
        public override string CommandName { get { return "savegame"; } }
        public override Module ModuleType { get { return Module.Engine; } }
        public override string Description { get { return "Save game."; } }


        public override void ExecuteCommand(string[] args)
        {
            //FactoryGame.Game.SaveGame();
            Debug.Log("Game saved.");
        }
    }

    public class CC_QuickLoad: CC_Base
    {
        public override string CommandName { get { return "loadgame"; } }
        public override Module ModuleType { get { return Module.Engine; } }
        public override string Description { get { return "Load game."; } }


        public override void ExecuteCommand(string[] args)
        {
            //FactoryGame.Game.LoadGame();
        }
    }



}

namespace Xenoride.TBC
{
    public class CC_TBC_EndTurn : CC_Base
    {
        public override string CommandName { get { return "endturn"; } }
        public override Module ModuleType { get { return Module.TBC; } }
        public override string Description { get { return "End current turn."; } }


        public override void ExecuteCommand(string[] args)
        {
            TurnBasedCombat.Turn.EndTurn();
        }
    }
}