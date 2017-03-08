using IB.DC.Business;
using IB.DC.Model.Business;
using IB.DC.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB.DC.Reports
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            string path = "";
            string dtIni = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " 00:00:00";
            string dtFim = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " 23:59:59";
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (System.IO.Path.GetDirectoryName(executable));
            path = System.IO.Path.Combine(path, "DB");
            //path = "D:\\rparana\\Profile\\OneDrive\\IB Tecnologia\\Clientes\\Ed Senado\\Source\\IBDataCollector\\IB.DC.Data\\DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            bool exists = System.IO.Directory.Exists(path);

            if (!exists)
                System.IO.Directory.CreateDirectory(path);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFim.Format = DateTimePickerFormat.Custom;
            dtpFim.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpInicio.Value = DateTime.Parse(dtIni);
            dtpFim.Value = DateTime.Parse(dtFim);

            ListAndar config = (ListAndar)System.Configuration.ConfigurationSettings.GetConfig("ListAndar");

            for(int i=0; i<config.andar.Count(); i++)
            {
                chklistAndar.Items.Add(config.andar[i], true);
            }

            //chklistAndar.Items.Add("1 SS", true);
            //chklistAndar.Items.Add("2 SS", true);
            //chklistAndar.Items.Add("3 SS", true);
            //chklistAndar.Items.Add("4 SS", true);
            //chklistAndar.Items.Add("5 SS", true);
            //chklistAndar.Items.Add("6 SS", true);
            chklistAndar.ColumnWidth = 50;

            cmbRelatorio.SelectedIndex = 0;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ISpacesController sc = new SpacesController();
            List<SpaceReport> sr = new List<SpaceReport>();
            SpaceReport srtemp = new SpaceReport();
            string s = "";
            if (chklistAndar.CheckedItems.Count != 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i <= chklistAndar.CheckedItems.Count - 1; i++)
                {
                    srtemp = new SpaceReport();
                    srtemp.dt_ini = dtpInicio.Value;
                    srtemp.dt_fim = dtpFim.Value;
                    srtemp.andar = chklistAndar.CheckedItems[i].ToString();
                    sr.Add(srtemp);
                }
                sr = sc.GerarRelatorio(1, dtpInicio.Value, dtpFim.Value, sr);
                if (sr.Count > 0)
                { 
                    s = sr[0].andar + "\n" + sr[0].vagas_ocupadas.ToString() + "\n" + sr[0].vagas_ocupadas_percent.ToString();
                    frmReport report = new frmReport();
                    report.srpt = sr;
                    report.Show();
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    s = "Nenhum registro encontrado.";
                    MessageBox.Show(s, "IB Technology", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Não existe andar selecionado.", "IB Tecnologia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {
            //string url = "file://D:/Temp/Senado2.htm";
            string url = "file:///D:/rparana/Profile/Desktop/teste.html";
        
            SpacesController sc = new SpacesController();
            try
            { 
            sc.ColletarDados(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                /*
            List<Space> _spaces = new List<Space>();

            var sp = new Space();

            var sc = new SpacesController();

            List<int> vagastemp = new List<int>();

            WebClient _cli = new WebClient(); //Criação do componente para carregamento da Pagina HTML
            string pagina = _cli.DownloadString(url); //Leitura da pagina HTML
            string _vagasTotais = "Vazio";
            string _vagasLivres = "Vazio";
            string _andar = "Vazio";
            if (pagina.IndexOf("<input name=\"{DT_30_") >= 0)
            {
                //logradouro = Regex.Match(pagina, "<input name=\"{DT_30_.*? value=\"(.*)\".*? > ").Groups[1].Value;
                string pattern1 = "<input name=\"{DT_30_.*?=\"(.+?)\""; //pattern para vagas totais
                string pattern2 = "<div class=\"id2\">(.+?)</div>"; //pattern para andar
                string pattern3 = "<input name=\"{DT_31_.*?=\"(.+?)\""; //pattern para vagas livres

                MatchCollection matches1 = Regex.Matches(pagina, pattern1);
                MatchCollection matches2 = Regex.Matches(pagina, pattern2);
                MatchCollection matches3 = Regex.Matches(pagina, pattern3);
                //MessageBox.Show("Matches found: " + matches3.Count);

                for (int i = 0; i < matches1.Count; i++)
                {
                    sp = new Space();
                    sp.andar = matches2[i + 1].Groups[1].Value.Trim();
                    sp.vagas_totais = long.Parse(matches1[i].Groups[1].Value.Trim());
                    sp.vagas_disponiveis = long.Parse(matches3[i].Groups[1].Value.Trim());
                    sp.vagas_ocupadas = sp.vagas_totais - sp.vagas_disponiveis;
                    _spaces.Add(sp);
                }
            }
            MessageBox.Show("FIM");
            */
        }

        private void frmInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F7)       // Ctrl-S Save
            {
                // Do what you want here
                button1.Visible = !button1.Visible;
                this.Refresh();
            }

        }
    }
}
