using JetBrains.Annotations;
using ReiHook.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UltimateWater.Internal;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ReiHook.Features.Miscellaneous {
    public class FlyMode : MonoBehaviour {
        private static bool toggleFly = false;
        private void Update() {
            if (RaftSettings.FlyMode) {
                if (Input.GetKeyDown(KeyCode.F1)) {
                    toggleFly = !toggleFly;
                }

                if (toggleFly) {
                    if (SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().flightCamera.enabled == false) { fly(true); }
                }
                else if (!toggleFly) {
                    if (SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().flightCamera.enabled == true) { fly(false); }
                }
            }
        }

        public void fly(bool pToggle) {
            if (toggleFly == true && pToggle == true && SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().flightCamera.enabled == false) {
                SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().flightCamera.Enable(pToggle);
            }
            else if (toggleFly == false && pToggle == false && SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().flightCamera.enabled == true) {
                SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().flightCamera.Disable(true);
            }
        }
    }
}
