namespace Projeto.View.HistoricoMedico
{
    partial class GraficoPressao
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraficoPressao));
            this.chartPrincipal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPrincipal
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPrincipal.ChartAreas.Add(chartArea2);
            this.chartPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartPrincipal.Legends.Add(legend2);
            this.chartPrincipal.Location = new System.Drawing.Point(0, 0);
            this.chartPrincipal.Name = "chartPrincipal";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartPrincipal.Series.Add(series2);
            this.chartPrincipal.Size = new System.Drawing.Size(1141, 366);
            this.chartPrincipal.TabIndex = 1;
            this.chartPrincipal.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Pressão Arterial";
            this.chartPrincipal.Titles.Add(title2);
            // 
            // GraficoPressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 366);
            this.Controls.Add(this.chartPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GraficoPressao";
            this.Text = "Histórico - Pressão";
            ((System.ComponentModel.ISupportInitialize)(this.chartPrincipal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrincipal;
    }
}