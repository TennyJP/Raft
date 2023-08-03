using ReiHook.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace ReiHook.Features.Player {
    public class UnlimitedHealth : MonoBehaviour {
        private void Update() {
            if (RaftSettings.UnlimitedHealth) {
                if (SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_health.Value < SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_health.Max) {
                    SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_health.Value = SingletonGeneric<Network_Entity>.Singleton.Network.GetLocalPlayer().Stats.stat_health.Max;
                }
            }
        }
    }
}
