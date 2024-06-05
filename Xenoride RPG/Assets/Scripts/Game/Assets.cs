using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride
{
	public class Assets : MonoBehaviour
	{

        [SerializeField] private List<ItemSO> allItemSO = new List<ItemSO>();

        [FoldoutGroup("Direct Link")] public Sprite sprite_Inventory;
        [FoldoutGroup("Direct Link")] public Sprite sprite_SpecialAttack;

        public List<ItemSO> AllItemSO { get => allItemSO;}

        private void Awake()
        {
            RefreshDatabase();
        }

        [Button("Refresh Database")]
        public void RefreshDatabase()
        {
            var _allItems = Resources.LoadAll("", typeof(ItemSO)).Cast<ItemSO>().ToList();

            allItemSO = _allItems;
        }


        public ItemSO GetItem(string ID)
        {
            return AllItemSO.Find(x => x.ID == ID);
        }

    }
}