using System;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Assertions;

/// SPDX-License-Identifier: MIT
namespace UnityAtoms.Examples
{
    public class EnemyHealthBarManager : MonoBehaviour
    {
        [SerializeField] private AtomList _items;
        [SerializeField] private GameObject _prefabToSpawn;
        void Awake()
        {
            Assert.IsNotNull(_items);
            Assert.IsNotNull(_prefabToSpawn);
        }
        public void SpawnItem(AtomBaseVariable itemData)
        {
            var itemDataCollection = (AtomCollection) itemData;
            var spawnedObject = Instantiate(_prefabToSpawn, transform);
            var proxies = spawnedObject.GetComponents<IVariableProxy>();

            void VarAddedHandler(AtomBaseVariable baseVar)
            {
                foreach (var proxy in proxies)
                {
                    if (proxy.key() == baseVar.Id)
                    {
                        proxy.registerMe(baseVar);
                    }
                }
            }

            void VarRemovedHandler(AtomBaseVariable baseVar)
            {
                foreach (var proxy in proxies)
                {
                    if (proxy.key() == baseVar.Id)
                    {
                        proxy.unregisterMe(baseVar);
                    }
                }
            }

            itemDataCollection.Added.Register(VarAddedHandler);
            itemDataCollection.Removed.Register(VarRemovedHandler);

            Action<AtomBaseVariable> itemDataRemovedHandler = default;
            itemDataRemovedHandler = (itemDataToBeRemoved) =>
            {
                if (itemDataToBeRemoved == itemData)
                {
                    itemDataCollection.Added.Unregister(VarAddedHandler);
                    itemDataCollection.Removed.Unregister(VarRemovedHandler);
                    _items.Removed.Unregister(itemDataRemovedHandler);

                    if (spawnedObject != null && spawnedObject.gameObject != null)
                    {
                        Destroy(spawnedObject.gameObject);
                    }
                }
            };
            _items.Removed.Register(itemDataRemovedHandler);
        }
    }
}
