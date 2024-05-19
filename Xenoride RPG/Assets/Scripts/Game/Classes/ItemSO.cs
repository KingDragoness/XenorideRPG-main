using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride
{

	[CreateAssetMenu(fileName = "Phoenix Down", menuName = "Xenoride/Item", order = 1)]

	public class ItemSO : ScriptableObject
	{

		public string ID = "PhoenixDown";
		public string NameDisplay = "Phoenix Down";
		public Sprite SpriteIcon;
		public List<EffectUnit> allEffectUnits = new List<EffectUnit>();

	}
}