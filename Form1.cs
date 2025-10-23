using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

namespace MyClock
{
  public partial class Form1 : Form
  {
    public DateTime g_time;
    
    public Form1()
    {
      InitializeComponent();

      AudioDeviceManager.ListOutputDevicesToDebug();

      g_time = DateTime.Now;
      // フォームがキーイベントをコントロールより先に受け取るようにする
      this.KeyPreview = true;
      SetupAudioDeviceSelector();
      SetLabel1Time();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      DateTime t = DateTime.Now;
      if (t.Minute != g_time.Minute)
      {
        g_time = t;
        SetLabel1Time();
      }
    }

    public void SetLabel1Time()
    {
      //debug g_time = DateTime.Parse("01:03");
      label1.Text = g_time.ToString("HH:mm");
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      // Escキーが押されたらアプリケーションを終了する
      if (e.KeyCode == Keys.Escape)
      {
        this.Close();
      }
    }

    private void SetupAudioDeviceSelector()
    {
      var devices = AudioDeviceManager.GetOutputDevices();
      audioDeviceComboBox.DataSource = devices;
      audioDeviceComboBox.DisplayMember = "FriendlyName";
      audioDeviceComboBox.ValueMember = "Id";

      var defaultDevice = AudioDeviceManager.GetDefaultOutputDevice();
      if (defaultDevice != null)
      {
        // ComboBoxの選択を現在のデフォルトデバイスに設定
        audioDeviceComboBox.SelectedValue = defaultDevice.ID;
      }
    }

    private void audioDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (audioDeviceComboBox.SelectedItem is AudioDevice selectedDevice)
      {
        Debug.WriteLine($"オーディオデバイスが選択されました: {selectedDevice.FriendlyName} (ID: {selectedDevice.ID})");
        AudioDeviceManager.SetDefaultOutputDevice(selectedDevice.ID);
      }
    }

    private void audioDeviceComboBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      // ComboBoxが空、またはデザイン時の描画をスキップ
      if (e.Index < 0)
      {
        return;
      }

      // 背景の描画
      // 項目が選択されているかどうかで背景色を決定
      bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
      Color backgroundColor = isSelected ? Color.FromArgb(0, 120, 215) : Color.Black; // 選択時は青、非選択時は黒
      using (var backgroundBrush = new SolidBrush(backgroundColor))
      {
        e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
      }

      // テキストの描画
      string text = audioDeviceComboBox.GetItemText(audioDeviceComboBox.Items[e.Index]) ?? string.Empty;
      Color textColor = isSelected ? Color.White : Color.White; // 常に白
      using (var textBrush = new SolidBrush(textColor))
      {
        e.Graphics.DrawString(text, e.Font ?? audioDeviceComboBox.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
      }

      e.DrawFocusRectangle();
    }
  }
}
