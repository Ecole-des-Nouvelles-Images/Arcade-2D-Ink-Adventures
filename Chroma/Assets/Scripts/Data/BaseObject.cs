using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class BaseObject : MonoBehaviour
    {
        public int playerID;
        public Color objectColor;

        // Méthode d'initialisation pour enregistrer l'observateur
        void Start()
        {
            // Enregistrement en tant qu'observateur auprès du CharacterController
            FindObjectOfType<PlayerController>().OnColorChange += HandleColorChange;

            // Initialiser la couleur de l'objet en fonction de l'ID
            objectColor = GetColorByID(playerID);
        }

        // Méthode appelée lorsqu'un événement de changement de couleur est déclenché
        private void HandleColorChange(Color newColor)
        {
            // Mettre à jour la logique de l'objet en fonction de la nouvelle couleur
            // ...
        }
        private static Color GetColorByID(int id)
        {
            switch (id)
            {
                case 1:
                    return Color.red;
                case 2:
                    return Color.green;
                case 3:
                    return Color.blue;
                case 4:
                    return Color.yellow;
                case 5:
                    return Color.magenta;
                case 6:
                    return Color.cyan;
                default:
                    return Color.white;
            }
        }
    }
}

