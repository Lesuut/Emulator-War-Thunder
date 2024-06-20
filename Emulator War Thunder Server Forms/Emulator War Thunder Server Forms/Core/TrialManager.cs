﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

public class TrialManager
{
    private const string RegistryPath = @"Software\GullSoft\WarThunderEmulatorServer";
    private const string RegistryKey = "RemainingTime3264";
    private static readonly string EncryptionKey = "zdFroV3zyNTEBQLXYA4Pr/R6ef7CHBvijhzt2PQV5Hc=";

    private readonly int defaultValueInSeconds;

    public TrialManager(int defaultValueInSeconds)
    {
        this.defaultValueInSeconds = defaultValueInSeconds;
    }

    public void SaveData(int seconds)
    {
        DateTime expiryDate = DateTime.Now.AddSeconds(seconds);
        SaveExpiryDate(expiryDate);
    }

    public int LoadData()
    {
        string encryptedValue = ReadDataFromRegistry(RegistryKey);
        if (string.IsNullOrEmpty(encryptedValue))
        {
            SaveDefaultExpiryTime();
            return defaultValueInSeconds;
        }

        string expiryDateString = Decrypt(encryptedValue);
        DateTime expiryDate;
        if (!DateTime.TryParse(expiryDateString, out expiryDate))
        {
            SaveDefaultExpiryTime();
            return defaultValueInSeconds;
        }

        TimeSpan remainingTime = expiryDate - DateTime.Now;
        return (int)Math.Max(0, remainingTime.TotalSeconds);
    }

    private void SaveDefaultExpiryTime()
    {
        DateTime expiryDate = DateTime.Now.AddSeconds(defaultValueInSeconds);
        SaveExpiryDate(expiryDate);
    }

    private void SaveExpiryDate(DateTime expiryDate)
    {
        string expiryDateString = expiryDate.ToString();
        string encryptedValue = Encrypt(expiryDateString);
        SaveDataToRegistry(RegistryKey, encryptedValue);
    }

    private void SaveDataToRegistry(string key, string value)
    {
        try
        {
            using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                registryKey.SetValue(key, value);
            }
        }
        catch (UnauthorizedAccessException ex)
        {        
            Console.WriteLine("Failed to access Registry: " + ex.Message);
        }
    }

    private string ReadDataFromRegistry(string key)
    {
        try
        {
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                return registryKey?.GetValue(key)?.ToString();
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Failed to access Registry: " + ex.Message);
            return null;
        }
    }

    private static string Encrypt(string plainText)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private static string Decrypt(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                }
                return Encoding.Unicode.GetString(ms.ToArray());
            }
        }
    }
}
