using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateWater.Internal;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class Armor : MonoBehaviour {
        public static void GiveArmor() {
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Equipment_LeatherHelmet", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Equipment_LeatherChest", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Equipment_LeatherLegs", 1);
        }
    }
}
