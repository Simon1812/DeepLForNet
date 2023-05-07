
namespace Artificial.Neural.Network.Number.View {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.numberPictureBox = new System.Windows.Forms.PictureBox();
            this.showResult = new System.Windows.Forms.Button();
            this.thatIsLabel = new System.Windows.Forms.Label();
            this.next = new System.Windows.Forms.Button();
            this.reallynumber = new System.Windows.Forms.Label();
            this.trainingButton = new System.Windows.Forms.Button();
            this.accuracyLabel = new System.Windows.Forms.Label();
            this.generationLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.ModelInfos = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numberPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // numberPictureBox
            // 
            this.numberPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.numberPictureBox.Location = new System.Drawing.Point(0, 0);
            this.numberPictureBox.Name = "numberPictureBox";
            this.numberPictureBox.Size = new System.Drawing.Size(243, 248);
            this.numberPictureBox.TabIndex = 0;
            this.numberPictureBox.TabStop = false;
            // 
            // showResult
            // 
            this.showResult.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.showResult.Location = new System.Drawing.Point(261, 12);
            this.showResult.Name = "showResult";
            this.showResult.Size = new System.Drawing.Size(207, 43);
            this.showResult.TabIndex = 1;
            this.showResult.Text = "Ergebnis anzeigen";
            this.showResult.UseVisualStyleBackColor = true;
            this.showResult.Click += new System.EventHandler(this.ShowResult_Click);
            // 
            // thatIsLabel
            // 
            this.thatIsLabel.AutoSize = true;
            this.thatIsLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.thatIsLabel.Location = new System.Drawing.Point(261, 67);
            this.thatIsLabel.Name = "thatIsLabel";
            this.thatIsLabel.Size = new System.Drawing.Size(154, 25);
            this.thatIsLabel.TabIndex = 3;
            this.thatIsLabel.Text = "Pc sagt es ist eine:";
            this.thatIsLabel.Visible = false;
            // 
            // next
            // 
            this.next.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.next.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.next.Location = new System.Drawing.Point(474, 12);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(207, 43);
            this.next.TabIndex = 4;
            this.next.Text = "Nächste Zahl";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.Next_Click);
            // 
            // reallynumber
            // 
            this.reallynumber.AutoSize = true;
            this.reallynumber.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reallynumber.Location = new System.Drawing.Point(261, 92);
            this.reallynumber.Name = "reallynumber";
            this.reallynumber.Size = new System.Drawing.Size(205, 25);
            this.reallynumber.TabIndex = 5;
            this.reallynumber.Text = "Der tatsächliche Wert ist:";
            this.reallynumber.Visible = false;
            // 
            // trainingButton
            // 
            this.trainingButton.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.trainingButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.trainingButton.Location = new System.Drawing.Point(261, 163);
            this.trainingButton.Name = "trainingButton";
            this.trainingButton.Size = new System.Drawing.Size(207, 43);
            this.trainingButton.TabIndex = 6;
            this.trainingButton.Text = "Model trainieren";
            this.trainingButton.UseVisualStyleBackColor = true;
            this.trainingButton.Click += new System.EventHandler(this.trainingButton_Click);
            // 
            // accuracyLabel
            // 
            this.accuracyLabel.AutoSize = true;
            this.accuracyLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.accuracyLabel.Location = new System.Drawing.Point(497, 198);
            this.accuracyLabel.Name = "accuracyLabel";
            this.accuracyLabel.Size = new System.Drawing.Size(109, 25);
            this.accuracyLabel.TabIndex = 7;
            this.accuracyLabel.Text = "Genauigkeit:";
            this.accuracyLabel.Visible = false;
            // 
            // generationLabel
            // 
            this.generationLabel.AutoSize = true;
            this.generationLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.generationLabel.Location = new System.Drawing.Point(504, 223);
            this.generationLabel.Name = "generationLabel";
            this.generationLabel.Size = new System.Drawing.Size(102, 25);
            this.generationLabel.TabIndex = 8;
            this.generationLabel.Text = "Generation:";
            this.generationLabel.Visible = false;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorLabel.Location = new System.Drawing.Point(261, 117);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(63, 25);
            this.errorLabel.TabIndex = 9;
            this.errorLabel.Text = "Fehler:";
            this.errorLabel.Visible = false;
            // 
            // ModelInfos
            // 
            this.ModelInfos.AutoSize = true;
            this.ModelInfos.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ModelInfos.Location = new System.Drawing.Point(474, 173);
            this.ModelInfos.Name = "ModelInfos";
            this.ModelInfos.Size = new System.Drawing.Size(204, 25);
            this.ModelInfos.TabIndex = 10;
            this.ModelInfos.Text = "Ki Model Informationen:";
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.saveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveButton.Location = new System.Drawing.Point(261, 213);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(207, 43);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Model speichern";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loadButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.loadButton.Location = new System.Drawing.Point(261, 262);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(207, 43);
            this.loadButton.TabIndex = 12;
            this.loadButton.Text = "Model laden";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.ModelInfos);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.generationLabel);
            this.Controls.Add(this.accuracyLabel);
            this.Controls.Add(this.trainingButton);
            this.Controls.Add(this.reallynumber);
            this.Controls.Add(this.next);
            this.Controls.Add(this.thatIsLabel);
            this.Controls.Add(this.showResult);
            this.Controls.Add(this.numberPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numberPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox numberPictureBox;
        private System.Windows.Forms.Button showResult;
        private System.Windows.Forms.Label thatIsLabel;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Label reallynumber;
        private System.Windows.Forms.Button trainingButton;
        private System.Windows.Forms.Label accuracyLabel;
        private System.Windows.Forms.Label generationLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label ModelInfos;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
    }
}

