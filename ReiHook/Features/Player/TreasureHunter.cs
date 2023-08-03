using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class TreasureHunter : MonoBehaviour {
        public static void GiveTreasureHunterItems() {
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Shovel", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("MetalDetector", 1);
        }
    }
}
