using IB.DC.Model.Entity;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB.DC.Reports
{
    public partial class frmReport : Form
    {
        public List<SpaceReport> srpt = new List<SpaceReport>();
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            ReportDataSource rds = new ReportDataSource("SpaceReportDS", srpt);
            this.rptv1.LocalReport.DataSources.Clear();
            rptv1.LocalReport.ReportEmbeddedResource = "IB.DC.Reports.RelatorioOcupacao.rdlc";
            rptv1.LocalReport.DataSources.Add(rds);
            rds.Value = srpt;
            rptv1.SetDisplayMode(DisplayMode.PrintLayout);
            rptv1.ZoomMode = ZoomMode.Percent;
            rptv1.ZoomPercent = 100;
            rptv1.LocalReport.Refresh();
            this.rptv1.RefreshReport();
        }
    }
}
