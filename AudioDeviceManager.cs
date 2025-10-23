using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi;

namespace MyClock;

public record AudioDevice(string ID, string FriendlyName);

public class AudioDeviceManager
{
  public static List<AudioDevice> GetOutputDevices()
  {
    using var enumerator = new MMDeviceEnumerator();
    var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
    return devices.Select(d => new AudioDevice(d.ID, d.FriendlyName)).ToList();
  }

  public static AudioDevice? GetDefaultOutputDevice()
  {
    using var enumerator = new MMDeviceEnumerator();
    try
    {
      using var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
      return new AudioDevice(defaultDevice.ID, defaultDevice.FriendlyName);
    }
    catch (Exception)
    {
      // デフォルトデバイスが見つからない場合
      return null;
    }
  }

  public static void SetDefaultOutputDevice(string deviceId)
  {
    var policyConfig = (IPolicyConfig)new PolicyConfigClient();
    // Role.Console, Role.Multimedia, Role.Communications のすべてに設定
    policyConfig.SetDefaultEndpoint(deviceId, Role.Console);
    policyConfig.SetDefaultEndpoint(deviceId, Role.Multimedia);
    policyConfig.SetDefaultEndpoint(deviceId, Role.Communications);
    Marshal.ReleaseComObject(policyConfig!);
  }

  /// <summary>
  /// 利用可能なオーディオデバイスをデバッグ出力します。
  /// </summary>
  public static void ListOutputDevicesToDebug()
  {
    Debug.WriteLine("利用可能なオーディオ出力デバイス：");
    Debug.WriteLine("-------------------");
    var devices = GetOutputDevices();
    if (!devices.Any())
    {
      Debug.WriteLine("デバイスが見つかりませんでした。");
      return;
    }

    int deviceNum = 1;
    foreach (var device in devices)
    {
      Debug.WriteLine($"デバイス {deviceNum}");
      Debug.WriteLine($" フレンドリ名：{device.FriendlyName}");
      Debug.WriteLine($" デバイスID：{device.ID}");
      deviceNum++;
    }

    var defaultDevice = GetDefaultOutputDevice();
    Debug.WriteLine("-------------------");
    if (defaultDevice != null)
    {
      Debug.WriteLine($"現在のデバイス：{defaultDevice.FriendlyName}");
      Debug.WriteLine($" デバイスID：{defaultDevice.ID}");
    }
    else
    {
      Debug.WriteLine("現在のデバイスが見つかりませんでした。");
    }
  }

  // Windows Core Audio APIのCOMインターフェースを定義
  [ComImport, Guid("F8679F50-850A-41CF-9C72-430F290290C8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IPolicyConfig
  {
    [PreserveSig]
    int GetMixFormat(string pszDeviceName, out IntPtr ppFormat);
    [PreserveSig]
    int GetDeviceFormat(string pszDeviceName, bool bDefault, out IntPtr ppFormat);
    [PreserveSig]
    int ResetDeviceFormat(string pszDeviceName);
    [PreserveSig]
    int SetDeviceFormat(string pszDeviceName, IntPtr pEndpointFormat, IntPtr pMixFormat);
    [PreserveSig]
    int GetProcessingPeriod(string pszDeviceName, bool bDefault, out long pmftDefaultPeriod, out long pmftMinimumPeriod);
    [PreserveSig]
    int SetProcessingPeriod(string pszDeviceName, ref long pmftPeriod);
    [PreserveSig]
    int GetShareMode(string pszDeviceName, out IntPtr pMode);
    [PreserveSig]
    int SetShareMode(string pszDeviceName, IntPtr mode);
    [PreserveSig]
    int GetPropertyValue(string pszDeviceName, ref PROPERTYKEY key, out PROPVARIANT pv);
    [PreserveSig]
    int SetPropertyValue(string pszDeviceName, ref PROPERTYKEY key, ref PROPVARIANT pv);
    [PreserveSig]
    int SetDefaultEndpoint(string pszDeviceName, Role role);
    [PreserveSig]
    int SetEndpointVisibility(string pszDeviceName, bool bVisible);
  }

  [ComImport, Guid("870AF99C-171D-4F9E-AF0D-E63DF40C2BC9")]
  internal class PolicyConfigClient
  {
  }

  // これらの構造体はIPolicyConfigインターフェースで必要
  [StructLayout(LayoutKind.Sequential)]
  internal struct PROPERTYKEY { public Guid fmtid; public uint pid; }
  [StructLayout(LayoutKind.Explicit)]
  internal struct PROPVARIANT { [FieldOffset(0)] public short vt; [FieldOffset(8)] public IntPtr pszVal; }
}
