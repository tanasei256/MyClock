﻿namespace MyClock
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      label1 = new Label();
      timer1 = new System.Windows.Forms.Timer(components);
      audioDeviceComboBox = new ComboBox();
      SuspendLayout();
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.BackColor = Color.Black;
      label1.Font = new Font("Yu Gothic UI", 48F, FontStyle.Bold);
      label1.ForeColor = Color.SkyBlue;
      label1.Location = new Point(0, -3);
      label1.Name = "label1";
      label1.Size = new Size(196, 86);
      label1.TabIndex = 0;
      label1.Text = "00:00";
      label1.TextAlign = ContentAlignment.MiddleCenter;
      label1.Click += label1_Click;
      // 
      // timer1
      // 
      timer1.Enabled = true;
      timer1.Interval = 1000;
      timer1.Tick += timer1_Tick;
      // 
      // audioDeviceComboBox
      // 
      audioDeviceComboBox.BackColor = Color.Black;
      audioDeviceComboBox.DrawMode = DrawMode.OwnerDrawFixed;
      audioDeviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      audioDeviceComboBox.ForeColor = Color.White;
      audioDeviceComboBox.FormattingEnabled = true;
      audioDeviceComboBox.Location = new Point(12, 86);
      audioDeviceComboBox.Name = "audioDeviceComboBox";
      audioDeviceComboBox.Size = new Size(392, 24);
      audioDeviceComboBox.TabIndex = 1;
      audioDeviceComboBox.DrawItem += audioDeviceComboBox_DrawItem;
      audioDeviceComboBox.SelectedIndexChanged += audioDeviceComboBox_SelectedIndexChanged;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.Black;
      ClientSize = new Size(191, 140);
      Controls.Add(audioDeviceComboBox);
      Controls.Add(label1);
      ForeColor = Color.White;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "Form1";
      Text = "Form1";
      Load += Form1_Load;
      KeyDown += Form1_KeyDown;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Label label1;
    private System.Windows.Forms.Timer timer1;
    private ComboBox audioDeviceComboBox;
  }
}
