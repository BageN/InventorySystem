using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Контроллер UI рюкзака
    /// </summary>
    public class BackpackUI : MonoBehaviour {

        #region Inspector fields

        // окно
        [SerializeField] private GameObject window = null;
        // контейнер елементов
        [SerializeField] private Transform group = null;
        // префаб елемента
        [SerializeField] private GameObject itemPrefab = null;

        #endregion

        #region Unity events

        private void Awake () {
            // скрываем при старте окно
            Hide();
        }

        #endregion

        #region Logic

        // отобразить список элементов
        public void Show (Dictionary<FigureType, IFigure> figures) {
            foreach (FigureType figureType in figures.Keys) {
                // инстанциируем айтем и передаем в него данные для отображения
                GameObject itemGo = Instantiate(itemPrefab, group);
                IItem item = itemGo.GetComponent<IItem>();
                IFigure figure = figures[figureType];
                item.SetData(figureType, figure != null ? figure.GetFigureData : null);
            }
            window.SetActive(true);
        }

        // Скрыть список
        public void Hide () {
            window.SetActive(false);
            // очистить список
            foreach (Transform tr in group) {
                Destroy(tr.gameObject);
            }
        }

        #endregion
    }
}
