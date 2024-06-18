﻿namespace Emulator_War_Thunder_Server_Forms
{
    partial class MainForm
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
            consoleBox = new ListBox();
            GearsQuantityForwardInput = new TextBox();
            label1 = new Label();
            label2 = new Label();
            GearsQuantityBackInput = new TextBox();
            CurrentGearText = new Label();
            jsonDebugText = new TextBox();
            sayHiButton = new Button();
            Amunition1Count = new TextBox();
            Amunition2Count = new TextBox();
            ReloadTimeTextBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            checkBox1 = new CheckBox();
            RestartReloadingButton = new Button();
            label7 = new Label();
            label8 = new Label();
            label6 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            DisableInputSimulationLable = new Label();
            trackBar1 = new TrackBar();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // consoleBox
            // 
            consoleBox.BackColor = Color.Black;
            consoleBox.ForeColor = Color.DeepPink;
            consoleBox.FormattingEnabled = true;
            consoleBox.ItemHeight = 15;
            consoleBox.Location = new Point(175, 12);
            consoleBox.Name = "consoleBox";
            consoleBox.Size = new Size(613, 409);
            consoleBox.TabIndex = 0;
            // 
            // GearsQuantityForwardInput
            // 
            GearsQuantityForwardInput.Location = new Point(4, 30);
            GearsQuantityForwardInput.Name = "GearsQuantityForwardInput";
            GearsQuantityForwardInput.Size = new Size(42, 23);
            GearsQuantityForwardInput.TabIndex = 1;
            GearsQuantityForwardInput.Text = "6";
            GearsQuantityForwardInput.TextAlign = HorizontalAlignment.Center;
            GearsQuantityForwardInput.TextChanged += GearsQuantityForwardInput_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 12);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 2;
            label1.Text = "Gears Quantity Forward";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 56);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 4;
            label2.Text = "Gears Quantity Back";
            // 
            // GearsQuantityBackInput
            // 
            GearsQuantityBackInput.Location = new Point(4, 74);
            GearsQuantityBackInput.Name = "GearsQuantityBackInput";
            GearsQuantityBackInput.Size = new Size(42, 23);
            GearsQuantityBackInput.TabIndex = 3;
            GearsQuantityBackInput.Text = "1";
            GearsQuantityBackInput.TextAlign = HorizontalAlignment.Center;
            // 
            // CurrentGearText
            // 
            CurrentGearText.AutoSize = true;
            CurrentGearText.Location = new Point(4, 100);
            CurrentGearText.Name = "CurrentGearText";
            CurrentGearText.Size = new Size(77, 15);
            CurrentGearText.TabIndex = 5;
            CurrentGearText.Text = "Current Gear:";
            // 
            // jsonDebugText
            // 
            jsonDebugText.BackColor = Color.FromArgb(0, 64, 0);
            jsonDebugText.ForeColor = Color.Yellow;
            jsonDebugText.Location = new Point(175, 430);
            jsonDebugText.Multiline = true;
            jsonDebugText.Name = "jsonDebugText";
            jsonDebugText.Size = new Size(613, 99);
            jsonDebugText.TabIndex = 6;
            jsonDebugText.Text = "JSON";
            // 
            // sayHiButton
            // 
            sayHiButton.Location = new Point(12, 506);
            sayHiButton.Name = "sayHiButton";
            sayHiButton.Size = new Size(75, 23);
            sayHiButton.TabIndex = 7;
            sayHiButton.Text = "Say Hi";
            sayHiButton.UseVisualStyleBackColor = true;
            sayHiButton.Click += SayHiButton_Click;
            // 
            // Amunition1Count
            // 
            Amunition1Count.Location = new Point(4, 231);
            Amunition1Count.Name = "Amunition1Count";
            Amunition1Count.Size = new Size(42, 23);
            Amunition1Count.TabIndex = 8;
            Amunition1Count.Text = "10";
            Amunition1Count.TextAlign = HorizontalAlignment.Center;
            // 
            // Amunition2Count
            // 
            Amunition2Count.Location = new Point(4, 285);
            Amunition2Count.Name = "Amunition2Count";
            Amunition2Count.Size = new Size(42, 23);
            Amunition2Count.TabIndex = 11;
            Amunition2Count.Text = "0";
            Amunition2Count.TextAlign = HorizontalAlignment.Center;
            // 
            // ReloadTimeTextBox
            // 
            ReloadTimeTextBox.Location = new Point(101, 176);
            ReloadTimeTextBox.Name = "ReloadTimeTextBox";
            ReloadTimeTextBox.Size = new Size(42, 23);
            ReloadTimeTextBox.TabIndex = 12;
            ReloadTimeTextBox.Text = "10.0";
            ReloadTimeTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 179);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 13;
            label3.Text = "Recharge Time";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(144, 179);
            label4.Name = "label4";
            label4.Size = new Size(25, 15);
            label4.TabIndex = 14;
            label4.Text = "Sec";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(52, 234);
            label5.Name = "label5";
            label5.Size = new Size(120, 15);
            label5.TabIndex = 15;
            label5.Text = "Ammunition 1 Count";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(4, 151);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(97, 19);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "Use Recharge";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // RestartReloadingButton
            // 
            RestartReloadingButton.AllowDrop = true;
            RestartReloadingButton.Location = new Point(4, 314);
            RestartReloadingButton.Name = "RestartReloadingButton";
            RestartReloadingButton.Size = new Size(157, 23);
            RestartReloadingButton.TabIndex = 18;
            RestartReloadingButton.Text = "Restart Reloading";
            RestartReloadingButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(19, 445);
            label7.Name = "label7";
            label7.Size = new Size(136, 15);
            label7.TabIndex = 19;
            label7.Text = "Disable Input Simulation";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(62, 463);
            label8.Name = "label8";
            label8.Size = new Size(49, 15);
            label8.TabIndex = 20;
            label8.Text = "ALT + Q";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(52, 288);
            label6.Name = "label6";
            label6.Size = new Size(120, 15);
            label6.TabIndex = 21;
            label6.Text = "Ammunition 2 Count";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Absent", "", "Armor-piercing ББ", "", "High explosive ОФ" });
            comboBox1.Location = new Point(4, 205);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(139, 23);
            comboBox1.TabIndex = 22;
            comboBox1.Text = "Armor-piercing ББ";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Absent", "", "Armor-piercing ББ", "", "High explosive ОФ" });
            comboBox2.Location = new Point(4, 259);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(139, 23);
            comboBox2.TabIndex = 23;
            comboBox2.Text = "Absent";
            // 
            // DisableInputSimulationLable
            // 
            DisableInputSimulationLable.AutoSize = true;
            DisableInputSimulationLable.Location = new Point(52, 482);
            DisableInputSimulationLable.Name = "DisableInputSimulationLable";
            DisableInputSimulationLable.Size = new Size(68, 15);
            DisableInputSimulationLable.TabIndex = 24;
            DisableInputSimulationLable.Text = "Active: True";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(6, 350);
            trackBar1.Maximum = 2;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(155, 45);
            trackBar1.TabIndex = 25;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 380);
            label9.Name = "label9";
            label9.Size = new Size(163, 15);
            label9.TabIndex = 26;
            label9.Text = "50mm       105mm       155mm";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(802, 536);
            Controls.Add(label9);
            Controls.Add(trackBar1);
            Controls.Add(DisableInputSimulationLable);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label6);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(RestartReloadingButton);
            Controls.Add(checkBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(ReloadTimeTextBox);
            Controls.Add(Amunition2Count);
            Controls.Add(Amunition1Count);
            Controls.Add(sayHiButton);
            Controls.Add(jsonDebugText);
            Controls.Add(CurrentGearText);
            Controls.Add(label2);
            Controls.Add(GearsQuantityBackInput);
            Controls.Add(label1);
            Controls.Add(GearsQuantityForwardInput);
            Controls.Add(consoleBox);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox consoleBox;
        private TextBox GearsQuantityForwardInput;
        private Label label1;
        private Label label2;
        private TextBox GearsQuantityBackInput;
        private Label CurrentGearText;
        private TextBox jsonDebugText;
        private Button sayHiButton;
        private TextBox Amunition1Count;
        private TextBox Amunition2Count;
        private TextBox ReloadTimeTextBox;
        private Label label3;
        private Label label4;
        private Label label5;
        private CheckBox checkBox1;
        private Button RestartReloadingButton;
        private Label label7;
        private Label label8;
        private Label label6;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label DisableInputSimulationLable;
        private TrackBar trackBar1;
        private Label label9;
    }
}
