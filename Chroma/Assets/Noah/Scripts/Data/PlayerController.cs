using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class PlayerController : MonoBehaviour
    {

        // Déclaration de l'événement pour le changement de couleur
        public event Action<Color> OnColorChange;

        // Méthode pour changer la couleur du personnage
        public void ChangeColor(Color newColor)
        {
            // Changer la couleur du personnage
            

            // Notifier les observateurs du changement de couleur
            OnColorChange?.Invoke(newColor);
        }

    }
}
