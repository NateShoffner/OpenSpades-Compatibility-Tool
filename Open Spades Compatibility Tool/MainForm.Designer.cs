namespace OpenSpadesCompatibilityTool
{
    partial class MainForm
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
            this.checkbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listExtensions = new System.Windows.Forms.ListView();
            this.col_ext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_extstatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblRenderer = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblShader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listModes = new System.Windows.Forms.ListBox();
            this.listVariables = new System.Windows.Forms.ListView();
            this.col_variable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_varvalue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.lblresult = new System.Windows.Forms.Label();
            this.downloadbtn = new System.Windows.Forms.Button();
            this.aboutbtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkbtn
            // 
            this.checkbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbtn.Location = new System.Drawing.Point(12, 506);
            this.checkbtn.Name = "checkbtn";
            this.checkbtn.Size = new System.Drawing.Size(75, 23);
            this.checkbtn.TabIndex = 0;
            this.checkbtn.Text = "Check";
            this.checkbtn.UseVisualStyleBackColor = true;
            this.checkbtn.Click += new System.EventHandler(this.checkbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Supported Video Modes (SDL):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "OpenGL Extensions:";
            // 
            // listExtensions
            // 
            this.listExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listExtensions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_ext,
            this.col_extstatus});
            this.listExtensions.FullRowSelect = true;
            this.listExtensions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listExtensions.Location = new System.Drawing.Point(12, 129);
            this.listExtensions.Name = "listExtensions";
            this.listExtensions.Size = new System.Drawing.Size(398, 133);
            this.listExtensions.TabIndex = 8;
            this.listExtensions.UseCompatibleStateImageBehavior = false;
            this.listExtensions.View = System.Windows.Forms.View.Details;
            // 
            // col_ext
            // 
            this.col_ext.Text = "Extension";
            this.col_ext.Width = 225;
            // 
            // col_extstatus
            // 
            this.col_extstatus.Text = "Status";
            this.col_extstatus.Width = 150;
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(6, 16);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(44, 13);
            this.lblVendor.TabIndex = 7;
            this.lblVendor.Text = "Vendor:";
            // 
            // lblRenderer
            // 
            this.lblRenderer.AutoSize = true;
            this.lblRenderer.Location = new System.Drawing.Point(6, 33);
            this.lblRenderer.Name = "lblRenderer";
            this.lblRenderer.Size = new System.Drawing.Size(54, 13);
            this.lblRenderer.TabIndex = 8;
            this.lblRenderer.Text = "Renderer:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(6, 50);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Version:";
            // 
            // lblShader
            // 
            this.lblShader.AutoSize = true;
            this.lblShader.Location = new System.Drawing.Point(6, 67);
            this.lblShader.Name = "lblShader";
            this.lblShader.Size = new System.Drawing.Size(37, 13);
            this.lblShader.TabIndex = 10;
            this.lblShader.Text = "GLSL:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblShader);
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.lblRenderer);
            this.groupBox1.Controls.Add(this.lblVendor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 86);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OpenGL Information:";
            // 
            // listModes
            // 
            this.listModes.FormattingEnabled = true;
            this.listModes.Location = new System.Drawing.Point(172, 385);
            this.listModes.Name = "listModes";
            this.listModes.Size = new System.Drawing.Size(238, 82);
            this.listModes.TabIndex = 10;
            // 
            // listVariables
            // 
            this.listVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_variable,
            this.col_varvalue});
            this.listVariables.FullRowSelect = true;
            this.listVariables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listVariables.Location = new System.Drawing.Point(12, 290);
            this.listVariables.Name = "listVariables";
            this.listVariables.Size = new System.Drawing.Size(398, 89);
            this.listVariables.TabIndex = 11;
            this.listVariables.UseCompatibleStateImageBehavior = false;
            this.listVariables.View = System.Windows.Forms.View.Details;
            // 
            // col_variable
            // 
            this.col_variable.Text = "Variable";
            this.col_variable.Width = 225;
            // 
            // col_varvalue
            // 
            this.col_varvalue.Text = "Value";
            this.col_varvalue.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "OpenGL Variables:";
            // 
            // lblresult
            // 
            this.lblresult.AutoSize = true;
            this.lblresult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblresult.Location = new System.Drawing.Point(169, 470);
            this.lblresult.Name = "lblresult";
            this.lblresult.Size = new System.Drawing.Size(245, 17);
            this.lblresult.TabIndex = 13;
            this.lblresult.Text = "You are not able to run OpenSpades.";
            this.lblresult.Visible = false;
            // 
            // downloadbtn
            // 
            this.downloadbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadbtn.Location = new System.Drawing.Point(335, 506);
            this.downloadbtn.Name = "downloadbtn";
            this.downloadbtn.Size = new System.Drawing.Size(75, 23);
            this.downloadbtn.TabIndex = 14;
            this.downloadbtn.Text = "Download";
            this.downloadbtn.UseVisualStyleBackColor = true;
            this.downloadbtn.Visible = false;
            this.downloadbtn.Click += new System.EventHandler(this.downloadbtn_Click);
            // 
            // aboutbtn
            // 
            this.aboutbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutbtn.Location = new System.Drawing.Point(12, 477);
            this.aboutbtn.Name = "aboutbtn";
            this.aboutbtn.Size = new System.Drawing.Size(75, 23);
            this.aboutbtn.TabIndex = 15;
            this.aboutbtn.Text = "About...";
            this.aboutbtn.UseVisualStyleBackColor = true;
            this.aboutbtn.Click += new System.EventHandler(this.aboutbtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 541);
            this.Controls.Add(this.aboutbtn);
            this.Controls.Add(this.downloadbtn);
            this.Controls.Add(this.lblresult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listVariables);
            this.Controls.Add(this.listModes);
            this.Controls.Add(this.listExtensions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenSpades Compatibility Tool";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button checkbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listExtensions;
        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.Label lblRenderer;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblShader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader col_ext;
        private System.Windows.Forms.ColumnHeader col_extstatus;
        private System.Windows.Forms.ListBox listModes;
        private System.Windows.Forms.ListView listVariables;
        private System.Windows.Forms.ColumnHeader col_variable;
        private System.Windows.Forms.ColumnHeader col_varvalue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblresult;
        private System.Windows.Forms.Button downloadbtn;
        private System.Windows.Forms.Button aboutbtn;
    }
}

