namespace XBot
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.btn_open_type = new System.Windows.Forms.Button();
            this.txt_business_type = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.starttime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.remain = new System.Windows.Forms.Label();
            this.lab_status1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_delete = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_startnbn = new Bunifu.Framework.UI.BunifuFlatButton();
            this.chk_continue = new System.Windows.Forms.CheckBox();
            this.btn_save = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_start = new Bunifu.Framework.UI.BunifuFlatButton();
            this.counttime = new System.Windows.Forms.Label();
            this.btn_open_postcode = new System.Windows.Forms.Button();
            this.txt_postcode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel_tbl = new System.Windows.Forms.TableLayoutPanel();
            this.grid_main = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputBusinessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scrap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bind_main = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txt_server = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.txt_database = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.table = new System.Windows.Forms.Label();
            this.txt_table = new System.Windows.Forms.TextBox();
            this.scrap_count = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_tbl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind_main)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_open_type
            // 
            this.btn_open_type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_open_type.BackgroundImage = global::XBot.Properties.Resources.Open_52px;
            this.btn_open_type.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_open_type.FlatAppearance.BorderSize = 0;
            this.btn_open_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_open_type.Location = new System.Drawing.Point(1187, 16);
            this.btn_open_type.Name = "btn_open_type";
            this.btn_open_type.Size = new System.Drawing.Size(28, 23);
            this.btn_open_type.TabIndex = 2;
            this.btn_open_type.UseVisualStyleBackColor = true;
            this.btn_open_type.Click += new System.EventHandler(this.btn_open_main_Click);
            // 
            // txt_business_type
            // 
            this.txt_business_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_business_type.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txt_business_type.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_business_type.Location = new System.Drawing.Point(308, 16);
            this.txt_business_type.Name = "txt_business_type";
            this.txt_business_type.Size = new System.Drawing.Size(872, 23);
            this.txt_business_type.TabIndex = 1;
            this.txt_business_type.Text = "G:\\Freelancer\\20190511_australia_scrap\\Scrap_bot\\type.txt";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.scrap_count);
            this.panel2.Controls.Add(this.starttime);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.remain);
            this.panel2.Controls.Add(this.lab_status1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 695);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1229, 34);
            this.panel2.TabIndex = 2;
            // 
            // starttime
            // 
            this.starttime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.starttime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.starttime.Location = new System.Drawing.Point(91, 4);
            this.starttime.Name = "starttime";
            this.starttime.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.starttime.Size = new System.Drawing.Size(161, 30);
            this.starttime.TabIndex = 0;
            this.starttime.Text = "2019-05-13";
            this.starttime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Emoji", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(838, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Count";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remain
            // 
            this.remain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.remain.AutoSize = true;
            this.remain.Font = new System.Drawing.Font("Segoe UI Emoji", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remain.ForeColor = System.Drawing.Color.Red;
            this.remain.Location = new System.Drawing.Point(934, 4);
            this.remain.Name = "remain";
            this.remain.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.remain.Size = new System.Drawing.Size(48, 32);
            this.remain.TabIndex = 0;
            this.remain.Text = "0";
            this.remain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lab_status1
            // 
            this.lab_status1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_status1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lab_status1.Location = new System.Drawing.Point(0, 4);
            this.lab_status1.Name = "lab_status1";
            this.lab_status1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lab_status1.Size = new System.Drawing.Size(91, 30);
            this.lab_status1.TabIndex = 0;
            this.lab_status1.Text = "Last Update";
            this.lab_status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(237, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_table);
            this.panel1.Controls.Add(this.table);
            this.panel1.Controls.Add(this.txt_port);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txt_database);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_pass);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_user);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_server);
            this.panel1.Controls.Add(this.btn_delete);
            this.panel1.Controls.Add(this.btn_startnbn);
            this.panel1.Controls.Add(this.chk_continue);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.btn_start);
            this.panel1.Controls.Add(this.counttime);
            this.panel1.Controls.Add(this.btn_open_postcode);
            this.panel1.Controls.Add(this.txt_postcode);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btn_open_type);
            this.panel1.Controls.Add(this.txt_business_type);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1223, 145);
            this.panel1.TabIndex = 0;
            // 
            // btn_delete
            // 
            this.btn_delete.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_delete.BorderRadius = 0;
            this.btn_delete.ButtonText = "Delete DB";
            this.btn_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_delete.DisabledColor = System.Drawing.Color.Gray;
            this.btn_delete.Font = new System.Drawing.Font("Segoe UI Historic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_delete.Iconimage = null;
            this.btn_delete.Iconimage_right = null;
            this.btn_delete.Iconimage_right_Selected = null;
            this.btn_delete.Iconimage_Selected = null;
            this.btn_delete.IconMarginLeft = 0;
            this.btn_delete.IconMarginRight = 0;
            this.btn_delete.IconRightVisible = true;
            this.btn_delete.IconRightZoom = 0D;
            this.btn_delete.IconVisible = true;
            this.btn_delete.IconZoom = 90D;
            this.btn_delete.IsTab = false;
            this.btn_delete.Location = new System.Drawing.Point(1099, 91);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_delete.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_delete.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_delete.selected = false;
            this.btn_delete.Size = new System.Drawing.Size(119, 48);
            this.btn_delete.TabIndex = 13;
            this.btn_delete.Text = "Delete DB";
            this.btn_delete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_delete.Textcolor = System.Drawing.Color.White;
            this.btn_delete.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_startnbn
            // 
            this.btn_startnbn.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_startnbn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_startnbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_startnbn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_startnbn.BorderRadius = 0;
            this.btn_startnbn.ButtonText = "Start NBN";
            this.btn_startnbn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_startnbn.DisabledColor = System.Drawing.Color.Gray;
            this.btn_startnbn.Font = new System.Drawing.Font("Segoe UI Historic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startnbn.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_startnbn.Iconimage = null;
            this.btn_startnbn.Iconimage_right = null;
            this.btn_startnbn.Iconimage_right_Selected = null;
            this.btn_startnbn.Iconimage_Selected = null;
            this.btn_startnbn.IconMarginLeft = 0;
            this.btn_startnbn.IconMarginRight = 0;
            this.btn_startnbn.IconRightVisible = true;
            this.btn_startnbn.IconRightZoom = 0D;
            this.btn_startnbn.IconVisible = true;
            this.btn_startnbn.IconZoom = 90D;
            this.btn_startnbn.IsTab = false;
            this.btn_startnbn.Location = new System.Drawing.Point(848, 91);
            this.btn_startnbn.Name = "btn_startnbn";
            this.btn_startnbn.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_startnbn.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_startnbn.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_startnbn.selected = false;
            this.btn_startnbn.Size = new System.Drawing.Size(119, 48);
            this.btn_startnbn.TabIndex = 12;
            this.btn_startnbn.Text = "Start NBN";
            this.btn_startnbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_startnbn.Textcolor = System.Drawing.Color.White;
            this.btn_startnbn.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startnbn.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            // 
            // chk_continue
            // 
            this.chk_continue.AutoSize = true;
            this.chk_continue.Checked = true;
            this.chk_continue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_continue.ForeColor = System.Drawing.Color.White;
            this.chk_continue.Location = new System.Drawing.Point(639, 118);
            this.chk_continue.Name = "chk_continue";
            this.chk_continue.Size = new System.Drawing.Size(78, 21);
            this.chk_continue.TabIndex = 11;
            this.chk_continue.Text = "Continue";
            this.chk_continue.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.BorderRadius = 0;
            this.btn_save.ButtonText = "Save CVS";
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.DisabledColor = System.Drawing.Color.Gray;
            this.btn_save.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_save.Iconimage = null;
            this.btn_save.Iconimage_right = null;
            this.btn_save.Iconimage_right_Selected = null;
            this.btn_save.Iconimage_Selected = null;
            this.btn_save.IconMarginLeft = 0;
            this.btn_save.IconMarginRight = 0;
            this.btn_save.IconRightVisible = true;
            this.btn_save.IconRightZoom = 0D;
            this.btn_save.IconVisible = true;
            this.btn_save.IconZoom = 90D;
            this.btn_save.IsTab = false;
            this.btn_save.Location = new System.Drawing.Point(974, 91);
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.btn_save.Name = "btn_save";
            this.btn_save.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_save.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_save.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_save.selected = false;
            this.btn_save.Size = new System.Drawing.Size(119, 48);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "Save CVS";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_save.Textcolor = System.Drawing.Color.White;
            this.btn_save.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_start
            // 
            this.btn_start.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_start.BorderRadius = 0;
            this.btn_start.ButtonText = "Start";
            this.btn_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_start.DisabledColor = System.Drawing.Color.Gray;
            this.btn_start.Font = new System.Drawing.Font("Segoe UI Historic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_start.Iconimage = null;
            this.btn_start.Iconimage_right = null;
            this.btn_start.Iconimage_right_Selected = null;
            this.btn_start.Iconimage_Selected = null;
            this.btn_start.IconMarginLeft = 0;
            this.btn_start.IconMarginRight = 0;
            this.btn_start.IconRightVisible = true;
            this.btn_start.IconRightZoom = 0D;
            this.btn_start.IconVisible = true;
            this.btn_start.IconZoom = 90D;
            this.btn_start.IsTab = false;
            this.btn_start.Location = new System.Drawing.Point(723, 91);
            this.btn_start.Name = "btn_start";
            this.btn_start.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_start.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_start.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_start.selected = false;
            this.btn_start.Size = new System.Drawing.Size(119, 48);
            this.btn_start.TabIndex = 5;
            this.btn_start.Text = "Start";
            this.btn_start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_start.Textcolor = System.Drawing.Color.White;
            this.btn_start.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // counttime
            // 
            this.counttime.AutoSize = true;
            this.counttime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.counttime.Location = new System.Drawing.Point(174, 109);
            this.counttime.Name = "counttime";
            this.counttime.Size = new System.Drawing.Size(0, 17);
            this.counttime.TabIndex = 0;
            // 
            // btn_open_postcode
            // 
            this.btn_open_postcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_open_postcode.BackgroundImage = global::XBot.Properties.Resources.Open_52px;
            this.btn_open_postcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_open_postcode.FlatAppearance.BorderSize = 0;
            this.btn_open_postcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_open_postcode.Location = new System.Drawing.Point(1187, 58);
            this.btn_open_postcode.Name = "btn_open_postcode";
            this.btn_open_postcode.Size = new System.Drawing.Size(28, 23);
            this.btn_open_postcode.TabIndex = 4;
            this.btn_open_postcode.UseVisualStyleBackColor = true;
            this.btn_open_postcode.Click += new System.EventHandler(this.btn_open_postcode_Click);
            // 
            // txt_postcode
            // 
            this.txt_postcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_postcode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txt_postcode.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_postcode.Location = new System.Drawing.Point(308, 56);
            this.txt_postcode.Name = "txt_postcode";
            this.txt_postcode.Size = new System.Drawing.Size(872, 23);
            this.txt_postcode.TabIndex = 3;
            this.txt_postcode.Text = "G:\\Freelancer\\20190511_australia_scrap\\Scrap_bot\\postcodes qld.txt";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(237, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "PostCode";
            // 
            // panel_tbl
            // 
            this.panel_tbl.ColumnCount = 1;
            this.panel_tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_tbl.Controls.Add(this.grid_main, 0, 1);
            this.panel_tbl.Controls.Add(this.panel1, 0, 0);
            this.panel_tbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_tbl.Location = new System.Drawing.Point(0, 0);
            this.panel_tbl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_tbl.Name = "panel_tbl";
            this.panel_tbl.RowCount = 2;
            this.panel_tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.panel_tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_tbl.Size = new System.Drawing.Size(1229, 695);
            this.panel_tbl.TabIndex = 3;
            // 
            // grid_main
            // 
            this.grid_main.AllowUserToAddRows = false;
            this.grid_main.AllowUserToDeleteRows = false;
            this.grid_main.BackgroundColor = System.Drawing.Color.Azure;
            this.grid_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.InputBusinessType,
            this.PostCode,
            this.Scrap});
            this.grid_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_main.Location = new System.Drawing.Point(3, 156);
            this.grid_main.Name = "grid_main";
            this.grid_main.ReadOnly = true;
            this.grid_main.RowHeadersVisible = false;
            this.grid_main.RowTemplate.Height = 30;
            this.grid_main.Size = new System.Drawing.Size(1223, 536);
            this.grid_main.TabIndex = 9;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 50;
            // 
            // InputBusinessType
            // 
            this.InputBusinessType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputBusinessType.HeaderText = "BusinessType";
            this.InputBusinessType.Name = "InputBusinessType";
            this.InputBusinessType.ReadOnly = true;
            // 
            // PostCode
            // 
            this.PostCode.FillWeight = 80F;
            this.PostCode.HeaderText = "PostCode";
            this.PostCode.Name = "PostCode";
            this.PostCode.ReadOnly = true;
            this.PostCode.Width = 80;
            // 
            // Scrap
            // 
            this.Scrap.HeaderText = "Scrap";
            this.Scrap.Name = "Scrap";
            this.Scrap.ReadOnly = true;
            // 
            // txt_server
            // 
            this.txt_server.Location = new System.Drawing.Point(77, 5);
            this.txt_server.Name = "txt_server";
            this.txt_server.Size = new System.Drawing.Size(76, 25);
            this.txt_server.TabIndex = 14;
            this.txt_server.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(37, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "User";
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(77, 33);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(77, 25);
            this.txt_user.TabIndex = 17;
            this.txt_user.Text = "root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(38, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Pass";
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(78, 62);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(76, 25);
            this.txt_pass.TabIndex = 17;
            // 
            // txt_database
            // 
            this.txt_database.Location = new System.Drawing.Point(78, 91);
            this.txt_database.Name = "txt_database";
            this.txt_database.Size = new System.Drawing.Size(76, 25);
            this.txt_database.TabIndex = 19;
            this.txt_database.Text = "scrap_t";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Database";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(40, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Port";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(78, 120);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(77, 25);
            this.txt_port.TabIndex = 19;
            this.txt_port.Text = "3306";
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.ForeColor = System.Drawing.Color.White;
            this.table.Location = new System.Drawing.Point(186, 123);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(40, 17);
            this.table.TabIndex = 18;
            this.table.Text = "Table";
            // 
            // txt_table
            // 
            this.txt_table.Location = new System.Drawing.Point(224, 120);
            this.txt_table.Name = "txt_table";
            this.txt_table.Size = new System.Drawing.Size(77, 25);
            this.txt_table.TabIndex = 19;
            this.txt_table.Text = "Scrap";
            // 
            // scrap_count
            // 
            this.scrap_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scrap_count.AutoSize = true;
            this.scrap_count.Font = new System.Drawing.Font("Segoe UI Emoji", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrap_count.ForeColor = System.Drawing.Color.Red;
            this.scrap_count.Location = new System.Drawing.Point(1135, 0);
            this.scrap_count.Name = "scrap_count";
            this.scrap_count.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.scrap_count.Size = new System.Drawing.Size(48, 32);
            this.scrap_count.TabIndex = 1;
            this.scrap_count.Text = "0";
            this.scrap_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(52)))), ((int)(((byte)(88)))));
            this.ClientSize = new System.Drawing.Size(1229, 729);
            this.Controls.Add(this.panel_tbl);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainFrm";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_tbl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_open_type;
        private System.Windows.Forms.TextBox txt_business_type;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lab_status1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel panel_tbl;
        private System.Windows.Forms.DataGridView grid_main;
        private System.Windows.Forms.BindingSource bind_main;
        private System.Windows.Forms.Label starttime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label remain;
        private System.Windows.Forms.Button btn_open_postcode;
        private System.Windows.Forms.TextBox txt_postcode;
        private System.Windows.Forms.Label label5;
        private Bunifu.Framework.UI.BunifuFlatButton btn_start;
        private Bunifu.Framework.UI.BunifuFlatButton btn_save;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chk_continue;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputBusinessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scrap;
        private Bunifu.Framework.UI.BunifuFlatButton btn_startnbn;
        private System.Windows.Forms.Label counttime;
        private Bunifu.Framework.UI.BunifuFlatButton btn_delete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_server;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_database;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_table;
        private System.Windows.Forms.Label table;
        private System.Windows.Forms.Label scrap_count;
    }
}

