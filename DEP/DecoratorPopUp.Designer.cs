namespace DEP
{
    partial class DecoratorPopUp
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
            this.Top = new System.Windows.Forms.RadioButton();
            this.Bottom = new System.Windows.Forms.RadioButton();
            this.Left = new System.Windows.Forms.RadioButton();
            this.Right = new System.Windows.Forms.RadioButton();
            this.Save = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.DecoratorText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Top
            // 
            this.Top.AutoSize = true;
            this.Top.Location = new System.Drawing.Point(12, 78);
            this.Top.Name = "Top";
            this.Top.Size = new System.Drawing.Size(44, 17);
            this.Top.TabIndex = 0;
            this.Top.TabStop = true;
            this.Top.Text = "Top";
            this.Top.UseVisualStyleBackColor = true;
            // 
            // Bottom
            // 
            this.Bottom.AutoSize = true;
            this.Bottom.Location = new System.Drawing.Point(12, 101);
            this.Bottom.Name = "Bottom";
            this.Bottom.Size = new System.Drawing.Size(58, 17);
            this.Bottom.TabIndex = 1;
            this.Bottom.TabStop = true;
            this.Bottom.Text = "Bottom";
            this.Bottom.UseVisualStyleBackColor = true;
            // 
            // Left
            // 
            this.Left.AutoSize = true;
            this.Left.Location = new System.Drawing.Point(12, 124);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(43, 17);
            this.Left.TabIndex = 2;
            this.Left.TabStop = true;
            this.Left.Text = "Left";
            this.Left.UseVisualStyleBackColor = true;
            // 
            // Right
            // 
            this.Right.AutoSize = true;
            this.Right.Location = new System.Drawing.Point(12, 147);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(50, 17);
            this.Right.TabIndex = 3;
            this.Right.TabStop = true;
            this.Right.Text = "Right";
            this.Right.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(143, 167);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(143, 199);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // DecoratorText
            // 
            this.DecoratorText.Location = new System.Drawing.Point(12, 31);
            this.DecoratorText.Name = "DecoratorText";
            this.DecoratorText.Size = new System.Drawing.Size(100, 20);
            this.DecoratorText.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Decorator text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Decorator position";
            // 
            // DecoratorPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 237);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecoratorText);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Bottom);
            this.Controls.Add(this.Top);
            this.Name = "DecoratorPopUp";
            this.Text = "DecoratorPopUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Top;
        private System.Windows.Forms.RadioButton Bottom;
        private System.Windows.Forms.RadioButton Left;
        private System.Windows.Forms.RadioButton Right;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox DecoratorText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}