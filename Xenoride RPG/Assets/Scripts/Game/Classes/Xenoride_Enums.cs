using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using System;

namespace Xenoride
{

    [System.Serializable]
    public class EffectUnit
    {
        public EffectType EffectType;
        public float Value = 0f;
    }

    public enum TypeBattleStat
    {
        VIT, //vitality
        STR, //strength
        SHOOT, //shooting
        DEX, //dexterity
        ENE //energy
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

    public class Item
    {

        public enum ItemCategory
        {
            Loot,
            Key,
            Weapon,
            Consumable
        }

        public enum WeaponCategory
        {
            Sword,
            Gun,
            Spear
        }

        [System.Serializable]
        public class ItemData
        {
            public string ID = "Phoenix";
            public int Guid = 0;
            public int Count = 1;
            public ItemCategory Category;
        }

        [System.Serializable]
        public class Inventory
        {
            public List<ItemData> allItemDatas = new List<ItemData>();
            public int CurrentIndex = 0;

            //search
            public ItemData SearchByID(string ID)
            {
                return allItemDatas.Find(x => x.ID == ID);
            }

            [Button("Add Item")] //Create new item from scratch
            public ItemData AddItem(ItemSO itemInventory, int count = 1)
            {
                ItemData itemDataSave = null;


                if (SearchByID(itemInventory.ID) != null && itemInventory.itemType != ItemCategory.Weapon)
                {
                    itemDataSave = SearchByID(itemInventory.ID);
                    itemDataSave.Count += count;

                }
                else
                {
                    itemDataSave = new ItemData();
                    itemDataSave.ID = itemInventory.ID;
                    itemDataSave.Guid = CurrentIndex;
                    itemDataSave.Category = itemInventory.itemType;
                    itemDataSave.Count = count;
                    allItemDatas.Add(itemDataSave);
                }

                CurrentIndex++;
                return itemDataSave;
            }

            public ItemData AddItemGenericSafe(ItemSO itemInventory, ItemData _itemDat, int count = 1)
            {
                ItemData itemDataSave = null;


                if (SearchByID(itemInventory.ID) != null
                    && itemInventory.itemType != ItemCategory.Weapon)
                {
                    itemDataSave = SearchByID(itemInventory.ID);
                    itemDataSave.Count += count;

                }
                else
                {
                    itemDataSave = new ItemData();
                    itemDataSave.ID = itemInventory.ID;
                    itemDataSave.Guid = CurrentIndex;
                    itemDataSave.Category = itemInventory.itemType;
                    itemDataSave.Count = count;
                    allItemDatas.Add(itemDataSave);
                }

                CurrentIndex++;
                return itemDataSave;
            }

            public void RemoveItem(ItemData targetData, int count = 1)
            {
                targetData.Count -= count;

                if (targetData.Count <= 0)
                {
                    allItemDatas.Remove(targetData);
                }

            }

            public void RemoveAllItemByType(ItemCategory category)
            {
                var allItemCopyList = new List<ItemData>();
                allItemCopyList.AddRange(allItemDatas);

                foreach (var item in allItemCopyList)
                {
                    var itemClass = Engine.Assets.GetItem(item.ID);

                    if (itemClass.itemType == category)
                    {
                        allItemDatas.Remove(item);
                    }
                }
            }

            public void RemoveItem(string ID, int count = 1)
            {
                ItemData targetData = SearchByID(ID);

                if (targetData != null)
                    RemoveItem(targetData, count);
                else
                {
                    Debug.LogError($"Item doesn't exist: {ID}");
                }

            }

            public int Count(ItemSO itemInventory)
            {
                int count = 0;

                count = Count(itemInventory.ID);


                return count;
            }

            public int Count(string ID)
            {
                int count = 0;

                var itemData = SearchByID(ID);

                if (itemData != null)
                {
                    count += itemData.Count;
                }

                return count;
            }


            /// <summary>
            /// Transfer item from this inventory to targeted inventory.
            /// </summary>
            /// <param name="target"></param>
            /// <param name="currentIndex">Selected index of current inventory.</param>
            public ItemData TransferTo(Inventory target, int currentIndex)
            {
                var itemDat = allItemDatas[currentIndex];
                var itemClass = Engine.Assets.GetItem(itemDat.ID);

                if (itemClass.itemType == ItemCategory.Weapon)
                {
                    target.allItemDatas.Add(itemDat);
                }
                else
                {
                    target.AddItem(itemClass, itemDat.Count);
                }
                allItemDatas.Remove(itemDat);
                return itemDat;
            }


            [Button("Validate GUID")] //Revalidate guids
            public void ValidateInventory()
            {
                List<int> occupiedGuids = new List<int>();

                foreach(var itemDat in allItemDatas)
                {
                    int count1 = 0;

                    while (occupiedGuids.Contains(itemDat.Guid))
                    {
                        if (count1 > 1300) break; //give up
                        CurrentIndex++;
                        count1++;
                        itemDat.Guid = CurrentIndex;
                    }

                    occupiedGuids.Add(itemDat.Guid);
                }
            }
        }

    }
}