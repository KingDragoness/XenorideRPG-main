using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{

    public class TBC
    {


        public enum ChildUI_Type
        {
            Item,
            SpecialAttack
        }


        [System.Serializable]
        public class TurnOrder
        {
            public TBC_Party party;

            public TurnOrder(TBC_Party party)
            {
                this.party = party;
            }
        }

        [System.Serializable]
        public class EffectToken
        {
            public EffectType effectType;
            public TBC_Party origin;
            public float Value = 0f;

        }

        [System.Serializable]
        public class OrderToken
        {
            public TBC_Action commandOrder;
            public TBC_Party target;

            public OrderToken(TBC_Action commandOrder, TBC_Party target)
            {
                this.commandOrder = commandOrder;
                this.target = target;
            }
        }

    }

}