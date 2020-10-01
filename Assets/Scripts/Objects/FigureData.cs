using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Данные о фигуре
    /// </summary>
    public class FigureData : MonoBehaviour {

        #region Inspector fields

        [SerializeField] private float weight = 0f;
        [SerializeField] private string title = string.Empty;
        [SerializeField] private int id = 0;
        [SerializeField] private FigureType figureType = FigureType.Cube;

        #endregion

        #region Public fields

        public FigureType GetFigureType {
            get {
                return figureType;
            }
        }
        public string GetTitle {
            get {
                return title;
            }
        }
        public float GetWeight {
            get {
                return weight;
            }
        }
        public int GetId {
            get {
                return id;
            }
        }

        #endregion

    }
}
