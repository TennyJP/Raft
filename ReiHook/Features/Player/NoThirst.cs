using ReiHook.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class NoThirst : MonoBehaviour {
        private void Update() {
            if (RaftSettings.NoThirst) {
                if (SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_thirst.Normal.NormalValue < SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_thirst.Normal.Max) {
                    SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_thirst.Normal.NormalValue = SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_thirst.Normal.Max;
                }
            }
        }
    }
}
