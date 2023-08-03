using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateWater.Internal;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class Unlockables : MonoBehaviour {
        public static void UnlockAll() {
            ComponentManager<NoteBook>.Value.UnlockAllNotes();
            Singleton<Inventory_ResearchTable>.Instance.LearnAllRecipesInstantly();
            foreach (Item_Base ItemBase in ItemManager.GetAllItems()) {
                Singleton<Inventory_ResearchTable>.Instance.ResearchBlueprint(ItemBase);
                Singleton<Inventory_ResearchTable>.Instance.Research(ItemBase, true);
            }
            foreach (QuestItemType QuestItemType in (QuestItemType[])Enum.GetValues(typeof(QuestItemType))) {
                ComponentManager<QuestItemManager>.Value.AddQuestItemsNetworked(true, new QuestItem[]
                {
                    new QuestItem(QuestItemManager.GetSOQuestItemFromType(QuestItemType), 1)
                });
            }
        }

        public static void UnlockAchievements() {
            foreach (object Achievements in Enum.GetValues(typeof(AchievementType))) { AchievementHandler.UnlockAchievement((AchievementType)Achievements); }
        }
    }
}
