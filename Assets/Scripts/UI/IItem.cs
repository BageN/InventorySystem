using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Интерфейс записи в списке UI
    /// </summary>
    public interface IItem {

        // установить данные для отображения
        void SetData (FigureType figureType, FigureData data);

    }
}
