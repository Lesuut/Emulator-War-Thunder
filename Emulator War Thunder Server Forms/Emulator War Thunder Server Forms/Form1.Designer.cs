namespace Emulator_War_Thunder_Server_Forms
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
            GearsQuantityForwardInput.Location = new Point(12, 30);
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
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 2;
            label1.Text = "Gears Quantity Forward";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 56);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 4;
            label2.Text = "Gears Quantity Back";
            // 
            // GearsQuantityBackInput
            // 
            GearsQuantityBackInput.Location = new Point(12, 74);
            GearsQuantityBackInput.Name = "GearsQuantityBackInput";
            GearsQuantityBackInput.Size = new Size(42, 23);
            GearsQuantityBackInput.TabIndex = 3;
            GearsQuantityBackInput.Text = "1";
            GearsQuantityBackInput.TextAlign = HorizontalAlignment.Center;
            // 
            // CurrentGearText
            // 
            CurrentGearText.AutoSize = true;
            CurrentGearText.Location = new Point(12, 110);
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
            jsonDebugText.Size = new Size(613, 101);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(802, 541);
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
    }
}
