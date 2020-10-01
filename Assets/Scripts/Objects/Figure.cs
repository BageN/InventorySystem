using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Объект фигуры
    /// </summary>
    public class Figure : MonoBehaviour, IFigure {

        #region Private variables
        // ссылки на компоненты фигуры
        private Transform cachTransform;
        private Rigidbody cachRigidbody;
        // данные фигуры
        private FigureData figureData = null;

        #endregion

        #region Unity events

        private void Awake () {
            // кешируем ссылки
            cachTransform = transform;
            cachRigidbody = GetComponent<Rigidbody>();
            figureData = GetComponent<FigureData>();
        }

        #endregion

        #region IFigure implementation

        public FigureType GetFigureType {
            get {
                return figureData.GetFigureType;
            }
        }

        public FigureData GetFigureData {
            get {
                return figureData;
            }
        }

        // начало движения
        public void StartMove () {
            cachRigidbody.useGravity = false;
        }

        // перемещение
        public void MoveToPosition (Vector3 point) {
            cachTransform.position = Vector3.Lerp(cachTransform.position, point, 0.2f);
        }

        // конец движения
        public void EndMove () {
            cachRigidbody.useGravity = true;
        }

        // положить в позицию и зафиксировать
        public void PutToPosition (Vector3 point) {
            cachRigidbody.isKinematic = true;
            iTween.MoveTo(gameObject, point, 2f);
            iTween.RotateTo(gameObject, Vector3.zero, 2f);
        }

        // переместить в позицию и бросить
        public void ExitFromPosition (Vector3 point) {
            iTween.MoveTo(gameObject, iTween.Hash("x", point.x, "y", point.y, "z", point.z, "time", 1f, "oncomplete", "OnCompleteExit"));
        }

        // активировать физический объект
        private void OnCompleteExit () {
            cachRigidbody.isKinematic = false;
        }

        #endregion

    }
}