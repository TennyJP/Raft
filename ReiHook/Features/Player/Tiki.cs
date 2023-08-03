using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class Tiki : MonoBehaviour {
        public static void GiveTiki() {
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Placeable_TikiPole_Feet", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Placeable_TikiPole_Mid_1", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Placeable_TikiPole_Mid_2", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Placeable_TikiPole_Top", 1);
            ComponentManager<Network_Player>.Value.Inventory.AddItem("Placeable_TikiPole_Complete", 1);
        }
    }
}
