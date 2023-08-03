using ReiHook.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReiHook.Features.Player {
    public class NoHunger : MonoBehaviour {
        private void Update() {
            if (RaftSettings.NoHunger) {
                if (SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_hunger.Normal.NormalValue < SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_hunger.Normal.Max) {
                    SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_hunger.Normal.NormalValue = SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_hunger.Normal.Max;
                }
            }
        }
    }
}
