
namespace Web_Ataskaitos
{
    partial class RDS
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
            this.webTextData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // webTextData
            // 
            this.webTextData.Location = new System.Drawing.Point(28, 12);
            this.webTextData.Multiline = true;
            this.webTextData.Name = "webTextData";
            this.webTextData.Size = new System.Drawing.Size(716, 415);
            this.webTextData.TabIndex = 1;
            // 
            // RDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webTextData);
            this.Name = "RDS";
            this.Text = "RDS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox webTextData;
    }
}