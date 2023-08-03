using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReiHook.Features.Miscellaneous {
    public class ForceAnchor : MonoBehaviour {
        public static bool forceAnchor = false;
        public static void AddAnchor() {
            forceAnchor = !forceAnchor;
            if (forceAnchor) { ComponentManager<Raft>.Value.AddAnchor(false, null); }
            else { ComponentManager<Raft>.Value.RemoveAnchor(10); }
        }
    }
}
