using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_VictoryUI : MonoBehaviour
	{

        [System.Serializable]
        public class Levelup_edParty
        {
            public int points = 0; //4 points per 1 level up
            public TBC_Party targetParty;
            public bool alreadyInvested = false;
        }

		public Transform parentPartyXP;
        public GameObject levelVictoryScreen;
        public List<TBC_Victory_PartyXP> victoryButtons = new List<TBC_Victory_PartyXP>();
        [FoldoutGroup("Invest Points")] public GameObject investPointScreen;
        [FoldoutGroup("Invest Points")] public List<TBC_PointInvestUIButton> allPointInvestButtons = new List<TBC_PointInvestUIButton>();
        [FoldoutGroup("Invest Points")] public Button button_OkButton;
        [FoldoutGroup("Invest Points")] public Text label_RemainingPoint;
        [FoldoutGroup("Invest Points")] public Text label_Header;
        [FoldoutGroup("Invest Points")] public Image image_Portrait;
        public List<Levelup_edParty> allLeveluped_Parties = new List<Levelup_edParty>();

        public Levelup_edParty GetCurrentParty()
        {
            if (allLeveluped_Parties.Count == 0) return null;
            return allLeveluped_Parties[0];
        }
        private TBC_Party currentParty;
        private bool isExiting = false;


        [FoldoutGroup("DEBUG")]
        [Button("Test_XP Run")]
        public void Inject_TestXP(int xp)
        {
            Debug.Log(SaveData.PartyStat.GetCurrentXP(xp));
        }

        [FoldoutGroup("DEBUG")]
        [Button("Test_LV Run")]
        public void Inject_TestLV(int xp)
        {
            Debug.Log(SaveData.PartyStat.GetCurrentLevel(xp));
        }

        private void OnEnable()
        {
            levelVictoryScreen.gameObject.SetActive(true);

            foreach (var button in victoryButtons)
            {
                button.gameObject.EnableGameobject(false);
            }

            var parties = TurnBasedCombat.Turn.GetAllPartyMembers();

            int i = 0;

            foreach (var button in victoryButtons)
            {
                if (i >= parties.Count) break;
                var party = parties[i];
                button.partyTarget = party;
                button.gameObject.EnableGameobject(true);
                button.XPLeftToRun = TurnBasedCombat.Turn.TotalXPGain;
                button.OverrideStart(party.partyStat.Level);

                int totalLevelups = button.TotalLevelups(TurnBasedCombat.Turn.TotalXPGain);
                if (totalLevelups > 0)
                {
                    CreateLevelup(totalLevelups, party);
                }

                i++;
            }
        }

        public void CreateLevelup(int levels, TBC_Party party)
        {
            Levelup_edParty lep = new Levelup_edParty();
            lep.points = levels * 4;
            lep.targetParty = party;
            lep.alreadyInvested = false;
            allLeveluped_Parties.Add(lep);
        }

        public void OkButton()
        {
            bool allowExit = true;

            foreach(var lep in allLeveluped_Parties)
            {
                if (lep.alreadyInvested == false)
                {
                    allowExit = false;
                    OpenInvestPoint(lep.targetParty);
                    break;
                }
                else
                {
                    LevelUpApply();
                    break;
                }
            }

            allLeveluped_Parties.RemoveAll(x => x.alreadyInvested == true);

            foreach (var lep in allLeveluped_Parties)
            {
                if (lep.alreadyInvested == false)
                {
                    allowExit = false;
                    OpenInvestPoint(lep.targetParty);
                    break;
                }
                else
                {
                    //cannot occur twice!
                }
            }

            if (allowExit)
            {

                isExiting = true;
                //trigger exit scene
            }
        }


        public void OpenInvestPoint(TBC_Party party)
        {
            levelVictoryScreen.gameObject.SetActive(false);
            investPointScreen.gameObject.SetActive(true);

            currentParty = party;

            RefreshUI();
        }

        public void LevelUpApply()
        {
            var party = GetCurrentParty();

            foreach (var button in allPointInvestButtons)
            {
                if (button.statType == TypeBattleStat.DEX)
                {
                    currentParty.partyStat.battleStat.DEX += button.netPoints;
                }
                else if (button.statType == TypeBattleStat.ENE)
                {
                    currentParty.partyStat.battleStat.ENE += button.netPoints;
                }
                else if (button.statType == TypeBattleStat.SHOOT)
                {
                    currentParty.partyStat.battleStat.SHOOT += button.netPoints;
                }
                else if (button.statType == TypeBattleStat.STR)
                {
                    currentParty.partyStat.battleStat.STR += button.netPoints;
                }
                else if (button.statType == TypeBattleStat.VIT)
                {
                    currentParty.partyStat.battleStat.VIT += button.netPoints;
                }

                button.netPoints = 0;
            }

        }

        public void SkillUp(TBC_PointInvestUIButton buttonInvest)
        {
            var party = GetCurrentParty();

            buttonInvest.netPoints++;
            party.points--;
            RefreshUI();
        }
        public void SkillDown(TBC_PointInvestUIButton buttonInvest)
        {
            var party = GetCurrentParty();

            buttonInvest.netPoints--;
            party.points++;
            RefreshUI();
        }

        private void RefreshUI()
        {
            var party = GetCurrentParty();
            label_Header.text = $"{party.targetParty.partyMemberSO.NameDisplay.ToUpper()} LEVEL UP!";
            image_Portrait.sprite = party.targetParty.partyMemberSO.sprite_wide_201px;

            if (party.points <= 0) //no more points to invest
            {
                foreach(var button in allPointInvestButtons)
                {
                    button.button_SkillUp.gameObject.SetActive(false);
                }

                party.alreadyInvested = true;
                button_OkButton.gameObject.SetActive(true);
            }
            else
            {
                foreach (var button in allPointInvestButtons)
                {
                    button.button_SkillUp.gameObject.SetActive(true);
                    if (button.netPoints <= 0)
                    {
                        button.button_SkillDown.gameObject.SetActive(false);
                    }
                    else
                    {
                        button.button_SkillDown.gameObject.SetActive(true);
                    }
                }

                party.alreadyInvested = false;
                button_OkButton.gameObject.SetActive(false);
            }

            label_RemainingPoint.text = $"x{party.points}";

            foreach (var button in allPointInvestButtons)
            {
                int skillLv = 0;

                if (button.statType == TypeBattleStat.DEX)
                {
                    skillLv = currentParty.partyStat.battleStat.DEX;
                }
                else if (button.statType == TypeBattleStat.ENE)
                {
                    skillLv = currentParty.partyStat.battleStat.ENE;
                }
                else if (button.statType == TypeBattleStat.SHOOT)
                {
                    skillLv = currentParty.partyStat.battleStat.SHOOT;
                }
                else if (button.statType == TypeBattleStat.STR)
                {
                    skillLv = currentParty.partyStat.battleStat.STR;
                }
                else if (button.statType == TypeBattleStat.VIT)
                {
                    skillLv = currentParty.partyStat.battleStat.VIT;
                }

                button.label_CurrentSkill.text = $"{skillLv}";
                button.label_NetSkill.text = $"+{button.netPoints}";
                button.label_FinalSkill.text = $"={skillLv + button.netPoints}";
            }
        }


        private void Update()
        {


            foreach (var button in victoryButtons)
            {
                button.Refresh();
            }

            if (isExiting == true)
            {
                Debug.Log("Exit scene!");
                levelVictoryScreen.gameObject.SetActive(false);
                investPointScreen.gameObject.SetActive(false);
                button_OkButton.gameObject.SetActive(false);
            }
        }

    }
}