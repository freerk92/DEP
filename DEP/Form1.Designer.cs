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
            this.Save = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.Redo = new System.Windows.Forms.Button();
            this.Undo = new System.Windows.Forms.Button();
            this.Resize = new System.Windows.Forms.RadioButton();
            this.AddToGroupButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowGroup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // Circle
            // 
            this.Circle.AutoSize = true;
            this.Circle.Checked = true;
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
            this.Square.Location = new System.Drawing.Point(12, 26);
            this.Square.Name = "Square";
            this.Square.Size = new System.Drawing.Size(59, 17);
            this.Square.TabIndex = 1;
            this.Square.Text = "Square";
            this.Square.UseVisualStyleBackColor = true;
            // 
            // Select
            // 
            this.Select.AutoSize = true;
            this.Select.Location = new System.Drawing.Point(12, 49);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(55, 17);
            this.Select.TabIndex = 2;
            this.Select.Text = "Select";
            this.Select.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(30, 387);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Move
            // 
            this.Move.AutoSize = true;
            this.Move.Location = new System.Drawing.Point(11, 72);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(52, 17);
            this.Move.TabIndex = 4;
            this.Move.Text = "Move";
            this.Move.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 321);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(84, 23);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(12, 350);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(84, 23);
            this.Load.TabIndex = 6;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // Redo
            // 
            this.Redo.Location = new System.Drawing.Point(11, 292);
            this.Redo.Name = "Redo";
            this.Redo.Size = new System.Drawing.Size(85, 23);
            this.Redo.TabIndex = 7;
            this.Redo.Text = "Redo";
            this.Redo.UseVisualStyleBackColor = true;
            this.Redo.Click += new System.EventHandler(this.Redo_Click);
            // 
            // Undo
            // 
            this.Undo.Location = new System.Drawing.Point(12, 263);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(84, 23);
            this.Undo.TabIndex = 8;
            this.Undo.Text = "Undo";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // Resize
            // 
            this.Resize.AutoSize = true;
            this.Resize.Location = new System.Drawing.Point(11, 96);
            this.Resize.Name = "Resize";
            this.Resize.Size = new System.Drawing.Size(57, 17);
            this.Resize.TabIndex = 9;
            this.Resize.Text = "Resize";
            this.Resize.UseVisualStyleBackColor = true;
            // 
            // AddToGroupButton
            // 
            this.AddToGroupButton.Location = new System.Drawing.Point(13, 185);
            this.AddToGroupButton.Name = "AddToGroupButton";
            this.AddToGroupButton.Size = new System.Drawing.Size(83, 23);
            this.AddToGroupButton.TabIndex = 11;
            this.AddToGroupButton.Text = "Add to group";
            this.AddToGroupButton.UseVisualStyleBackColor = true;
            this.AddToGroupButton.Click += new System.EventHandler(this.AddToGroupButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(58, 159);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Group nr:";
            // 
            // ShowGroup
            // 
            this.ShowGroup.Location = new System.Drawing.Point(13, 215);
            this.ShowGroup.Name = "ShowGroup";
            this.ShowGroup.Size = new System.Drawing.Size(75, 23);
            this.ShowGroup.TabIndex = 14;
            this.ShowGroup.Text = "Show group";
            this.ShowGroup.UseVisualStyleBackColor = true;
            this.ShowGroup.Click += new System.EventHandler(this.ShowGroup_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 387);
            this.Controls.Add(this.ShowGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.AddToGroupButton);
            this.Controls.Add(this.Resize);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.Redo);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.Save);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Circle;
        private System.Windows.Forms.RadioButton Square;
        private System.Windows.Forms.RadioButton Select;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.RadioButton Move;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.Button Redo;
        private System.Windows.Forms.Button Undo;
        private System.Windows.Forms.RadioButton Resize;
        private System.Windows.Forms.Button AddToGroupButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ShowGroup;
    }
}

