﻿namespace DEP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Circle = new System.Windows.Forms.RadioButton();
            this.Square = new System.Windows.Forms.RadioButton();
            this.Select = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Move = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Circle
            // 
            this.Circle.AutoSize = true;
            this.Circle.Location = new System.Drawing.Point(12, 3);
            this.Circle.Name = "Circle";
            this.Circle.Size = new System.Drawing.Size(51, 17);
            this.Circle.TabIndex = 0;
            this.Circle.TabStop = true;
            this.Circle.Text = "Circle";
            this.Circle.UseVisualStyleBackColor = true;
            // 
            // Square
            // 
            this.Square.AutoSize = true;
            this.Square.Location = new System.Drawing.Point(69, 3);
            this.Square.Name = "Square";
            this.Square.Size = new System.Drawing.Size(59, 17);
            this.Square.TabIndex = 1;
            this.Square.TabStop = true;
            this.Square.Text = "Square";
            this.Square.UseVisualStyleBackColor = true;
            // 
            // Select
            // 
            this.Select.AutoSize = true;
            this.Select.Location = new System.Drawing.Point(135, 3);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(55, 17);
            this.Select.TabIndex = 2;
            this.Select.TabStop = true;
            this.Select.Text = "Select";
            this.Select.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(536, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Move
            // 
            this.Move.AutoSize = true;
            this.Move.Location = new System.Drawing.Point(196, 3);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(52, 17);
            this.Move.TabIndex = 4;
            this.Move.TabStop = true;
            this.Move.Text = "Move";
            this.Move.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 361);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.Select);
            this.Controls.Add(this.Square);
            this.Controls.Add(this.Circle);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Circle;
        private System.Windows.Forms.RadioButton Square;
        private System.Windows.Forms.RadioButton Select;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.RadioButton Move;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

