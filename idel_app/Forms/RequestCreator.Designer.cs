namespace idel_app.Forms {
	partial class RequestCreator {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Article = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Cast = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ProviderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.WarePassed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DatePassed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Article,
            this.ProductName,
            this.Count,
            this.Cast,
            this.ProviderName,
            this.WarePassed,
            this.DatePassed,
            this.Comment});
			this.dataGridView1.Location = new System.Drawing.Point(14, 13);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(754, 367);
			this.dataGridView1.TabIndex = 0;
			// 
			// Article
			// 
			this.Article.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Article.HeaderText = "Артикул";
			this.Article.Name = "Article";
			// 
			// ProductName
			// 
			this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ProductName.HeaderText = "Наименование";
			this.ProductName.Name = "ProductName";
			// 
			// Count
			// 
			this.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Count.HeaderText = "Количество";
			this.Count.Name = "Count";
			// 
			// Cast
			// 
			this.Cast.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Cast.HeaderText = "Цена";
			this.Cast.Name = "Cast";
			// 
			// ProviderName
			// 
			this.ProviderName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ProviderName.HeaderText = "Поставщик";
			this.ProviderName.Name = "ProviderName";
			// 
			// WarePassed
			// 
			this.WarePassed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.WarePassed.HeaderText = "На складе";
			this.WarePassed.Name = "WarePassed";
			// 
			// DatePassed
			// 
			this.DatePassed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.DatePassed.HeaderText = "Дата поступления";
			this.DatePassed.Name = "DatePassed";
			// 
			// Comment
			// 
			this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Comment.HeaderText = "Комментарий";
			this.Comment.Name = "Comment";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(641, 386);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(127, 45);
			this.button1.TabIndex = 1;
			this.button1.Text = "Сохранить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// RequestCreator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(780, 440);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGridView1);
			this.Name = "RequestCreator";
			this.Text = "RequestCreator";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Article;
		private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Count;
		private System.Windows.Forms.DataGridViewTextBoxColumn Cast;
		private System.Windows.Forms.DataGridViewTextBoxColumn ProviderName;
		private System.Windows.Forms.DataGridViewTextBoxColumn WarePassed;
		private System.Windows.Forms.DataGridViewTextBoxColumn DatePassed;
		private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
		private System.Windows.Forms.Button button1;
	}
}