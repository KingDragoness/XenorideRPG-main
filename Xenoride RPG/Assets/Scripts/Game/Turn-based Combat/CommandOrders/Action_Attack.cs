using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{

    //derivates CO_Sword & CO_Gun is useless
	public class Action_Attack : TBC_Action
	{


        public override void Execute()
        {
            //Get current party member's hold weapon
            //calculate damage output
            Debug.Log("Execute attack animation.");
        }



    }
}