﻿namespace StudentTD
{
    partial class EndScreen
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
            this.btnBackMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBackMenu
            // 
            this.btnBackMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnBackMenu.BackgroundImage = global::StudentTD.Properties.Resources.backtomenu;
            this.btnBackMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBackMenu.FlatAppearance.BorderSize = 0;
            this.btnBackMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBackMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBackMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackMenu.Location = new System.Drawing.Point(98, 143);
            this.btnBackMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnBackMenu.Name = "btnBackMenu";
            this.btnBackMenu.Size = new System.Drawing.Size(117, 45);
            this.btnBackMenu.TabIndex = 0;
            this.btnBackMenu.UseVisualStyleBackColor = false;
            this.btnBackMenu.Click += new System.EventHandler(this.btnBackMenu_Click);
            // 
            // EndScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::StudentTD.Properties.Resources.endbackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(299, 199);
            this.Controls.Add(this.btnBackMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EndScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndScreen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EndScreen_FormClosed);
            this.Load += new System.EventHandler(this.EndScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBackMenu;
    }
}