using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem {
    /// <summary>
    /// Элемент в UI списке рюкзака
    /// </summary>
    public class Item : MonoBehaviour, IItem {

        #region Inspector fields

        // текст с информацией что фигуры нет
        [SerializeField] private GameObject noFigure = null;
        // объект с информацией о фигуре
        [SerializeField] private GameObject dataGo = null;
        [SerializeField] private Text textType = null;
        [SerializeField] private Text textWeight = null;
        [SerializeField] private Text textTitle = null;
        [SerializeField] private Text textId = null;

        #endregion

        #region IItem implementation

        // установить данные для отображения
        public void SetData (FigureType figureType, FigureData data) {
            textType.text = figureType.ToString();
            if (data != null) {
                textWeight.text = data.GetWeight.ToString();
                textTitle.text = data.GetTitle;
                textId.text = data.GetId.ToString();
            }
            // прячем объект с данными если фигуры нет
            dataGo.SetActive(data != null);
            // отображаем информацию если фигуры в рюкзаке нет
            noFigure.SetActive(data == null);
        } 

        #endregion
    }
}