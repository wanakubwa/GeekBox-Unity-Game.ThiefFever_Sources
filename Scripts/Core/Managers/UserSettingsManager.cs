using GeekBox.OdinSerializer;
using System;
using System.IO;
using UnityEngine;

public class UserSettingsManager : ManagerSingletonBase<UserSettingsManager>
{
    #region MEMEBRS

    [SerializeField]
    private string fileName = "UserSettings";

    #endregion

    #region PROPERTIES

    public string FileName { get => fileName; }

    public bool IsAudioMuted { get; set; } = false;
    public bool IsVibrationEnabled{ get; set; } = true;

    #endregion

    #region FUNCTIONS

    protected override void OnEnable()
    {
        base.OnEnable();

        UserSettingsManagerMemmento managerMemento = null;
        string savePath = GetSavePath();
        if (File.Exists(savePath) == true)
        {
            DataFormat dataFormat = DataFormat.Binary;

            byte[] bytes = File.ReadAllBytes(savePath);
            managerMemento = SerializationUtility.DeserializeValue<UserSettingsManagerMemmento>(bytes, dataFormat);
        }

        if (managerMemento != null)
        {
            IsAudioMuted = managerMemento.IsAudioMuted;
            IsVibrationEnabled = managerMemento.IsVibrationEnabled;
        }
        else
        {
            SetDefaultData();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        DataFormat dataFormat = DataFormat.Binary;
        string savePath = GetSavePath();
        UserSettingsManagerMemmento managerMemento = new UserSettingsManagerMemmento();
        managerMemento.SetCachedAudioMuted(AudioManager.Instance.IsAudioMute);
        managerMemento.SetVibrationEnabled(IsVibrationEnabled);

        byte[] bytes = SerializationUtility.SerializeValue(managerMemento, dataFormat);
        File.WriteAllBytes(savePath, bytes);
    }

    private void SetDefaultData()
    {
        IsAudioMuted = false;
        IsVibrationEnabled = true;
    }

    private string GetSavePath()
    {
        return Path.Combine(PathFacade.CurrentPath.DataSavePath, FileName + ".sma");
    }

    #endregion

    #region CLASS_ENUMS

    [Serializable]
    public class UserSettingsManagerMemmento
    {
        #region Fields

        [SerializeField]
        private bool isAudioMuted;
        [SerializeField]
        private bool isVibrationEnabled = true;

        #endregion

        #region Propeties
        public bool IsAudioMuted { get => isAudioMuted; set => isAudioMuted = value; }
        public bool IsVibrationEnabled { get => isVibrationEnabled; set => isVibrationEnabled = value; }

        #endregion

        #region Methods

        public void SetCachedAudioMuted(bool isMuted)
        {
            IsAudioMuted = isMuted;
        }

        public void SetVibrationEnabled(bool isOn)
        {
            IsVibrationEnabled = isOn;
        }

        #endregion

        #region Handlers



        #endregion
    }

    #endregion
}
