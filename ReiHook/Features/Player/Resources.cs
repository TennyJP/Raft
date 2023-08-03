using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateWater.Internal;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class Resources : MonoBehaviour {
        public static void GiveResources() {
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Brick_Dry", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Glass", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("MetalIngot", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Nail", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Plank", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Plastic", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Rope", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Scrap", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("SeaVine", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Stone", 20);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Thatch", 20);
        }
    }
}
