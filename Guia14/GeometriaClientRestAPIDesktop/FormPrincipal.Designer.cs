namespace GeometriaClientRestAPIDesktop
{
    partial class FormPrincipal
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
            listBox1 = new ListBox();
            btnConsulta = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.DisplayMember = "Descripcion";
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(12, 32);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(282, 327);
            listBox1.TabIndex = 0;
            // 
            // btnConsulta
            // 
            btnConsulta.Location = new Point(309, 115);
            btnConsulta.Name = "btnConsulta";
            btnConsulta.Size = new Size(130, 61);
            btnConsulta.TabIndex = 1;
            btnConsulta.Text = "Actualizar Lista";
            btnConsulta.UseVisualStyleBackColor = true;
            btnConsulta.Click += btnConsulta_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(451, 371);
            Controls.Add(btnConsulta);
            Controls.Add(listBox1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FormPrincipal";
            Text = "Cliente RestAPI";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button btnConsulta;
    }
}
