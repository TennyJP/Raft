using UnityEngine;
using UnityEngine.AzureSky;

namespace ReiHook.Features.Miscellaneous {
    public class Plants : MonoBehaviour {
        public static void WaterPlants() {
            foreach (Cropplot Cropplots in PlantManager.allCropplots) {
                if (Cropplots.SlotsNeedWater()) { Cropplots.AddWater(true); }
            }
        }

        public static void GrowPlants() {
            WaterPlants();
            SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().PlantManager.ForwardTime(9999f);
        }
    }
}
