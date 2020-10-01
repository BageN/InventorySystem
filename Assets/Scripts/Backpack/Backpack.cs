using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

namespace InventorySystem {
    /// <summary>
    /// Рюкзак
    /// </summary>
    public class Backpack : MonoBehaviour, IBackpack {

        #region Objects

        // позиции в рюкзаке для типов фигур
        [Serializable]
        private class TypePosition {
            public FigureType figureType = FigureType.Cube;
            public Transform point = null;
        }

        #endregion

        #region Inspector fields

        // позиции в рюкзаке для типов
        [SerializeField] private TypePosition[] positions = new TypePosition[] { };
        // точка выброса фигур из рюкзака
        [SerializeField] private Transform exitPoint = null;
        // Контроллер UI рюкзака
        [SerializeField] private BackpackUI backpackUI = null;

        #endregion

        #region Events

        // событие с параметром
        public class FigureEvent : UnityEvent<IFigure> { }
        // событие складываения фигуры в рюкзак
        public static FigureEvent eventAddFigure = new FigureEvent();
        // событие доставания фигуры из рюкзака
        public static FigureEvent eventRemoveFigure = new FigureEvent();

        #endregion

        #region Private variables

        // контейнер фигур
        private Dictionary<FigureType, IFigure> figures = new Dictionary<FigureType, IFigure>()
        { { FigureType.Cube, null },
        { FigureType.Capsule, null },
        { FigureType.Cylinder, null }};

        #endregion

        #region IBackpack implementation

        // положить фигуру в рюкзак
        public void PutFigure (IFigure figure) {
            if (figure == null) {
                return;
            }
            // добавляем фигуру в список
            figures[figure.GetFigureType] = figure;
            // положить фигуру в позицию по типу
            figure.PutToPosition(GetPointForType(figure.GetFigureType));
            if (eventAddFigure != null) {
                eventAddFigure.Invoke(figure);
            }
        }

        // достать фигуру из рюкзака
        public void GetFigure (IFigure figure) {
            if (figure == null) {
                return;
            }
            // убираем фигуру из списка
            figures[figure.GetFigureType] = null;
            // переместить фигуру из рюкзака и бросить
            figure.ExitFromPosition(exitPoint.position);
            if (eventRemoveFigure != null) {
                eventRemoveFigure.Invoke(figure);
            }
        }

        // показать окно со списком предметов
        public void SetActiveUI (bool status) { 
            if (status) {
                backpackUI.Show(figures);
            } else {
                backpackUI.Hide();
            }
        }

        #endregion

        #region Logic

        // получить позицию в рюкзаке по типу
        private Vector3 GetPointForType (FigureType figureType) {
            return positions.FirstOrDefault(pos => pos.figureType == figureType).point.position;
        }

        #endregion
    }
}