using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride
{

	[CreateAssetMenu(fileName = "Phoenix Down", menuName = "Xenoride/Weapon", order = 1)]

	public class WeaponItem : ScriptableObject
	{

		public string ID = "Sharpsword";
		public string displayName = "Sharpsword";
		public Sprite WeaponIcon;
		public Item.WeaponCategory category;
		public float baseDamage = 12f;


	}
}