﻿using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UnityAtoms.Examples
{
    public class EnemyHealthBarManager : MonoBehaviour
    {
        [SerializeField]
        private AtomList _enemies;

        [SerializeField]
        private GameObject _healthBarPrefab;

        [SerializeField]
        private RectTransform _canvasRectTransform;

        void Awake()
        {
            Assert.IsNotNull(_enemies);
            Assert.IsNotNull(_healthBarPrefab);
            Assert.IsNotNull(_canvasRectTransform);
        }

        public void SetupHealthBar(AtomBaseVariable enemyData)
        {
            var enemyDataCollection = (AtomCollection)enemyData;
            var healthBar = Instantiate(_healthBarPrefab).GetComponent<HealthBar>();
            healthBar.transform.SetParent(transform);

            Action<Vector3> positionChangedHandler = default;
            Action<AtomBaseVariable> varAddedHandler = (AtomBaseVariable baseVar) =>
            {
                switch (baseVar.Id)
                {
                    case "Health":
                        var healthVar = (IntVariable)baseVar;
                        healthBar.InitialHealth.Value = healthVar.InitialValue;
                        healthVar.Changed.Register(healthBar.HealthChanged);
                        break;
                    case "Position":
                        var positionVar = (Vector3Variable)baseVar;
                        positionChangedHandler = (pos) =>
                        {
                            Vector2 viewportPos = Camera.main.WorldToViewportPoint(pos);
                            Vector2 healthBarPos = new Vector2(
                                (viewportPos.x * _canvasRectTransform.sizeDelta.x) - (_canvasRectTransform.sizeDelta.x * 0.5f),
                                (viewportPos.y * _canvasRectTransform.sizeDelta.y) - (_canvasRectTransform.sizeDelta.y * 0.5f) + 38f
                            );
                            healthBar.GetComponent<RectTransform>().anchoredPosition = healthBarPos;
                        };
                        positionVar.Changed.Register(positionChangedHandler);
                        break;
                }
            };

            Action<AtomBaseVariable> varRemovedHandler = (AtomBaseVariable baseVar) =>
            {
                switch (baseVar.Id)
                {
                    case "Health":
                        var healthVar = (IntVariable)baseVar;
                        healthVar.Changed.Unregister(healthBar.HealthChanged);
                        break;
                    case "Position":
                        var positionVar = (Vector3Variable)baseVar;
                        positionVar.Changed.Unregister(positionChangedHandler);
                        break;
                }
            };

            enemyDataCollection.Added.Register(varAddedHandler);
            enemyDataCollection.Removed.Register(varRemovedHandler);

            Action<AtomBaseVariable> enemyDataRemovedHandler = default;
            enemyDataRemovedHandler = (enemyDataToBeRemoved) =>
            {
                if (enemyDataToBeRemoved == enemyData)
                {
                    enemyDataCollection.Added.Unregister(varAddedHandler);
                    enemyDataCollection.Removed.Unregister(varRemovedHandler);
                    _enemies.Removed.Unregister(enemyDataRemovedHandler);

                    if (healthBar != null && healthBar.gameObject != null)
                    {
                        Destroy(healthBar.gameObject);
                    }
                }
            };
            _enemies.Removed.Register(enemyDataRemovedHandler);
        }
    }
}