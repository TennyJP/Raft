using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace ReiHook {
    public class ReiLoader : Monobehaviour {
        public static GameObject pLoader;
        public static void ReiAyanami() {
            pLoader = new GameObject();
            pLoader.AddComponent<UI.Menu>();

            pLoader.AddComponent<Features.Player.UnlimitedHealth>();
            pLoader.AddComponent<Features.Player.NoThirst>();
            pLoader.AddComponent<Features.Player.NoHunger>();
            pLoader.AddComponent<Features.Visuals.ESP>();
            pLoader.AddComponent<Features.Miscellaneous.FlyMode>();
            UnityEngine.GameObject.DontDestroyOnLoad(pLoader);
        }

        public static void ReiUnload() {
            UnityEngine.Object.Destroy(pLoader);
        }
    }
}
