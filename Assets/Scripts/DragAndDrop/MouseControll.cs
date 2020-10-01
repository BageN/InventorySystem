using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    /// <summary>
    /// Контроллер мыши для Drag & Drop
    /// </summary>
    public class MouseControll : MonoBehaviour {

        #region Inspector fields

        [SerializeField] private Camera mainCamera = null;

        #endregion

        #region Private variables

        private RaycastHit hit;
        private Ray ray;
        // перемещаемая фигура
        private IFigure figure = null;
        // рюкзак
        private IBackpack backpack = null;
        private Vector3 cursorPosition = Vector3.zero;

        #endregion

        #region Unity events

        private void Update () {
            UpdateInput();
            UpdateDrag();
        }

        #endregion

        #region Input

        // события мыши
        private void UpdateInput () {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // посылаем луч из камеры по направлению к курсору, маска по уровню Plane
            if (Physics.Raycast(ray, out hit, 500f, 1 << 8)) {
                cursorPosition = hit.point;
            }
            // если нажата левая клавиша мыши
            if (Input.GetMouseButtonDown(0)) {
                // посылаем луч из камеры по направлению к курсору, маска по уровню Backpack
                if (Physics.Raycast(ray, out hit, 500f, 1 << 9)) {
                    // проверяем интерфейс рюкзака
                    backpack = hit.collider.GetComponent<IBackpack>();
                    if (backpack != null) {
                        // показать UI рюкзака
                        backpack.SetActiveUI(true);
                    }
                // посылаем луч из камеры по направлению к курсору
                } else if (Physics.Raycast(ray, out hit)) {
                    // проверяем интерфейс фигуры
                    figure = hit.collider.GetComponent<IFigure>();
                    if (figure != null) {
                        figure.StartMove();
                    }
                }

            }
            // если держим фигуру и отпустили клавишу мыши
            if (Input.GetMouseButtonUp(0)) {
                if (figure != null) {
                    figure.EndMove();
                }
                // посылаем луч из камеры по направлению к курсору, маска по уровню Backpack
                if (Physics.Raycast(ray, out hit, 500f, 1 << 9)) {
                    // проверяем интерфейс рюкзака
                    IBackpack backpack = hit.collider.GetComponent<IBackpack>();
                    if (backpack != null) {
                        if (figure != null) {
                            // кладем фигуру в рюкзак
                            backpack.PutFigure(figure);
                        // посылаем луч из камеры по направлению к курсору
                        } else if (Physics.Raycast(ray, out hit, 500f)) {
                            // проверяем интерфейс фигуры
                            figure = hit.collider.GetComponent<IFigure>();
                            if (figure != null) {
                                backpack.GetFigure(figure);
                            }
                        }
                    }
                }
                if (backpack != null) {
                    // скрыть UI рюкзака
                    backpack.SetActiveUI(false);
                }
                backpack = null;
                figure = null;
            }
        }

        #endregion

        #region Drag & Drop

        // перемещение предмета
        private void UpdateDrag () {
            if (figure == null) {
                return;
            }
            figure.MoveToPosition(new Vector3(cursorPosition.x, cursorPosition.y + 2f, cursorPosition.z));
        }

        #endregion
    }
}