using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformManager : MonoBehaviour
{
    public enum TrapType
    {
        None,
        Vent,
        Trap
    }

    [System.Serializable]
    public class TrapSlot
    {
        //public string name;              // Pour repérer (ex: "Bottom Left")
        public Transform placeholder;    // Slot parent
        public TrapType trapType;        // Choix dans l’inspecteur
    }

    public GameObject ventPrefab;
    public GameObject trapPrefab;

    public TrapSlot[] trapSlots = new TrapSlot[6];

    void Start()
    {
        foreach (var slot in trapSlots)
        {
            if (slot.placeholder == null) continue;

            // Récupère l’enfant de base du slot (si existe)
            if (slot.placeholder.childCount > 0)
            {
                Transform baseChild = slot.placeholder.GetChild(0);

                if (slot.trapType == TrapType.None)
                {
                    baseChild.gameObject.SetActive(true); // garder actif
                }
                else
                {
                    baseChild.gameObject.SetActive(false); // cacher
                }
            }

            // Instancier un piège si besoin
            GameObject prefabToSpawn = null;
            switch (slot.trapType)
            {
                case TrapType.Vent: prefabToSpawn = ventPrefab; break;
                case TrapType.Trap: prefabToSpawn = trapPrefab; break;
                case TrapType.None: break;
            }

            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, slot.placeholder.position, slot.placeholder.rotation, slot.placeholder);
            }
        }
    }
}
