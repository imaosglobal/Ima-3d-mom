using System.Collections.Generic;
using UnityEngine;

namespace Ima.Characters
{
    public class GearManager : MonoBehaviour
    {
        [System.Serializable]
        public class GearSlot
        {
            public string id;
            public GameObject gearObject;
        }

        public List<GearSlot> gear = new List<GearSlot>();

        public void Equip(string id)
        {
            foreach (var g in gear)
            {
                g.gearObject.SetActive(g.id == id);
            }
        }

        public void UnequipAll()
        {
            foreach (var g in gear)
                g.gearObject.SetActive(false);
        }
    }
}