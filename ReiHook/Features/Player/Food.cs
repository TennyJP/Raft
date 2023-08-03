using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateWater.Internal;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class Food : MonoBehaviour {
        public static void GiveFood() {
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(11).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(12).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(13).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(14).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(99).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(133).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(134).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(135).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(136).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(137).UniqueName, 10);
            ComponentManager<Network_Player>.Value.Inventory.AddItem(ItemManager.GetItemByIndex(288).UniqueName, 10);
        }
    }
}
