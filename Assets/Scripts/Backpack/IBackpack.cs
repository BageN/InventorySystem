using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Интерфейс рюкзака
    /// </summary>
    public interface IBackpack {

        void PutFigure (IFigure figure);
        void GetFigure (IFigure figure);
        void SetActiveUI (bool status);

    }
}