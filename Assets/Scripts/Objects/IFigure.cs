using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Интерфейс фигуры
    /// </summary>
    public interface IFigure {

        // начало движения
        void StartMove ();
        // перемещение
        void MoveToPosition (Vector3 point);
        // конец движения
        void EndMove ();
        // положить в позицию и зафиксировать
        void PutToPosition (Vector3 point);
        // переместить в позицию и бросить
        void ExitFromPosition (Vector3 point);

        FigureType GetFigureType { get; }
        FigureData GetFigureData { get; }

    }
}