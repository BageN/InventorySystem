using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace InventorySystem {
    /// <summary>
    /// Класс взаимодействия с сервером
    /// </summary>
    public class ServerController : MonoBehaviour {

        #region Unity events

        private void Awake () {
            Backpack.eventAddFigure.AddListener(AddFigure);
            Backpack.eventRemoveFigure.AddListener(RemoveFigure);
        }
        
        private void OnDestroy () {
            Backpack.eventAddFigure.RemoveListener(AddFigure);
            Backpack.eventRemoveFigure.RemoveListener(RemoveFigure);
        }

        #endregion

        #region Events

        // событие складываения фигуры в рюкзак
        private void AddFigure (IFigure figure) {
            StartCoroutine(SendToServer("add", figure));
        }

        // событие доставания фигуры в рюкзак
        private void RemoveFigure (IFigure figure) {
            StartCoroutine(SendToServer("remove", figure));
        }

        #endregion

        #region Logic 

        // отправить статус складываения/доставания фигуры из рюкзака
        private IEnumerator SendToServer (string status, IFigure figure) {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection(string.Format("id={0}&status={1}", figure.GetFigureData.GetId, status)));

            UnityWebRequest www = UnityWebRequest.Post("https://dev3r02.elysium.today/inventory/status", formData);
            www.SetRequestHeader("auth", "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else { 
                string responseText = www.downloadHandler.text;
                //Debug.Log("Response Text:" + responseText);
            }
        }

        #endregion

    }
}