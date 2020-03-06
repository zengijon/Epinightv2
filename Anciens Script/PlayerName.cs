﻿
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Epinight.Menus
{
    public class PlayerName : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button ContinueButton = null;


        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start()
        {
            SetUpInputField();
        }

        private void SetUpInputField()
        {
            if(!PlayerPrefs.HasKey(PlayerPrefsNameKey)){return;}

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            ContinueButton.interactable = string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;
            
            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
        }

    }
}

